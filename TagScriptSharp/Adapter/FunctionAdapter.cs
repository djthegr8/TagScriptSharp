using System;
using System.ComponentModel;
using TagScriptSharp;
namespace TagScriptSharp.Adapter
{
    public class FunctionAdapter
    {
        public Delegate fn;

        public FunctionAdapter(Delegate _fn)
        {
            fn = _fn;
        }

        public string GetValue()
        {
            return fn.DynamicInvoke().ToString();
        }
    }
}