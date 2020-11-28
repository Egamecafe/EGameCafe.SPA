using EGameCafe.SPA.ViewModels;
using FluentValidation;
using System;

namespace EGameCafe.SPA.Validations
{
    public class ResetPasswordValodation : AbstractValidator<IResetPasswordVm>
    {
        public ResetPasswordValodation()
        {
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Require");
                

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Require")
                .Equal(x => x.Password)
                .When(x => !string.IsNullOrWhiteSpace(x.Password))
                .WithMessage("Not macth"); ;
                
        }
    }
}
