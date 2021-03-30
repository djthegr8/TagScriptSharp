using TagScriptSharp.Interface;
namespace TagScriptSharp.Adapter
{
    public class IntAdapter : IAdapter
    {
        public int Integer;
        public IntAdapter(int integer)
        {
            Integer = integer;
        }

        public string GetValue(Verb ctxt)
        {
            return Integer.ToString();
        }
    }
}