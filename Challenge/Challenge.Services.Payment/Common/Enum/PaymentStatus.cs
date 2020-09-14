namespace Challenge.Services.Payment.Domain
{
    public enum PaymentStatus
    {
        Pending, // the payment is waiting to be processed. 
        Success, // the payment has been processed and accepted.
        Rejected,
    }
}