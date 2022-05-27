using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;

namespace YSKProje.ToDo.Web.CustomExtensions
{
    public static class SessionExtension
    {
        public static void SetObject(this ISession session, string key, object deger)
        {
            string gelenData = JsonConvert.SerializeObject(deger);
            session.SetString(key,gelenData);
        }

        public static T GetObject<T>(this ISession session, string key) where T : class,new()
        {
            string gelenData = session.GetString(key);
            if (gelenData != null)
            {
                return JsonConvert.DeserializeObject<T>(gelenData);
            }
                
            return null;
        }
    }
    
}
