using System;
using System.Linq;

namespace TagScriptSharp
{
    /// <summary>
    /// A class representing the verb string broken down into a clean format for consumption
    /// </summary>
    public class Verb
    {
        public string Declaration = null;
        public string Parameter = null;
        public string Payload = null;
        private string ParsedString = "";
        private int decDepth, decStart;
        public Verb(string verbString = null, int limit = 2000)
        {
            if (verbString == null) return;
            ParsedString = verbString.Remove(0, 1).Remove(verbString.Length - 1, 1);
            decDepth = decStart = 0;
            var skipNext = false;
            var ttk = string.Join("",ParsedString.Take(limit));
            for (var i = 0; i < ttk.Length; i++)
            {
                if (skipNext)
                {
                    skipNext = false;
                    continue;
                }
                var character = ttk[i];
                switch (character)
                {
                    case '\\':
                        skipNext = true;
                        continue;
                    case ':' when decDepth == 0:
                    {
                        setPayload();
                        return;
                    }
                    case '(':
                        openParameter(i);
                        break;
                    case ')' when decDepth != 0:
                        if (closeParameter(i)) return;
                        break;
                    default:
                        setPayload();
                        break;
                }
            }
            
        }
        public void setPayload()
        {
            var res = ParsedString.Split(':');
            if (res.Length != 2) return;
            Payload = res[1];
            Declaration = res[0];
        }

        public void openParameter(int i)
        {
            decDepth += 1;
            if (decStart != 0) return;
            decStart = i;
            Declaration = string.Join("", ParsedString.Skip(i));
        }

        public bool closeParameter(int i)
        {
            decDepth -= 1;
            if (decDepth != 0) return false;
            Parameter = string.Join("",ParsedString.Skip(decStart).Take(i - decStart));
            try
            {
                if (ParsedString[i + 1] == ':')
                {
                    Payload = string.Join("", ParsedString.Skip(i + 2));
                }
            }
            catch
            {
                // Haha suppressed
            }

            return true;
        }
        /// <summary>
        /// Makes it compatible with `ToString()` 
        /// </summary>
        /// <returns>The compiled form of the tag</returns>
        public override string ToString()
        {
            var response = "{";
            if (Declaration != null) response += Declaration;
            if (Parameter != null) response += $"({Parameter})";
            if (Payload != null) response += $":{Payload}";
            return $"{response}}}";
        }
    }
}
