using Challenge.Services.Payment.Model.Request;

namespace Challenge.Services.Payment.Type
{
    public struct CardExpirationDate
    {
        public MonthOfYear Month;
        public int Year;
    }
}