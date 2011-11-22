// <copyright file="CnDateTest.cs" company="Microsoft">Copyright ?Microsoft 2011</copyright>
using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Src;

namespace Src
{
    /// <summary>This class contains parameterized unit tests for CnDate</summary>
    [PexClass(typeof(CnDate))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestClass]
    public partial class CnDateTest
    {
        /// <summary>Test stub for .ctor(DateTime)</summary>
        [PexMethod]
        public CnDate Constructor(DateTime date)
        {
            CnDate target = new CnDate(date);
            return target;
            // TODO: add assertions to method CnDateTest.Constructor(DateTime)
        }

        [TestMethod]
        public void CnDate01()
        {
            CnDate target = new CnDate(new DateTime(2011,11,22));
            Assert.AreEqual("射手座", target.Constellation);
            Assert.AreEqual("十月", target.Month);
            Assert.AreEqual("廿七", target.Day);
            Assert.AreEqual("辛卯年", target.Year);
            Assert.AreEqual(false, target.IsSolarTerm);
            Assert.AreEqual(null, target.SolarTerm);
            Assert.AreEqual("辛卯年 十月廿七, 星期二", target.ToString());
            target = new CnDate(new DateTime(2011, 11, 23));
            Assert.AreEqual("射手座", target.Constellation);
            Assert.AreEqual("十月", target.Month);
            Assert.AreEqual("廿八", target.Day);
            Assert.AreEqual("辛卯年", target.Year);
            Assert.AreEqual(true, target.IsSolarTerm);
            Assert.AreEqual("小雪", target.SolarTerm);
            Assert.AreEqual("辛卯年 小雪, 星期三", target.ToString("Y S, W"));

        }
    }
}
