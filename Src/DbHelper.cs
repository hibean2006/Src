/**
 * file depends: IDbHelper.cs 
 *                      DbConnectionNotFoundException.cs
 * 
 * Haibin Zou=>zhb
 * hibean2006@126.com
 * 2011-11-18 Created 
 * */
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace Src
{
    /// <summary>
    /// <see cref="IDbHelper"/>的默认实现
    /// </summary>
    public class DbHelper : IDbHelper
    {
        #region Database Types

        /// <summary>
        /// Sql Server 数据库
        /// <example>
        /// string connection = "Data Source=.;Initial Catalog=dbname;Integrated Security=True";
        /// </example>
        /// </summary>
        public const string SqlServer = "System.Data.SqlClient";

        /// <summary>
        /// Oracle 数据库
        /// <example> 
        /// string connection = "data source=server:1521/orcl;user id=user;password=pwd";
        /// </example>
        /// </summary>
        public const string Oracle = "System.Data.OracleClient";

        /// <summary>
        /// Odbc 
        /// </summary>
        public const string Odbc = "System.Data.Odbc";

        /// <summary>
        /// OleDb
        /// </summary>
        public const string OleDb = "System.Data.OleDb";

        /// <summary>
        /// Sql Server 在移动终端上的版本
        /// </summary>
        public const string SqlCe = "System.Data.SqlServerCe.3.5";

        /// <summary>
        /// Sqlite 
        /// </summary>
        /// <example>
        /// string connection = "Data Source=c:\\file.sqlite;password=pwd";
        /// </example>
        public const string Sqlite = "System.Data.SQLite";

        /// <summary>
        /// MySql
        /// </summary>
        /// <example>
        /// string connection = "Database=dbname;Server=localhost;Password=pwd;User Id=root";
        /// </example>
        public const string MySql = "System.Data.MySqlClient";

        #endregion

        private readonly IDbConnection connection;
        private IDbTransaction transaction;
        private int tranDept = 0;
        private readonly string provider;

        /// <summary>
        /// 创建<see cref="IDbHelper"/>对象
        /// <remarks>
        /// 使用<see cref="ConfigurationManager.ConnectionStrings"/>取到第一个数据库连接信息，如果不存在，则抛出异常
        /// </remarks>
        /// </summary>
        public static IDbHelper Create()
        {
            if (ConfigurationManager.ConnectionStrings.Count == 0)
            {
                throw new DbConnectionNotFoundException();
            }
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[0];
            return Create(settings.ProviderName, settings.ConnectionString);
        }

        /// <summary>
        /// 创建<see cref="IDbHelper"/>的实例
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        /// <example>
        /// string connection = "Data Source=.;Initial Catalog=dbname;Integrated Security=True";
        /// using(IDbHelper helper = DbHelper.Create(DbHelper.SqlServer, connection)
        /// {
        ///     //...
        /// }
        /// </example>
        public static IDbHelper Create(string provider, string connectionString)
        {
            return new DbHelper(provider).Connect(connectionString);
        }

        /// <summary>
        /// 初始化<see cref="DbHelper"/>的实例
        /// </summary>
        /// <param name="provider"><see cref="DbProviderFactories.GetFactory(string)"/></param>
        protected DbHelper(string provider)
        {
            this.provider = provider;
            IsOracle = provider.Contains("Oracle");
            connection = DbProviderFactories.GetFactory(provider).CreateConnection();
        }

        #region Implementation of IDbHelper

        public IDbHelper Connect(string connectionString)
        {
            connection.ConnectionString = connectionString;
            return this;
        }

        public int ExecuteCommand(string sql, params object[] values)
        {
            using (var cmd = Prepare(sql, values))
            {
                return cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<IDataRecord> ExecuteReader(string sql, params object[] values)
        {
            using (var cmd = Prepare(sql, values))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        yield return reader;
                    }
                }
            }
        }

        public object[] First(string sql, params object[] values)
        {
            using (var cmd = Prepare(sql, values))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var result = new object[reader.FieldCount];
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            result[i] = reader[i];
                        }
                        return result;
                    }
                    return null;
                }
            }
        }

        public void ReadFirst(Action<IDataRecord> readAction, string sql, params object[] values)
        {
            using (var cmd = Prepare(sql, values))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read() && readAction != null)
                    {
                        readAction(reader);
                    }
                }
            }
        }

        public object ExecuteScalar(string sql, params object[] values)
        {
            using (var cmd = Prepare(sql, values))
            {
                return cmd.ExecuteScalar();
            }
        }

        public void BeginTransaction()
        {
            OpenConnection();
            transaction = transaction ?? connection.BeginTransaction();
            tranDept++;//支持嵌套Transaction
        }

        public void Commit()
        {
            if (transaction == null) throw new ApplicationException("并没有未开始事务，请调用 BeginTransaction");
            tranDept--;
            if (tranDept > 0) return;
            //提交时，查找嵌套的深度
            transaction.Commit();
            transaction.Dispose();
            transaction = null;
        }

        public void Rollback()
        {
            if (transaction == null) throw new ApplicationException("并没有未开始事务，请调用 BeginTransaction");
            tranDept = 0;
            //回滚时，立即全部回滚
            transaction.Rollback();
            transaction.Dispose();
            transaction = null;
        }

        public bool IsOracle
        {
            get;
            private set;
        }

        public IBatchCommand BeginBatch(string sql, int parameterCount)
        {
            var cmd = Prepare(sql, parameterCount);
            cmd.Prepare();
            return new BatchCommand(cmd);
        }

        private class BatchCommand : IBatchCommand
        {
            private readonly IDbCommand cmd;

            public BatchCommand(IDbCommand cmd)
            {
                this.cmd = cmd;
            }

            #region Implementation of IDisposable

            public void Dispose()
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }
            }

            public int Execute(params object[] values)
            {
                SetParameterValue(values, cmd);
                return cmd.ExecuteNonQuery();
            }

            #endregion
        }

        #endregion

        #region Implementation of IDisposable

        public void Dispose()
        {
            if (transaction != null)
            {
                transaction.Rollback();
                transaction.Dispose();
                transaction = null;
            }
            if (connection != null)
            {
                connection.Dispose();
            }
        }

        #endregion

        #region Prepare DbCommand
        private IDbCommand Prepare(string sql, int count)
        {
            var cmd = connection.CreateCommand();
            cmd.Transaction = transaction;
            if (count > 0)
            {
                var names = new string[count];
                for (int i = 0; i < count; i++)
                {
                    names[i] = CreateParameterName(i);
                    var parameter = cmd.CreateParameter();
                    parameter.ParameterName = names[i];
                    cmd.Parameters.Add(parameter);
                }
                cmd.CommandText = String.Format(sql, names);
            }
            else
            {
                cmd.CommandText = sql;
            }
            OpenConnection();

            return cmd;
        }

        private void OpenConnection()
        {
            if (connection.State != ConnectionState.Closed && connection.State != ConnectionState.Broken) return;
            if (string.IsNullOrEmpty(connection.ConnectionString))
            {
                connection.ConnectionString = ConfigurationManager.ConnectionStrings[0].ConnectionString;
            }
            connection.Open();
        }

        private IDbCommand Prepare(string sql, object[] values)
        {
            var cmd = Prepare(sql, values != null ? values.Length : -1);
            SetParameterValue(values, cmd);
            return cmd;
        }

        private static void SetParameterValue(object[] values, IDbCommand cmd)
        {
            if (cmd == null) throw new ArgumentNullException("cmd");
            if (cmd.Parameters.Count == 0) return;
            if (values == null) throw new ArgumentNullException("values");
            if (values.Length != cmd.Parameters.Count)
            {
                throw new ArgumentException("values 数组长度与 cmd 中参数个数应保持一致");
            }
            for (int i = 0; i < values.Length; i++)
            {
                var dbParameter = (DbParameter)cmd.Parameters[i];
                dbParameter.Value = values[i];
            }
        }

        private string CreateParameterName(int position)
        {
            switch (provider)
            {
                case SqlServer:
                    return string.Format("@p{0}", position);
                case Oracle:
                    return string.Format(":p{0}", position);
                case Sqlite:
                    return string.Format("@p{0}", position);
                case Odbc:
                    return "?";
                case OleDb:
                    return "?";
                case SqlCe:
                    return string.Format("@p{0}", position);
                case MySql:
                    return string.Format("?p{0}", position);
            }
            throw new NotSupportedException("DbHelper 尚不支持该数据库");
        }
        #endregion
    }


}