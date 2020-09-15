using System;
using Challenge.Services.Payment.Common.Enum;
using Challenge.Services.Payment.Type;

namespace Challenge.Services.Payment.Domain
{
    public class Payment : IIdentifiable
    {
        public string Id { get; set; }
        public string MerchantId { get; set; }
        public string CardNumber { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public int CVV { get; set; }
        public CardExpirationDate ExpirationDate { get; set; }
        public PaymentStatus Status { get; set; }
        public DateTime CreationDateTime { get; set; }
        public DateTime? ModificationDateTime { get; set; }
        public string TrackingId { get; set; }

    }
}
