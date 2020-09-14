using Challenge.Services.Payment.Domain;

namespace Challenge.Services.Payment.Model.Response
{
    public class BankProcessPaymentResponseModel
    {
        public string TrackingId { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
    }
}