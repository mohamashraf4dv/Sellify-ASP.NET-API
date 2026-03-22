namespace Sellify.Application.Features.Authentication.Commands.UpdateUserProfile
{
    public class UpdateUserProfileCommandValidation:AbstractValidator<UpdateUserProfileDTO>
    {
        public UpdateUserProfileCommandValidation()
        {

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email is invalid");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("FirstName is Required")
                .Must(fn=> fn.All(char.IsLetter) ).WithMessage("FirstName should contains letters only");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("LastName is Required")
                .Must(ln => ln.All(char.IsLetter)).WithMessage("LastName should contains letters only");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("UserName is Required")
                .Must(un => un.All(char.IsLetter)).WithMessage("UserName should contains letters only");

            RuleFor(x => x.PhoneNumber).Matches("^\\+?[1-9]\\d{1,14}$").WithMessage("Phonenumber format is invalid write only digits with no spaces.");

        }
    }
}
