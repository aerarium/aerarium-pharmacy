using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace AerariumTech.Pharmacy.App.Extensions
{
    public static class CookieExtensions
    {
        public static void Set<T>(this IResponseCookies cookies, string key, T value)
        {
            cookies.Append(key, JsonConvert.SerializeObject(value), new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddMinutes(300)
            });
        }

        public static T Set<T>(this IRequestCookieCollection cookies, string key, T value)
        {
            cookies.Append(new KeyValuePair<string, string>(key, JsonConvert.SerializeObject(value)));
            return value;
        }

        public static T Get<T>(this IRequestCookieCollection cookies, string key)
        {
            return cookies.TryGetValue(key, out var value) ? JsonConvert.DeserializeObject<T>(value) : default(T);
        }
    }
}