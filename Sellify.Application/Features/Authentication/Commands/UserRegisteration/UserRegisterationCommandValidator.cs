
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Sellify.Application.Features.Authentication.Commands.UserRegisteration
{
    public class UserRegisterationCommandValidator:AbstractValidator<UserRegisterationCommand>
    {
        private DateOnly maxDateOnlyFromNowToBeEighteen => DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-18));
        private DateOnly minDateOnly => maxDateOnlyFromNowToBeEighteen.AddYears(-100);
        public UserRegisterationCommandValidator()
        {
            RuleFor(x => x.userRegisteration)
                .NotNull().When(u=>u.userRegisteration is not null).WithMessage("You didn't encapsulate the properties.");

            RuleFor(x => x.userRegisteration.Email)
                .NotNull().When(u => u.userRegisteration is not null).NotEmpty().WithMessage("Email is required.")
                .When(u => u.userRegisteration is not null).WithMessage("Email is not a valid email address");

            RuleFor(x => x.userRegisteration.DateOfBirth)
                .Must(BeAValidDate).WithMessage("Dateofbirth is invalid")
                .DependentRules(() =>
                {
                    RuleFor(x => x.userRegisteration.DateOfBirth)
                            .Must(BeAdult).WithMessage("Your age should be above 18");
                });

            RuleFor(x => x.userRegisteration.FirstName)
                .NotEmpty().When(u => u.userRegisteration is not null).WithMessage("Lastname must be at least 2 characters. You entered 0 characters.")
                .MinimumLength(2).When(u => u.userRegisteration is not null).WithMessage("Firstname must be at least 2 characters");

            RuleFor(x => x.userRegisteration.LastName)
                .NotEmpty().When(u => u.userRegisteration is not null).WithMessage("Lastname must not be 0 characters.")
                .MinimumLength(2).When(u => u.userRegisteration is not null).WithMessage("Lastname must be at least 2 characters.");

            RuleFor(x=>x.userRegisteration.Password)
                .NotEmpty().When(u => u.userRegisteration is not null).WithMessage("Password must not be 0 characters.")
                .MinimumLength(6).When(u => u.userRegisteration is not null).WithMessage("Password must be at least 6 characters.");

            RuleFor(x => x.userRegisteration.PhoneNumber).Must(BeAValidPhoneNumber).WithMessage("Phonenumber format is invalid write only digits with no spaces.");
        }
        private bool BeAValidDate(string date) => DateOnly.TryParse(date, out _);
        private bool BeAdult(string date) 
        {
            DateOnly dateParsed;
           bool parsedCorrectly = DateOnly.TryParse(date, out dateParsed);

            if (!parsedCorrectly)
                return false;

            return (dateParsed <= maxDateOnlyFromNowToBeEighteen && dateParsed >= minDateOnly);
        }
        private bool BeAValidPhoneNumber(string? phoneNumber) 
        {
            if (string.IsNullOrEmpty(phoneNumber))
                return true;

            return Regex.IsMatch(phoneNumber,@"^\+?[1-9]\d{1,14}$");
        }
    }
}
