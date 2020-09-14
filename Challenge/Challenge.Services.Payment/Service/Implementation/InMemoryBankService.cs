using System;
using System.Threading.Tasks;
using Challenge.Services.Payment.Domain;
using Challenge.Services.Payment.Model.Response;
using Challenge.Services.Payment.Service.Protocol;

namespace Challenge.Services.Payment.Service.Implementation
{
    public class InMemoryBankService : IBankService
    {
        public async Task<BankProcessPaymentResponseModel> ProcessPaymentAsync(Domain.Payment payment)
        {
            await Task.Delay(3000);
            return new BankProcessPaymentResponseModel() { TrackingId = Guid.NewGuid().ToString(), PaymentStatus = PaymentStatus.Success };
        }
    }
}