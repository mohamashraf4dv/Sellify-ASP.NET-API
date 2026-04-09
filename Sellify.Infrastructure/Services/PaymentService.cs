using Microsoft.Extensions.Options;
using Sellify.Application.Features.Orders.Command.CreateOrder;
using Sellify.Infrastructure.AppSettingOptions;
using Stripe;
using Stripe.Checkout;
using Stripe.V2;

namespace Sellify.Infrastructure.Services
{
    public class PaymentService:IPaymentService
    {
        private readonly IOptions<URLS> _domainOptions;

        public PaymentService(IOptions<URLS> clientDomainOptions)
        {
            this._domainOptions = clientDomainOptions;
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

        public async Task<string> CreatePaymentSession(IReadOnlyList<Domain.Entities.Product> products ,IReadOnlyDictionary<Guid,CreateOrderDTO> createOrderDto,string buyerId)
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
            return session.Url;
        }
    }
}
