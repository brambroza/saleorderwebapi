using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaleorderWebApi.Models
{
    public class promotions
    { 
            public string CSUserUpd { get; set; }
         

            public int PromotionId { get; set; }

            public string PromotionName { get; set; }

            public string CSProductCode { get; set; }

            public string StartDate { get; set; }

            public string EndDate { get; set; }

            public int StatusActive { get; set; }

        }
    }