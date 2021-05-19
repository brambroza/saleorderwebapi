using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaleorderWebApi.Models
{
    public class saleorder
    {
       
            public string CSUserIns { get; set; }

            public string CSSaleOrderNo { get; set; }

            public string CDSaeOrderDate { get; set; }

            public string CSCustomerCode { get; set; }

            public string CSFactoryCode { get; set; }

            public string CSCustomerType { get; set; }

            public string CSCustomDocNo { get; set; }
         

            public string CSPayType { get; set; }

            public string CDDeliveryDate { get; set; }

            public int CSDeliveryTime { get; set; }

            public decimal CNSaleOrderAmt { get; set; }

        public decimal CNSaleOrderDiscountPer1 { get; set; }

        public decimal CNSaleOrderDiscountPer2 { get; set; }

        public decimal CNSaleOrderDiscountPer3 { get; set; }

        public decimal CNSaleOrderDiscountAmt1 { get; set; }

            public decimal CNSaleOrderDiscountAmt2 { get; set; }

            public decimal CNSaleOrderDiscountAmt3 { get; set; }

            public decimal CNSaleOrderNetAmt { get; set; }

            public decimal CNSaleOrderVatPer { get; set; }

            public decimal CNSaleOrderVatAmt { get; set; }

            public decimal CNSaleOrderGrandTotalAmt { get; set; }

            public string CSRemark { get; set; }
         

            public int CNSaleOrderType { get; set; }

            public string CSDeliveryAddress { get; set; }

          

            public string CDSaleOrderDate { get; set; }
 

            public string FTStateNotConvert { get; set; }

        public int FNMSysCmpId { get; set; }

        public int CNGrpCustomerId { get; set; }
        public string FTStateSendApp { get; set; }



    }

    public class saleorderappM
    {

        public string CSUserIns { get; set; }

        public string CSSaleOrderNo { get; set; }

        public string CDSaeOrderDate { get; set; }

        public string CSCustomerCode { get; set; }

        public string CSFactoryCode { get; set; }

        public string CSCustomerType { get; set; }

        public string CSCustomDocNo { get; set; }


        public string CSPayType { get; set; }

        public string CDDeliveryDate { get; set; }

        public int CSDeliveryTime { get; set; }

        public decimal CNSaleOrderAmt { get; set; }

        public decimal CNSaleOrderDiscountPer1 { get; set; }

        public decimal CNSaleOrderDiscountPer2 { get; set; }

        public decimal CNSaleOrderDiscountPer3 { get; set; }

        public decimal CNSaleOrderDiscountAmt1 { get; set; }

        public decimal CNSaleOrderDiscountAmt2 { get; set; }

        public decimal CNSaleOrderDiscountAmt3 { get; set; }

        public decimal CNSaleOrderNetAmt { get; set; }

        public decimal CNSaleOrderVatPer { get; set; }

        public decimal CNSaleOrderVatAmt { get; set; }

        public decimal CNSaleOrderGrandTotalAmt { get; set; }

        public string CSRemark { get; set; }


        public int CNSaleOrderType { get; set; }

        public string CSDeliveryAddress { get; set; }



        public string CDSaleOrderDate { get; set; }


        public string FTStateNotConvert { get; set; }

        public int FNMSysCmpId { get; set; }

        public string FTStateApp { get; set; }

      

    }


}