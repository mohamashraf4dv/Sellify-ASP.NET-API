namespace Sellify.Application.Features.Seller.Command.RequestRole
{
    public class RequestRoleCommandHandler : IRequestHandler<RequestRoleCommand, GenericResultDTO>
    {
        private readonly IUserService _userService;

        public RequestRoleCommandHandler(IUserService userService)
        {
            this._userService = userService;
        }
        public async Task<GenericResultDTO> Handle(RequestRoleCommand request, CancellationToken cancellationToken)
        {
           return await _userService.RequestRoleAsync(request.RefreshToken,request.SellerRoleRequestStatus);
        }
    }
}