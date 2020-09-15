using Challenge.Services.Payment.Domain;

namespace Challenge.Services.Payment.Model.Response
{
    public class CreatePaymentResponseModel
    {
        public string TrackingId { get; set; }
        public PaymentStatus Status { get; set; }
    }
}
