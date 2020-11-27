using EGameCafe.SPA.ViewModels;
using FluentValidation;

namespace EGameCafe.SPA.Validations
{
    public class LoginVmValidation : AbstractValidator<ILoginVm>
    {
        public LoginVmValidation()
        {
            RuleFor(x => x.Item.Username)
                .MaximumLength(50).WithMessage("maximun length is 50")
                .NotEmpty().WithMessage("Require");

            RuleFor(x => x.Item.Password)
                .MaximumLength(50).WithMessage("maximun length is 50")
                .NotEmpty().WithMessage("Require");
        }
    }
}
