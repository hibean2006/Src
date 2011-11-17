// <copyright file="FileHelperTest.cs" company="Microsoft">Copyright ?Microsoft 2011</copyright>
using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Src;

namespace Src
{
    /// <summary>This class contains parameterized unit tests for FileHelper</summary>
    [PexClass(typeof(FileHelper))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestClass]
    public partial class FileHelperTest
    {
        /// <summary>Test stub for EnsureDirectory(String)</summary>
        [PexMethod, PexAllowedException(typeof(ArgumentException))]
        public void EnsureDirectory(string path)
        {
            FileHelper.EnsureDirectory(path);
            // TODO: add assertions to method FileHelperTest.EnsureDirectory(String)
        }

        /// <summary>Test stub for GetPhysicalPath(String)</summary>
        [PexMethod, PexAllowedException(typeof(ArgumentException))]
        public string GetPhysicalPath(string path)
        {
            string result = FileHelper.GetPhysicalPath(path);
            return result;
            // TODO: add assertions to method FileHelperTest.GetPhysicalPath(String)
        }
    }
}
