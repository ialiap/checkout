using System;
using AutoFixture;
using Challenge.Services.Payment.Model.Request;

namespace Challenge.Services.Payment.Test.Helper
{
    public class CvvMustBeAtLeastThreeDigits : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            var value = new Random().Next(1000, 10000);
            fixture.Customize<CreatePaymentBindingModel>(composer =>
                composer.With(p => p.CVV, 1500));
        }
    }
}