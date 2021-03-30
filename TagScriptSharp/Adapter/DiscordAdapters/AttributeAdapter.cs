using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TagScriptSharp.Interface;
namespace TagScriptSharp.Adapter.DiscordAdapters
{
    public class AttributeAdapter : IAdapter
    {
        public dynamic _base;
        public Dictionary<string,object> Attributes;
        public AttributeAdapter(dynamic __base)
        {
            _base = __base;
            Attributes = new Dictionary<string, object>()
            { 
                {"id", _base.Id},
                {"created_at", _base.CreatedAt},
                {"timestamp", Convert.ToInt32(_base.CreatedAt.TimeStamp)},
                {"name", _base.GetAttr("name") == null ? _base.ToString() : _base.GetAttr("name")}
            };

        }
        public AttributeAdapter() { }
        public void UpdateAttributes()
        {
            
        }

        public string GetValue(Verb ctx)
        {
            string return_value = "";
            if (string.IsNullOrEmpty(ctx.Parameter)) return_value = _base.ToString();
            else
            {
                var param = Attributes[ctx.Parameter];
                return_value = param?.ToString();
            }

            return return_value == null ? null : Utils.EscapeContent(return_value);
        }
    }
}