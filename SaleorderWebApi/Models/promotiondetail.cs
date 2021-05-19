using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaleorderWebApi.Models
{
    public class promotiondetail
    {
        public string UserLogin { get; set; }
        public string FNPriceVerId { get; set; }
        public int Seq { get; set; }
        public string Description { get; set; }
        public string CSUnitCode { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
        public decimal DisAmt { get; set; }
        public decimal QuatityFree { get; set; }
        public string UnitcodeFree { get; set; }
        public Boolean StateJoin { get; set; }
        public Boolean StateAccumulate { get; set; }
        public int PointQty { get; set; }
         

    }
}