using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
namespace TagScriptSharp
{
    public static class Utils
    {
        public static readonly Regex PatternRegex = new Regex(@"(?<!\\)([{():|}])");
        public static object GetAttr(this object obj, string name)
        {
            Type type = obj.GetType();
            BindingFlags flags = BindingFlags.Instance |
                                     BindingFlags.Public |
                                     BindingFlags.GetProperty;

            return type.InvokeMember(name, flags, Type.DefaultBinder, obj, null);
        }

        /// <summary>
        /// Escapes content to prevent tampering with Engine behaviour
        /// </summary>
        /// <returns>Escaped content</returns>
        public static string EscapeContent(string str)
        {
            return string.IsNullOrEmpty(str) ? "" : PatternRegex.Replace(str, "\\$&");
        }

        public static Dictionary<T1, T2> Update<T1, T2>(this Dictionary<T1, T2> Dict, Dictionary<T1, T2> Other)
        {
            return new List<Dictionary<T1,T2>>(){Dict, Other}.SelectMany(x => x).ToDictionary(x => x.Key, y => y.Value);
        }
    }
}
