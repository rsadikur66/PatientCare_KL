using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;

namespace AmbucareDAL.Models
{
    public static class CommonHelper
    {
        public static List<T> convertJsonToObject<T>(object jsonData)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<T>));
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonData.ToString()));
            return ((List<T>)serializer.ReadObject(stream));
        }
    }
}