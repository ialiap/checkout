using System.Threading.Tasks;
using Challenge.Services.Payment.Model.Response;

namespace Challenge.Services.Payment.Service.Protocol
{
    public interface IBankService
    {
        Task<BankProcessPaymentResponseModel> ProcessPaymentAsync(Domain.Payment payment);
    }
}