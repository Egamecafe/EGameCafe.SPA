using EGameCafe.SPA.ViewModels;
using FluentValidation;

namespace EGameCafe.SPA.Validations
{
    public class OTPValidation : AbstractValidator<IOTPVm>
    {
        public OTPValidation()
        {
            RuleFor(x => x.OTPNumber)
              .NotEmpty().WithMessage("Require");
        }
    }
}
