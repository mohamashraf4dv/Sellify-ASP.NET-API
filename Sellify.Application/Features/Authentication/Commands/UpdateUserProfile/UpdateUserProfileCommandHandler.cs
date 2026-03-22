namespace Sellify.Application.Features.Authentication.Commands.UpdateUserProfile
{
    public class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand, GenericResultDTO>
    {
        private readonly IUserService _userSerive;

        public UpdateUserProfileCommandHandler(IUserService userSerive)
        {
            this._userSerive = userSerive;
        }
        public async Task<GenericResultDTO> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
        {
            return await _userSerive.UpdateUserProfile(request.UpdateUserProfileDTO);
        }
    }
}
