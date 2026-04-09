using Sellify.Application.Contracts;
using Sellify.Application.Contracts.Repositories;

namespace Sellify.Application.Features.Payment.Command.CreatePaymentIntent
{
    public class CreatePaymentSessionCommandHandler : IRequestHandler<CreatePaymentSessionCommand, GenericResultDTO>
    {
        private readonly IPaymentService _paymentService;
        private readonly IProductRepository _productRepository;

        public CreatePaymentSessionCommandHandler(IPaymentService paymentService,IProductRepository productRepository )
        {
            this._paymentService = paymentService;
            this._productRepository = productRepository;
        }
        public async Task<GenericResultDTO> Handle(CreatePaymentSessionCommand request, CancellationToken cancellationToken)
        {
            var productsFromDB = _productRepository.GetAllQueryable(p => request.ProductsIds.Contains(p.Id)).ToList().AsReadOnly();
            var sessionUrl =await _paymentService.CreatePaymentSession(productsFromDB, request.OrderDTO,request.BuyerId);
            return new GenericResultDTO(sessionUrl,200);
        }
    }
}
