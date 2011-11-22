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
            Assert.AreEqual("������", target.Constellation);
            Assert.AreEqual("ʮ��", target.Month);
            Assert.AreEqual("إ��", target.Day);
            Assert.AreEqual("��î��", target.Year);
            Assert.AreEqual(false, target.IsSolarTerm);
            Assert.AreEqual(null, target.SolarTerm);
            Assert.AreEqual("��î�� ʮ��إ��, ���ڶ�", target.ToString());
            target = new CnDate(new DateTime(2011, 11, 23));
            Assert.AreEqual("������", target.Constellation);
            Assert.AreEqual("ʮ��", target.Month);
            Assert.AreEqual("إ��", target.Day);
            Assert.AreEqual("��î��", target.Year);
            Assert.AreEqual(true, target.IsSolarTerm);
            Assert.AreEqual("Сѩ", target.SolarTerm);
            Assert.AreEqual("��î�� Сѩ, ������", target.ToString("Y S, W"));

        }
    }
}
