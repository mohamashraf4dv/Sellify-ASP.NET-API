namespace Sellify.Application.Features.Authentication.Commands.InternalUserLogin
{
    public class InternalUserLoginCommandValidator : AbstractValidator<InternalUserLoginCommand>
    {
        public InternalUserLoginCommandValidator() 
        {
            RuleFor(x=>x.userLoginDTO.LoginIdentifier).NotEmpty().WithMessage("LoginIdentifer cannot be Empty");
            RuleFor(x => x.userLoginDTO.IsPersistence).NotEmpty().WithMessage("Rememberme shouldn't be empty");
            RuleFor(x => x.userLoginDTO.Password).NotEmpty().WithMessage("Password cannot be empty");
            RuleFor(x => x.userLoginDTO.Password).Length(6).WithMessage("Password cannot be less than 6 character");
        }
    }
}
