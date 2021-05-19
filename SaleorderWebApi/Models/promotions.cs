using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaleorderWebApi.Models
{
    public class promotions
    {
        public string FTInsUser { get; set; } 

        public string FNPriceVerId { get; set; }

        public string FTPriceVerName { get; set; }

        public string FDStartDate { get; set; }

        public string FDEndDate { get; set; }

        public int FNMSysRawMatId { get; set; }

        public int CNGrpCustomerId { get; set; }

        public int CNCustomerId { get; set; }

        public string FTStateActive { get; set; }
    }
    }