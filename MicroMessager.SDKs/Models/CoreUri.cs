using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace MicroMessager.SDKs.Models
{
    public class CoreUri
    {
        public string Address { get; set; }
        public Dictionary<string, string> Params { get; set; } = new Dictionary<string, string>();
        public CoreUri(string address)
        {
            Address = address;
        }
        public CoreUri(string address, object param) : this(address)
        {
            var t = param.GetType();
            foreach (var prop in t.GetProperties())
            {
                if (prop.GetValue(param) != null)
                {
                    var propName = prop.Name;
                    var propValue = prop.GetValue(param).ToString();
                    var fromQuery = prop.GetCustomAttributes(typeof(FromQueryAttribute), true).FirstOrDefault();
                    if (fromQuery != null)
                    {
                        propName = (fromQuery as FromQueryAttribute).Name;
                    }
                    if (prop == typeof(DateTime))
                    {
                        var nullableValue = prop.GetValue(param) as DateTime?;
                        if (nullableValue != null)
                        {
                            propValue = nullableValue.Value.ToString("o", CultureInfo.InvariantCulture);
                        }
                    }
                    Params.Add(propName, propValue);
                }
            }
        }
        public CoreUri(string host, string path, object param) : this(host + path, param) { }
        public CoreUri(string host, string controllerName, string actionName, object param) : this(host, $"/{WebUtility.UrlEncode(controllerName)}/{WebUtility.UrlEncode(actionName)}", param) { }
        public override string ToString()
        {
            string appendPart = "?";
            foreach (var param in this.Params)
            {
                appendPart += param.Key.ToLower() + "=" + WebUtility.UrlEncode(param.Value) + "&";
            }
            return this.Address + appendPart.TrimEnd('?', '&');
        }
    }
}