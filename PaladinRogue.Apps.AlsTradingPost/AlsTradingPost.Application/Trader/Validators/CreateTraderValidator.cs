using AlsTradingPost.Application.Trader.Models;
using FluentValidation;

namespace AlsTradingPost.Application.Trader.Validators
{
    public class CreateTraderValidator : AbstractValidator<RegisterTraderAdto>
    {
        public CreateTraderValidator()
        {
            RuleFor(x => x.Alias)
                .MaximumLength(50)
                .NotEmpty();
            
            RuleFor(x => x.DCI)
                .MaximumLength(50)
                .NotEmpty();
        }
    }
}