using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaleorderWebApi.Models
{
    public class promotionsdetail
    { 
            public string CSUserUpd { get; set; } 
            public int PromotionId { get; set; }

            public int Seq { get; set; }

            public int QuatityBuymin { get; set; }

            public int StateJoin { get; set; }

            public decimal PriceSale { get; set; }

            public string CSProductCode { get; set; }

            public int QuatityFree { get; set; }

            public string Unitcode { get; set; }

            public decimal DisAmout { get; set; }

            public int StateActive { get; set; }

        }
    }