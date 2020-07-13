using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaleorderWebApi.Models
{
    public class customershiper
    { 
            public string CSUserUpd { get; set; }
         

            public int CNDeliveryId { get; set; }

            public int CNCustomerId { get; set; }

            public int CNSeq { get; set; }

            public string CSAddressDelivery { get; set; }

            public int FNMSysCountryId { get; set; }

            public int FNMSysProvinceId { get; set; }

            public int FNMSysDistrictId { get; set; }

            public int FNMSysSubDistrictId { get; set; }

            public string CSPostCode { get; set; }

            public string CSEmail { get; set; }

            public string CSPhoneNo { get; set; }

        }
    }