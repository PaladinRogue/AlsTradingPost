using AlsTradingPost.Application.ItemReferenceData.Models;
using FluentValidation;

namespace AlsTradingPost.Application.ItemReferenceData.Validators
{
    public class ItemReferenceDataSearchValidator : AbstractValidator<ItemReferenceDataSearchAdto>
    {
        public ItemReferenceDataSearchValidator()
        {
            RuleFor(x => x.Name)
                .Length(3, 50);
        }
    }
}
