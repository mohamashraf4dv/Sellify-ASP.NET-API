namespace Sellify.Infrastructure.Mapperly
{
    [Mapper]
    public static partial class ApplicationUserMapper
    {
        public static partial ApplicationUser UserRegisterationDtoToApplicationUser(UserRegisterationDTO userRegisterationDTO);
        //public static partial ApplicationUser UpdateUserProfileToApplicationUser(UpdateUserProfileDTO updateUserProfileDTO);
        public static partial void UpdateUserProfileToApplicationUser(UpdateUserProfileDTO updateUserProfileDTO, ApplicationUser applicationUser);
    }
}
