using AlsTradingPost.Application.MagicItemTemplate.Models;
using FluentValidation;

namespace AlsTradingPost.Application.MagicItemTemplate.Validators
{
    public class MagicItemTemplateSearchValidator : AbstractValidator<MagicItemTemplateSearchAdto>
    {
        public MagicItemTemplateSearchValidator()
        {
            RuleFor(x => x.Name)
                .Length(3, 50);
        }
    }
}
