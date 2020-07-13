using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaleorderWebApi.Models
{
    public class saleorderdetail
    {
         
            public string CSUserIns { get; set; }
         

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