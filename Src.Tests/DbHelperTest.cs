// <copyright file="DbHelperTest.cs" company="Microsoft">Copyright ?Microsoft 2011</copyright>
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Src;

namespace Src
{
    /// <summary>This class contains parameterized unit tests for DbHelper</summary>
    [PexClass(typeof(DbHelper))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestClass]
    public partial class DbHelperTest
    {
        /// <summary>Test stub for Connect(String)</summary>
        [PexMethod]
        public IDbHelper Connect([PexAssumeUnderTest]DbHelper target, string connectionString)
        {
            IDbHelper result = target.Connect(connectionString);
            return result;
            // TODO: add assertions to method DbHelperTest.Connect(DbHelper, String)
        }

        /// <summary>Test stub for Create()</summary>
        [PexMethod]
        public IDbHelper Create()
        {
            IDbHelper result = DbHelper.Create();
            return result;
            // TODO: add assertions to method DbHelperTest.Create()
        }

        /// <summary>Test stub for Create(String, String)</summary>
        [PexMethod, PexAllowedException(typeof(ArgumentNullException)), PexAllowedException(typeof(ArgumentException))]
        public IDbHelper Create01(string provider, string connectionString)
        {
            IDbHelper result = DbHelper.Create(provider, connectionString);
            return result;
            // TODO: add assertions to method DbHelperTest.Create01(String, String)
        }

        /// <summary>Test stub for Dispose()</summary>
        [PexMethod]
        public void Dispose([PexAssumeUnderTest]DbHelper target)
        {
            target.Dispose();
            // TODO: add assertions to method DbHelperTest.Dispose(DbHelper)
        }

        /// <summary>Test stub for ExecuteCommand(String, Object[])</summary>
        [PexMethod]
        public int ExecuteCommand(
            [PexAssumeUnderTest]DbHelper target,
            string sql,
            object[] values
        )
        {
            int result = target.ExecuteCommand(sql, values);
            return result;
            // TODO: add assertions to method DbHelperTest.ExecuteCommand(DbHelper, String, Object[])
        }

        /// <summary>Test stub for ExecuteReader(String, Object[])</summary>
        [PexMethod]
        public IEnumerable<IDataRecord> ExecuteReader(
            [PexAssumeUnderTest]DbHelper target,
            string sql,
            object[] values
        )
        {
            IEnumerable<IDataRecord> result = target.ExecuteReader(sql, values);
            return result;
            // TODO: add assertions to method DbHelperTest.ExecuteReader(DbHelper, String, Object[])
        }

        /// <summary>Test stub for ExecuteScalar(String, Object[])</summary>
        [PexMethod]
        public object ExecuteScalar(
            [PexAssumeUnderTest]DbHelper target,
            string sql,
            object[] values
        )
        {
            object result = target.ExecuteScalar(sql, values);
            return result;
            // TODO: add assertions to method DbHelperTest.ExecuteScalar(DbHelper, String, Object[])
        }

        /// <summary>Test stub for First(String, Object[])</summary>
        [PexMethod]
        public object[] First(
            [PexAssumeUnderTest]DbHelper target,
            string sql,
            object[] values
        )
        {
            object[] result = target.First(sql, values);
            return result;
            // TODO: add assertions to method DbHelperTest.First(DbHelper, String, Object[])
        }
    }
}
