using System.Threading.Tasks;
using Challenge.Services.Payment.Model.Request;
using Challenge.Services.Payment.Model.Response;

namespace Challenge.Services.Payment.Service.Protocol
{
    public interface IPaymentService
    {
        Task<CreatePaymentResponseModel> ProcessPayment(CreatePaymentBindingModel requestModel);

        Task<GetPaymentResponseModel> GetByTrackingId(string trackingId);

    }
}

