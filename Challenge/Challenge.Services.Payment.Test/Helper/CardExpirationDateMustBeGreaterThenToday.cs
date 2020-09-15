using System;
using AutoFixture;
using Challenge.Services.Payment.Model.Request;
using Challenge.Services.Payment.Type;

namespace Challenge.Services.Payment.Test.Helper
{
    public class CardExpirationDateMustBeGreaterThenToday : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            var today = DateTime.Now.AddYears(1);
            fixture.Customize<CardExpirationDate>(composer => composer.With(p => p.Year, today.Year));
        }
    }
}