using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;

namespace Ambucare.Models
{
    public static class APILink
    {
        public static string apiLink = ConfigurationManager.ConnectionStrings["apiLink"].ConnectionString;
        public static string webSer = ConfigurationManager.ConnectionStrings["webSer"].ConnectionString;

        public static string Plink = "http://" + apiLink;
        //public static string PwebSer = "https://" + webSer;
        

        //Original
         public static string link = Plink + "/MainApi/";
        //LocalHost
        //public static string link = "http://localhost:55530/MainApi/";


        //public static string link = "http://202.40.189.18:82/MainApi/";
        //public static string link = "http://176.241.191.36:82/MainApi/";
        //public static string link = "http://100.43.0.41:83/MainApi/";


        public static string GetDataNoParam = link + "GetData";
        public static string GetDataParam = link + "GetDataSqlParam";
        public static string SaveDataSmallData = link + "SaveData";
        public static string SaveDataListData = link + "SaveDataList";




        public static string GetDistance = link + "GetDistance";



    }
}