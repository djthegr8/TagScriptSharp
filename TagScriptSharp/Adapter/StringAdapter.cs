using System;
using System.Linq;
using TagScriptSharp.Interface;
namespace TagScriptSharp.Adapter
{
    public class StringAdapter : IAdapter
    {
        public string Str;
        public bool EscapeContent;
        public StringAdapter(string str, bool escapeContent = false)
        {
            Str = str;
            EscapeContent = escapeContent;
        }

        /// <summary>
        /// Returns value escaped if <see cref="EscapeContent"/> is true.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string ReturnValue(string str)
        {
            return EscapeContent ? Utils.EscapeContent(str) : str;
        }

        public string HandleCtxt(Verb ctx)
        {
            if (string.IsNullOrEmpty(ctx.Parameter))
            {
                return Str;
            }

            try
            {
                var splitter = string.IsNullOrEmpty(ctx.Payload) ? " " : ctx.Payload;
                var split = Str.Split(splitter);
                if (!ctx.Parameter.Contains("+"))
                {
                    var index = int.Parse(ctx.Parameter) - 1;
                    return split[index];
                }
                else
                {
                    var index = int.Parse(ctx.Parameter.Replace("+", "")) - 1;
                    var joined = string.Join(splitter, split);
                    if (ctx.Parameter.StartsWith("+")) return string.Join("", joined.Take(index + 1));
                    return ctx.Parameter.EndsWith("+") ? string.Join("", joined.Skip(2)) : split[index];
                }

            }
            catch
            {
                return Str;
            }
        }

        public string GetValue(Verb ctxt)
        {
            return ReturnValue(HandleCtxt(ctxt));
        }
    }
}