using EGameCafe.SPA.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGameCafe.SPA.Validations
{
    public class RegisterVmValidation : AbstractValidator<IRegisterVm>
    {
        public RegisterVmValidation()
        {
            RuleFor(x => x.Fullname)
                .MaximumLength(50).WithMessage("maximun length is 50")
                .NotEmpty().WithMessage("Require");

            RuleFor(x => x.Item.Username)
                .MaximumLength(50).WithMessage("maximun length is 50")
                .NotEmpty().WithMessage("Require");

            RuleFor(x => x.Item.Email)
                .MaximumLength(50).WithMessage("maximun length is 50")
                .NotEmpty().WithMessage("Require");

            RuleFor(x => x.Item.PhoneNumber)
                .Length(11).WithMessage("Invalid phone number")
                .NotEmpty().WithMessage("Require");

            RuleFor(x => x.Item.Password)
                .MaximumLength(50).WithMessage("maximun length is 50")
                .NotEmpty().WithMessage("Require");
        }
    }
}
