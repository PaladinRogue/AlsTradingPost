using AlsTradingPost.Application.ItemReferenceData.Models;
using FluentValidation;

namespace AlsTradingPost.Application.ItemReferenceData.Validators
{
    public class ItemReferenceDataSearchAdtoValidator : AbstractValidator<ItemReferenceDataSearchAdto>
    {
        public ItemReferenceDataSearchAdtoValidator()
        {
            RuleFor(x => x.Name)
                .Length(3, 50);
        }
    }
}
