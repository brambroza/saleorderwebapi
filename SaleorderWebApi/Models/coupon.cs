using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaleorderWebApi.Models
{
    public class coupon
    {

        public string FTInsUser { get; set; }
        
        public int FNMSysCouponId { get; set; }
        public string FTCouponCode { get; set; }
        public string FTDescription { get; set; }
        public string FDStartDate { get; set; }
        public string FDEndDate { get; set; }
        public string FTStateActive { get; set; }
        public decimal FNDisAmt { get; set; }

    }


}