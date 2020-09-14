using Challenge.Services.Payment.Model.Request;
using FluentValidation;

namespace Challenge.Services.Payment.Validation
{
    public class CreateMaterialBindingModelValidator : AbstractValidator<CreatePaymentBindingModel>
    {
        public CreateMaterialBindingModelValidator()
        {
            RuleFor(x => x.Amount).GreaterThanOrEqualTo(1);
        }
    }
}
