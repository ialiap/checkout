using AutoFixture;
using Challenge.Services.Payment.Model.Request;

namespace Challenge.Services.Payment.Test.Helper
{
    public class CreditCardNumberGenerator : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<CreatePaymentBindingModel>(composer =>
                composer.With(p => p.CardNumber, "371390971360579"));
        }
    }
}