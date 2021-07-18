using System;
using System.Collections.Generic;
using System.Reflection;

namespace TR2RandoTracker.Utils
{
    public static class Extensions
    {
        public static Dictionary<string, object> ToDictionary(this object obj)
        {
            Type t = obj.GetType();
            PropertyInfo[] props = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            Dictionary<string, object> dict = new Dictionary<string, object>();
            foreach (PropertyInfo prop in props)
            {
                object value = prop.GetValue(obj, new object[] { });
                dict.Add(prop.Name, value);
            }
            return dict;
        }

        public static void SetFromDictionary(this object obj, Dictionary<string, object> dict)
        {
            Type t = obj.GetType();
            foreach (string prop in dict.Keys)
            {
                PropertyInfo pi = t.GetProperty(prop);
                if (pi != null && pi.CanWrite)
                {
                    pi.SetValue(obj, dict[prop], null);
                }
            }
        }
    }
}