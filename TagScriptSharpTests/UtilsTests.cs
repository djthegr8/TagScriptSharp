using TagScriptSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TagScriptSharp.Tests
{
    [TestClass()]
    public class UtilsTests
    {
        [TestMethod("Regex Test")]
        public void EscapeContentTest()
        {
            const string str = "{(weird):poggers|doggers}";
            Assert.AreEqual(Utils.EscapeContent(str), @"\{\(weird\)\:poggers\|doggers\}");
        }
    }
}