using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmbucareDAL.Models
{
    public class CommonModel
    {
        public int? T_COST_TYPE_ID { get; set; }
        public string TYPE_NAME { get; set; }
        public int? T_ITEM_ID { get; set; }
        public string ITEM_NAME { get; set; }
        public int? T_STORE_ID { get; set; }
        public int? T_COST_TYPE_DTL_ID { get; set; }

        public string PACK_SIZE { get; set; } //T_ITEM_UM_ID
        public int? T_ITEM_UM_ID { get; set; }
        public int? T_PRICE_ID { get; set; }
        public int? PUR_PRICE { get; set; }
        public decimal? SALE_PRICE { get; set; }
        public string T_ACTIVE { get; set; }
        //T_TOTAL_AMOUNT


        public string TRADE_CODE { get; set; }
        public string V_ITEM_CODE { get; set; }
        public string GEN_CODE { get; set; }
        public string GEN_DESC { get; set; }
        public string TRADE_DESC { get; set; }
        public string Instruction { get; set; }
        public string STRENGTH { get; set; }
        public string PRODUCT_DESC { get; set; }
        public string GEN_NAME { get; set; }
        public decimal? T_QUANTITY { get; set; }
        public string ITEM_CODE { get; set; }
        public int? ChekPriceSet { get; set; }

        public decimal? T_TOTAL_AMOUNT { get; set; }
        //ChekPriceSet

        // -----------for t74095------------
        //public string T_PROB_DELT_LANG2 { get; set; }
        //public string T_REGI_NO { get; set; }
        //public string T_ENGIN_NO { get; set; }


    }

    //public class T74098
    //{
    //    public int T_AUTO_ID { get; set; }
    //    public string T_RECIEVER_ID { get; set; }
    //    public int T_REF_ID { get; set; }
    //    public string T_SENDER_ID { get; set; }
    //    public string T_SENDER_TYPE { get; set; }
    //    public string T_TEXT { get; set; }
    //    public DateTime T_TIME { get; set; }
    //}
}