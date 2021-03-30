using System;
using TagScriptSharp;
namespace TagScriptSharp.Adapter
{
    public class SafeObjectAdapter
    {
        private object obj;

        public SafeObjectAdapter(object _base)
        {
            obj = _base;
        }
        /// <summary>
        /// NOTE: We return "" instead of nothing, hope thats alright
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public string GetValue(Verb ctx)
        {
            if (ctx.Parameter == null)
            {
                return obj.ToString();
            }
            if (ctx.Parameter.StartsWith("_") || ctx.Parameter.Contains("."))
            {
                return "";
            }

            object attribute;
            try
            {
                attribute = obj.GetAttr(ctx.Parameter);
                if (attribute == null) throw new Exception();
            }
            catch
            {
                return "";
            }

            switch (attribute)
            {
                case Delegate:
                    return "";
                case float:
                    attribute = int.Parse(attribute.ToString());
                    break;
            }
            return attribute.ToString();
        }
    }
}