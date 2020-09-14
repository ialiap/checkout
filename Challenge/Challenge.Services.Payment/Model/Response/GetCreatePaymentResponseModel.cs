using System;
using Challenge.Services.Payment.Domain;

namespace Challenge.Services.Payment.Model.Response
{
    public class GetCreatePaymentResponseModel
    {
        public string TrackingId { get; set; }
        public PaymentStatus Status { get; set; }
    }
}
