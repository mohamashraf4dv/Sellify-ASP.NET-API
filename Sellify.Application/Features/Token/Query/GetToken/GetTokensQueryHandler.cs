namespace Sellify.Application.Features.Token.Query.GetToken
{
    public class GetTokensQueryHandler : IRequestHandler<GetTokensQuery, GenericResultDTO>
    {
        public Task<GenericResultDTO> Handle(GetTokensQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
