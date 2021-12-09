using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ambucare.Models
{
    public class TRequest
    {
        public string sp { get; set; }

        // public Dictionary<string,string> dict { get; set; }
        public List<TParam> dict { get; set; }

        public List<List<TParam>> dictList { get; set; }
        public string Base64 { get; set; }
        public string extnsn { get; set; }
        public string db { get; set; }

        //---------------LatLong-------------------
        public string orLat { get; set; }
        public string orLng { get; set; }
        public string dsLat { get; set; }
        public string dsLng { get; set; }
    }
    public class TParam
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public class TResult
    {
        public string ID { get; set; }
        public System.Data.DataTable data { get; set; }

        public TResult(System.Data.DataTable dt)
        {
            data = dt;
        }
    }
}