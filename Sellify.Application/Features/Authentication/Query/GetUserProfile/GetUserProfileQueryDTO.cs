using Sellify.Domain.Enums;

namespace Sellify.Application.Features.Authentication.Query.GetUserProfile
{
    public record GetUserProfileQueryDTO(string FirstName,string LastName, string Email, string UserName, string PhoneNumber, IList<string> Roles,string? SellerRoleRequestStatus);

}
