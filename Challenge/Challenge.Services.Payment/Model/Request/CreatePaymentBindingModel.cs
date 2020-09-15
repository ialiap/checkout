using Challenge.Services.Payment.Common.Behavior;
using Challenge.Services.Payment.Common.Enum;
using Challenge.Services.Payment.Type;
using Challenge.Services.Payment.Validation;

namespace Challenge.Services.Payment.Model.Request
{
    public class CreatePaymentBindingModel : BindingModel<CreatePaymentBindingModel, CreateMaterialBindingModelValidator>
    {
        public string MerchantId { get; set; }
        public string CardNumber { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public int CVV { get; set; }
        public CardExpirationDate ExpirationDate { get; set; }

    }
}
