using EGameCafe.SPA.ViewModels;
using FluentValidation;


namespace EGameCafe.SPA.Validations
{
    public class CreateGroupValidation : AbstractValidator<ICreateGroupVm>
    {
        public CreateGroupValidation()
        {
            RuleFor(x => x.Item.GameId)
                .MaximumLength(64).WithMessage("maximun length is 64")
                .NotEmpty().WithMessage("Require");

            RuleFor(x => x.Item.GroupName)
                .MaximumLength(50).WithMessage("maximun length is 50")
                .NotEmpty().WithMessage("Require");

            RuleFor(x => x.Item.GroupType)
                .NotNull().WithMessage("Require");
        }
    }
}
