// <copyright file="StringHelperTest.cs" company="Microsoft">Copyright ?Microsoft 2011</copyright>
using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Src;
using Microsoft.Pex.Framework.Generated;

namespace Src
{
    /// <summary>This class contains parameterized unit tests for StringHelper</summary>
    [PexClass(typeof(StringHelper))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestClass]
    public partial class StringHelperTest
    {
        /// <summary>Test stub for Between(String, String, String)</summary>
        [PexMethod]
        public string Between(
            string source,
            string left,
            string right
        )
        {
            string result = StringHelper.Between(source, left, right);
            return result;
            // TODO: add assertions to method StringHelperTest.Between(String, String, String)
        }
        [TestMethod]
        public void Between0()
        {
            string s;
            s = this.Between("abc", "a", "c");
            Assert.AreEqual<string>("b", s);
        }

        [TestMethod]
        public void Between1()
        {
            string s;
            s = this.Between("abc", "", "c");
            Assert.AreEqual<string>("ab", s);
        }
        [TestMethod]
        public void Between2()
        {
            string s;
            s = this.Between("abc", "", "");
            Assert.AreEqual<string>("", s);
        }
        [TestMethod]
        public void Between3()
        {
            string s;
            s = this.Between("abca", "b", "a");
            Assert.AreEqual<string>("c", s);
        }

        [TestMethod]
        public void Between7()
        {
            string s;
            s = this.Between("\u0200\0", "\u0200", "\0");
            Assert.AreEqual<string>("", s);
        }
        [TestMethod]
        public void Between289()
        {
            string s;
            s = this.Between("\u0200", "\u0200", "\0");
            Assert.AreEqual<string>("", s);
        }
        [TestMethod]
        public void Between456()
        {
            string s;
            s = this.Between("\0", "\u0001", "\0");
            Assert.AreEqual<string>("", s);
        }
        [TestMethod]
        public void Between941()
        {
            string s;
            s = this.Between((string)null, "", "");
            Assert.AreEqual<string>((string)null, s);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void BetweenThrowsArgumentNullException583()
        {
            string s;
            s = this.Between((string)null, (string)null, (string)null);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void BetweenThrowsArgumentNullException220()
        {
            string s;
            s = this.Between("", "\0", (string)null);
        }
    }
}
