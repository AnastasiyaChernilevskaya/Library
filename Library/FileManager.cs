using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Library.FileManager
{
    public class FileManager
    {
        public static class JsonSerializeHelper
        {
            public static string Serialize<T>(T model)
            {
                if (model == null)
                {
                    return string.Empty;
                }
                var result = new JavaScriptSerializer().Serialize(model);
                return result;
            }

            public static T Serialize<T>(string json)
            {
                if (string.IsNullOrEmpty(json))
                {
                    return default(T);
                }
                var result = new JavaScriptSerializer().Deserialize<T>(json);
                return result;
            }
        }
    }
}