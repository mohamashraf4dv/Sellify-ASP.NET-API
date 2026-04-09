namespace Sellify.Application.Features.Payment.Query.GetStripePaymentIntentClientSecret
{
    public record GetPaymentIntentClientSecretQuery(decimal TotalAmount):IRequest<GenericResultDTO>;
}
