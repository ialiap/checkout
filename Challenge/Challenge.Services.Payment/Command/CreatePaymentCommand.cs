using Challenge.Services.Payment.Model.Request;

namespace Challenge.Services.Payment.Command
{
    public class CreatePaymentCommand
    {
        public CreatePaymentBindingModel Payload { get; set; }
        
    }
}
