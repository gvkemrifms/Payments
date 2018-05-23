using System;

namespace DailyCollectionAndPayments
{
    public class ExpectedColectionsModel
    {
        public string State { get; set; }
        public string Project { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public string Day { get; set; }
        public DateTime ExpectedStartDate { get; set; }
        public DateTime ExpectedEndDate { get; set; }
        public decimal Amount { get; set; }
    }
}