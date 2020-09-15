using System;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Challenge.Services.Payment.Common.Infrastructure;
using Challenge.Services.Payment.Model.Response;
using Challenge.Services.Payment.Service.Protocol;
using Challenge.Services.Payment.Test.Fixture;
using Challenge.Services.Payment.Test.Helper;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Challenge.Services.Payment.Test.IntegrationTest
{

    public class PaymentIntegrationTest : IntegrationTestBase, IClassFixture<DependencyResolverFixture>
    {
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;
        private readonly Setting _setting;


        public PaymentIntegrationTest(DependencyResolverFixture fixture)
        {
            var serviceProvide = fixture.ServiceProvider;
            _paymentService = serviceProvide.GetService<IPaymentService>();
            _mapper = serviceProvide.GetService<IMapper>();
            _setting = fixture.Setting;

        }
        [Fact]
        public async Task Process_A_Payment_And_Retrieve_Corresponding_Data_Must_Be_Successful()
        {
            var moq = MockHelper.MoqCreatePaymentBindingModel();
            var payment = await PostAsync(moq, $"{_setting.BaseUrl}/Payment");
            Assert.True(Guid.TryParse(payment.TrackingId, out _));
            var getByTrackingId = await GetAsync<GetPaymentResponseModel>($"{_setting.BaseUrl}/Payment/{payment.TrackingId}");
            Assert.Equal(getByTrackingId.TrackingId, payment.TrackingId);
        }

    }
}
