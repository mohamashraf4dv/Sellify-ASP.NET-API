using Microsoft.Extensions.Options;
using Sellify.Application.Features.Orders.Command.CreateOrder;
using Sellify.Application.Features.Payment.Command.CreatePaymentSession;
using Sellify.Application.Global;
using Sellify.Infrastructure.AppSettingOptions;
using Stripe;
using Stripe.Checkout;
using Stripe.V2;

namespace Sellify.Infrastructure.Services
{
    public class PaymentService:IPaymentService
    {
        private readonly IOptions<URLS> _domainOptions;
        private readonly IGenericRepositoryWithNoSoftDeleteAndUpdate<PaymentSession> _paymentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(IOptions<URLS> clientDomainOptions,IGenericRepositoryWithNoSoftDeleteAndUpdate<PaymentSession> paymentRepository, IUnitOfWork unitOfWork)
        {
            this._domainOptions = clientDomainOptions;
            this._paymentRepository = paymentRepository;
            this._unitOfWork = unitOfWork;
        }
        public async Task<string> GetPaymentIntentClientSecret(decimal amount , string currency = "usd")
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = Convert.ToInt64(amount) * 100,
                Currency = currency,
                AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                {
                    Enabled = true,
                },
            };
            var service = new PaymentIntentService();

            var paymentIntent = await service.CreateAsync(options);
            return paymentIntent.ClientSecret;

        }

        public async Task<string> CreatePaymentSession(IReadOnlyList<Domain.Entities.Product> products ,IReadOnlyDictionary<Guid, CreatePaymentSessionUsingOrderDTO> createOrderDto,string buyerId)
        {
            var lintItemsList = new List<SessionLineItemOptions>();
            foreach (var product in products) 
            {
                lintItemsList.Add(new SessionLineItemOptions()
                {
                    PriceData = new SessionLineItemPriceDataOptions()
                    {
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions()
                        {
                            Name = product.Name,
                            // -- won't work locally since stripe doesn't have access to my local machine -> uncomment it in production --
                            //Images = new List<string> { $"{_domainOptions.Value.ServerDomain}/{product.ThumbnailSource}" }, 
                        }
                            ,
                        UnitAmountDecimal = product.Price * 100

                    }
                        ,
                    Quantity = createOrderDto[product.Id].Quantity,
                    Metadata = new Dictionary<string, string>()
                    {
                        ["SellerId"] = product.SellerId,
                        ["BuyerId"] = buyerId,
                        ["ProductId"] = product.Id.ToString()
                    }
                });
            }

            var options = new SessionCreateOptions()
            {
                LineItems = lintItemsList,
                Mode = "payment",
                SuccessUrl = $"{_domainOptions.Value.ClientDomain}/success?session_id={{CHECKOUT_SESSION_ID}}",
                CancelUrl = $"{_domainOptions.Value.ClientDomain}/cancel"
            };
            var service = new SessionService();
            Session session = await service.CreateAsync(options);
            var result = await SaveSession(session.Id, buyerId, products);
            return result ? session.Url : "";
        }

        public async Task<Dictionary<string,OrderSentFromStripeDTO>> GetOrderBySessionId(string sessionId)
        {
            var sessionService = new SessionService();
            var session = await sessionService.GetAsync(sessionId);
            if (session?.PaymentStatus != "paid")
               return null;

            var lineItemService = new SessionLineItemService();

            var list = await lineItemService.ListAsync(sessionId);
            //var selectedList = list.Select(l => new OrderSentFromStripeDTO(l.Metadata["SellerId"], l.Metadata["BuyerId"], l.Metadata["ProductId"], l.Price.UnitAmountDecimal, l.Quantity, l.AmountTotal));
            var selectedList = list.GroupBy(l => l.Metadata["ProductId"], l=>  new OrderSentFromStripeDTO(l.Metadata["SellerId"], l.Metadata["BuyerId"], l.Metadata["ProductId"], l.Price.UnitAmountDecimal, l.Quantity, l.AmountTotal)).ToDictionary(g=> g.Key, i=> i.FirstOrDefault());
            return selectedList;
        }
        private async Task<bool> SaveSession(string sessionId, string buyerId, IReadOnlyList<Domain.Entities.Product> products)
        {
            var paymentSession = new PaymentSession() { BuyerId = buyerId, SessionId = sessionId, TotalAmount = products.Sum(p => p.Price) };
            await _paymentRepository.CreateAsync(paymentSession);
            var result = await _unitOfWork.SaveChangesAsync();
            return result > 0;
        }
    }
}
