using System;
using System.Threading.Tasks;
using AutoMapper;
using Challenge.Services.Payment.Domain;
using Challenge.Services.Payment.Model.Request;
using Challenge.Services.Payment.Service.Protocol;
using Challenge.Services.Payment.Test.Fixture;
using Challenge.Services.Payment.Test.Helper;
using Challenge.Services.Payment.Type;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Challenge.Services.Payment.Test.UnitTest
{

    public class PaymentServiceShould : IClassFixture<DependencyResolverFixture>
    {
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;


        public PaymentServiceShould(DependencyResolverFixture fixture)
        {
            var serviceProvide = fixture.ServiceProvider;
            _paymentService = serviceProvide.GetService<IPaymentService>();
            _mapper = serviceProvide.GetService<IMapper>();
        }
        [Fact]
        public async Task Create_New_Payment_With_Valid_Data_Must_Be_Success()
        {
            var moq = MockHelper.MoqCreatePaymentBindingModel();
            var result = await _paymentService.ProcessPayment(moq);
            Assert.True(result.Status == PaymentStatus.Success);
            Assert.True(Guid.TryParse(result.TrackingId, out _));
        }

        [Fact]
        public async Task Create_New_Payment_With_Invalid_Expiration_Date_Must_Throw_ArgumentException()
        {
            var moq = MockHelper.MoqCreatePaymentBindingModel();
            moq.ExpirationDate = new CardExpirationDate() { Month = MonthOfYear.April, Year = 1990 };
            await Assert.ThrowsAsync<ArgumentException>(() => _paymentService.ProcessPayment(moq));
        }
        [Fact]
        public async Task Create_New_Payment_With_Invalid_CVV_Must_Throw_ArgumentException()
        {
            var moq = MockHelper.MoqCreatePaymentBindingModel();
            moq.CVV = 1;
            await Assert.ThrowsAsync<ArgumentException>(() => _paymentService.ProcessPayment(moq));
        }
        [Fact]
        public async Task Read_Payment_By_TrackingId_Returns_Single_Result()
        {
            var moq = MockHelper.MoqCreatePaymentBindingModel();
            var newlyInsertedPayment = await _paymentService.ProcessPayment(moq);
            var refetchedPayment = await _paymentService.GetByTrackingId(newlyInsertedPayment.TrackingId);
            Assert.NotNull(refetchedPayment);
            Assert.Equal(refetchedPayment.TrackingId, newlyInsertedPayment.TrackingId);
        }

    }
}
