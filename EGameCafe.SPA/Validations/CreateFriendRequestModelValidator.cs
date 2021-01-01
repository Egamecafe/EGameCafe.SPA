using EGameCafe.SPA.Models;
using EGameCafe.SPA.ViewModels;
using FluentValidation;

namespace EGameCafe.SPA.Validations
{
    public class CreateFriendRequestModelValidator : AbstractValidator<FriendRequestCreateModel>
    {
        public CreateFriendRequestModelValidator()
        {
            RuleFor(x => x.ReceiverId)
                .MaximumLength(64).WithMessage("maximun length is 64")
                .NotEmpty().WithMessage("Require");

            RuleFor(x => x.SenderId)
                .MaximumLength(64).WithMessage("maximun length is 64")
                .NotEmpty().WithMessage("Require");
        }
    }
}
