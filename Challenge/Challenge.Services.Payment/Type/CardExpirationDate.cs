using Challenge.Services.Payment.Model.Request;

namespace Challenge.Services.Payment.Type
{
    public struct CardExpirationDate
    {
        public CardExpirationDate(MonthOfYear monthOfYear ,int year)
        {
            this.Month = monthOfYear;
            this.Year = year;
        }
        public MonthOfYear Month { get; set; }
        public int Year { get; set; }

    }
}