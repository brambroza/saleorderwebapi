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

    public class saleappMobile
    {
        public string UserLogin { get; set; }
        public string CSSaleOrderNo { get; set; }
        public string StateApp { get; set; }
    }

    public class CheckIn
    {
        public int CNCustomerId { get; set; }
        public string CDDateTrans { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string ImagePath { get; set; }
        public string CSTime { get; set; }
        public string CSUserLogin { get; set; }
        public string Remark { get; set; }
    }

    public class NewCustomer
    {
        public int CNCustomerId { get; set; }
        public string CDDateTrans { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string ImagePath { get; set; }
        public string CSTime { get; set; }
        public string CSUserLogin { get; set; }
        public string Remark { get; set; }
        public string CSContactName { get; set; }
        public string CSContactPhone { get; set; }
        public string CSContactName2 { get; set; }
        public string CSContactPhone2 { get; set; }
        public string CSContactName3 { get; set; }
        public string CSContactPhone3 { get; set; }
        public string CustName { get; set; }
        public string CustAddr { get; set; }
        public int SaleAreaId { get; set; }



    }



    public class CheckInWarehouse
    {
        public int CNCustomerId { get; set; }
        public int CNDeliveryId { get; set; }
        public string CDDateTrans { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string ImagePath { get; set; }
        public string CSTime { get; set; }
        public string CSUserLogin { get; set; }
    }




    public class SaleTrip
    {
        public int CNCustomerId { get; set; }
        public int Seq { get; set; }
        public string CDDateTrans { get; set; }
        public string CSUserLogin { get; set; }
        public string ImagePath { get; set; }

        public string CSTime { get; set; }
        public string StatusCheckIn { get; set; }
        public string Description { get; set; }

    }


    public class appsaletrip
    {
        public int CmpId { get; set; }
        public string UserLogin { get; set; }
        public string DateTrans { get; set; }
        public string UserApp { get; set; }
        public string StateApp { get; set; }

    }

    public class bulkappsaletrip
    {
        public int CmpId { get; set; }
        public string UserLogin { get; set; }
        public string SDate { get; set; }
        public string EDate { get; set; }
        public string UserApp { get; set; }
        public string StateApp { get; set; }

    }


    public class sendappsaletrip
    {
        public int CmpId { get; set; }
        public string UserLogin { get; set; }
        public string DateTrans { get; set; } 
    }



}