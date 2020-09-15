using AutoFixture;
using Challenge.Services.Payment.Model.Request;

namespace Challenge.Services.Payment.Test.Helper
{
    public static class MockHelper
    {
        public static CreatePaymentBindingModel MoqCreatePaymentBindingModel()
        {
            var moq = (
                    new AutoFixture.Fixture()
                        .Customize(new CardExpirationDateMustBeGreaterThenToday())
                        .Customize(new CvvMustBeAtLeastThreeDigits()))
                        .Customize(new CreditCardNumberGenerator())
                        .Create<CreatePaymentBindingModel>();
            return moq;
        }

    }
}
