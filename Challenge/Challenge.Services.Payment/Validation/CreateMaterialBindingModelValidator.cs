using System;
using AutoFixture;
using Challenge.Services.Payment.Model.Request;
using Challenge.Services.Payment.Type;
using FluentValidation;

namespace Challenge.Services.Payment.Validation
{
    public class CreateMaterialBindingModelValidator : AbstractValidator<CreatePaymentBindingModel>
    {
        public CreateMaterialBindingModelValidator()
        {
            RuleFor(x => x.Amount).GreaterThanOrEqualTo(1).LessThanOrEqualTo(1000000);
            RuleFor(x => x.CVV).GreaterThanOrEqualTo(100).LessThanOrEqualTo(10000);
            RuleFor(x => x.MerchantId).NotEmpty().NotNull();
            RuleFor(x => x.CardNumber).NotEmpty().NotNull().CreditCard();
            RuleFor(x => x.ExpirationDate).Must(x =>
           {
               var cardExpirationDate = new DateTime(x.Year, (int)x.Month, 1);
               var limit = DateTime.Today.AddMonths(1);
               var max = new DateTime(limit.Year, limit.Month, 1);
               return cardExpirationDate >= max;

           });

        }

    }

}
