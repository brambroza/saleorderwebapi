using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaleorderWebApi.Models
{
    public class customer
    {
        
            public string CSUserUpd { get; set; } 

            public int CNCustomerId { get; set; }

            public string CSCustomerCode { get; set; }

            public string CSCustomerName { get; set; }

            public int CNGrpCustomerId { get; set; }

            public string CSStateActive { get; set; }

            public string CSAddress { get; set; }

            public string CSDeliveryAddriess { get; set; }

            public string CSBranch { get; set; }

            public string CSTaxNo { get; set; }

            public int CNDeliverryTime { get; set; }

            public decimal CNCreditAmt { get; set; }

            public decimal CNCreditPayAmt { get; set; }

            public decimal CNCreditRcvAmt { get; set; }

            public string CSStateCheckBudget { get; set; }

            public int FNMSysCreditAccId { get; set; }

            public int FNMSysDebitAccId { get; set; }

            public string CNCustomerRef { get; set; }

            public string CSNote { get; set; }

            public string CSContactPhone { get; set; }

            public string CSContactName { get; set; }

            public int FNMSysProvinceId { get; set; }

            public string CSCustomerMainCode { get; set; }

            public decimal FNVat { get; set; }

        }
    }