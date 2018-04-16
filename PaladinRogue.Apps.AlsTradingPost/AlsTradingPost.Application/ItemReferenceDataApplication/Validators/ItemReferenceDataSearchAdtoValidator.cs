using AlsTradingPost.Application.ItemReferenceDataApplication.Models;
using FluentValidation;

namespace AlsTradingPost.Application.ItemReferenceDataApplication.Validators
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
