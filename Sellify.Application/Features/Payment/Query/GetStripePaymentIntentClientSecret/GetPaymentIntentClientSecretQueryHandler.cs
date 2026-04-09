namespace Sellify.Application.Features.Payment.Query.GetStripePaymentIntentClientSecret
{
    public class GetPaymentIntentClientSecretQueryHandler : IRequestHandler<GetPaymentIntentClientSecretQuery, GenericResultDTO>
    {
        private readonly IPaymentService _paymentService;

        public GetPaymentIntentClientSecretQueryHandler(IPaymentService paymentService)
        {
            this._paymentService = paymentService;
        }
        public async Task<GenericResultDTO> Handle(GetPaymentIntentClientSecretQuery request, CancellationToken cancellationToken)
        {
            var clientSecret = await _paymentService.GetPaymentIntentClientSecret(request.TotalAmount);
            return new GenericResultDTO(clientSecret, 200);
        }
    }
}
