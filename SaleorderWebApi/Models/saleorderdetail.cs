using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaleorderWebApi.Models
{
    public class saleorderdetail
    {

        public string user { get; set; }

        public int docno { get; set; }

        public string productcode { get; set; }
        public string productname { get; set; }
        public int FNMSysRawMatId { get; set; }
        public int FNPcsQty { get; set; }
        public decimal FNPcsPrice { get; set; }
        public int FNDozQty { get; set; }
        public decimal FNDozPrice { get; set; }
        public int FNBoxQty { get; set; }
        public decimal FNBoxPrice { get; set; }
        public int FNCaseQty { get; set; }
        public decimal FNCasePrice { get; set; }
        public int FNFreePcs { get; set; }
        public int FNFreeDoz { get; set; }
        public int FNSaleOrderType { get; set; }
        public decimal FNAmount { get; set; }



    }

    public class TPreSaleOrder_Detail
    {
        public string CSUserIns { get; set; }

        public string CDDateIns { get; set; } 

        public string CSSaleOrderNo { get; set; }

        public string CSProductCode { get; set; }

        public string CSUnitCode { get; set; }

        public decimal CNQty { get; set; }

        public decimal CNUnitPrice { get; set; }

        public decimal CNDiscountPer1 { get; set; }

        public decimal CNDiscountPer2 { get; set; }

        public decimal CNDiscountPer3 { get; set; }

        public decimal CNDiscountAmt { get; set; }

        public decimal CNAmount { get; set; }

        public int Seq { get; set; }

        public string FTStateFree { get; set; }

        public int FTStateOther { get; set; }

    }

}