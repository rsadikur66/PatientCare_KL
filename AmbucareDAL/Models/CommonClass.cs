using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using AmbucareDAL.Repository.DAL;

namespace AmbucareDAL.Models
{
    public static class CommonClass
    {
        private static AmbucareContainer obj = new AmbucareContainer();
        public static string General_Code(string type)
        {
            string code = "";
            string entryyear = DateTime.Now.Year.ToString();
            string yr = entryyear.Substring(entryyear.Length - Math.Min(2, entryyear.Length));
            string ad = HttpContext.Current.Session["T_USER_ID"].ToString();
            if (type == "PR")
            {
                int ID = obj.T74030.Count()>0? obj.T74030.Where(m => m.T_ENTRY_USER == ad).Max(a => a.T_PUR_ID):0;
                string PUR_NO = ID!=0?obj.T74030.Where(m => m.T_PUR_ID == ID).Select(a => a.T_PUR_NO).FirstOrDefault():"PR-"+yr+"-00000";
                string lst_Digit = PUR_NO.Substring(PUR_NO.Length - Math.Min(5, PUR_NO.Length));
                lst_Digit = (Int32.Parse(lst_Digit) + 1).ToString();
                code = type+"-" + yr + "-" + lst_Digit.PadLeft(5, '0');
            }
           else if (type == "SR")
            {
               
                int ID = obj.T74048.Count() > 0 ? obj.T74048.Where(m => m.T_ENTRY_USER == ad).Max(a => a.T_SL_REQ_ID) : 0;
                string SALE_NO = ID != 0 ? obj.T74048.Where(m => m.T_SL_REQ_ID == ID).Select(a => a.T_SL_REQ_NO).FirstOrDefault() : "SR-" + yr + "-00000";
                string lst_Digit = SALE_NO.Substring(SALE_NO.Length - Math.Min(5, SALE_NO.Length));
                lst_Digit = (Int32.Parse(lst_Digit) + 1).ToString();
                code = type + "-" + yr + "-" + lst_Digit.PadLeft(5, '0');
            }
            else if (type == "TI")
            {
                int? ID = obj.T74080.Count() > 0 ? obj.T74080.Where(m => m.T_ENTRY_USER == ad).Max(a => a.T_TRANSFER_REQ_ID) : 0;
                string SALE_NO = ID != 0 ? obj.T74080.Where(m => m.T_TRANSFER_REQ_ID == ID).Select(a => a.T_TRANSFER_REQ_NO).FirstOrDefault() : "TI-" + yr + "-00000";
                string lst_Digit = SALE_NO.Substring(SALE_NO.Length - Math.Min(5, SALE_NO.Length));
                lst_Digit = (Int32.Parse(lst_Digit) + 1).ToString();
                code = type + "-" + yr + "-" + lst_Digit.PadLeft(5, '0');
            }
            else if (type == "TR")
            {

                int ID = obj.T74080.Count() > 0 ? obj.T74080.Where(m => m.T_ENTRY_USER == ad).Max(a => a.T_TRANSFER_REQ_ID) : 0;
                string SALE_NO = ID != 0 ? obj.T74080.Where(m => m.T_TRANSFER_REQ_ID == ID).Select(a => a.T_TRANSFER_REQ_NO).FirstOrDefault() : "TR-" + yr + "-00000";
                string lst_Digit = SALE_NO.Substring(SALE_NO.Length - Math.Min(5, SALE_NO.Length));
                lst_Digit = (Int32.Parse(lst_Digit) + 1).ToString();
                code = type + "-" + yr + "-" + lst_Digit.PadLeft(5, '0');
            }
            return code;

        }
        //public static bool CheckForInternetConnection()
        //{
        //    try
        //    {
        //        using (var client = new WebClient())
        //        {
        //            CommonDAL c = new CommonDAL();
        //            //using (client.OpenRead("http://100.43.0.38:91/"))
        //            //{
        //                return true;
        //          //  }

        //        }
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
    }
}