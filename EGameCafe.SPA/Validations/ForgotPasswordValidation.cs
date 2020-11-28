using EGameCafe.SPA.Models;
using FluentValidation;


namespace EGameCafe.SPA.Validations
{
    public class ForgotPasswordValidation : AbstractValidator<ForgotPasswordModel>
    {
        public ForgotPasswordValidation()
        {
            RuleFor(x => x.Email)
                .MaximumLength(50).WithMessage("maximun length is 50")
                .NotEmpty().WithMessage("Require");

            RuleFor(x => x.Confirmation)
                .Length(11).WithMessage("Invalid phone number")
                .NotEmpty().WithMessage("Require");
        }
    }
}
