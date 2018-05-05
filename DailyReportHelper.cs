
using System;
using System.Collections.Generic;

namespace DailyCollectionAndPayments
{
    public class DailyReportHelper
    {
        public int Id { get; set; }
        public int Salary { get; set; }
        public int Fuel { get; set; }
        public int VendorsRegular { get; set; }
        public int VendorsOverDue { get; set; }
        public string StateName { get; set; }
        public DateTime PaymentDate { get; set; }
        public int Total { get; set; }
        public int TotalSalary { get; set; }
        public int TotalFuelAmount { get; set; }
        public int TotalVendorRegular { get; set; }
        public int TotalVendorOverDue { get; set; }



        public static List<DailyReportHelper> dailyReport = new List<DailyReportHelper>()
        {
            new DailyReportHelper {Id=1,Salary=1000,Fuel=5,VendorsRegular=2,VendorsOverDue=3,StateName="CG-102",PaymentDate=new DateTime(2017,7,21) },
             new DailyReportHelper {Id=2,Salary=2000,Fuel=7,VendorsRegular=4,VendorsOverDue=8,StateName="CG-108",PaymentDate=new DateTime(2017,7,21) },
               new DailyReportHelper {Id=1,Salary=2198,Fuel=5,VendorsRegular=4,VendorsOverDue=8,StateName="UP-108",PaymentDate=new DateTime(2017,7,21) },
               new DailyReportHelper {Id=3,Salary=456,Fuel=9,VendorsRegular=21,VendorsOverDue=7,StateName="AP-CC",PaymentDate=new DateTime(2017,7,21) },
                new DailyReportHelper {Id=4,Salary=456,Fuel=9,VendorsRegular=21,VendorsOverDue=7,StateName="AP-100",PaymentDate=new DateTime(2017,7,21) },
 new DailyReportHelper {Id=4,Salary=456,Fuel=19,VendorsRegular=20,VendorsOverDue=7,StateName="HP-102",PaymentDate=new DateTime(2017,7,21) }
        };
    
    }

   
}