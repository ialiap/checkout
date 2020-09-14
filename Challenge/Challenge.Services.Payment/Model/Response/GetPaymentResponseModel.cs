using System;
using Challenge.Services.Payment.Domain;

namespace Challenge.Services.Payment.Model.Response
{
    public class GetPaymentResponseModel
    {
        public string CardNumber { get; set; }
        public PaymentStatus Status { get; set; }
        public DateTime TransactionDatetime { get; set; }
        public string TrackingId { get; set; }

    }
}
