/**
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
        private DbHelper(string provider)
        {
            this.provider = provider;
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

        public object ExecuteScalar(string sql, params object[] values)
        {
            using (var cmd = Prepare(sql, values))
            {
                return cmd.ExecuteScalar();
            }
        }

        #endregion

        #region Implementation of IDisposable

        public void Dispose()
        {
            if (connection != null)
            {
                connection.Dispose();
            }
        }

        #endregion

        #region Prepare DbCommand
        private IDbCommand Prepare(string sql, object[] values)
        {
            var cmd = connection.CreateCommand();
            if (values != null && values.Length > 0)
            {
                var names = new string[values.Length];
                for (int i = 0; i < values.Length; i++)
                {
                    names[i] = CreateParameterName(i);
                }
                var parameters = new IDbDataParameter[values.Length];
                for (int i = 0; i < values.Length; i++)
                {
                    parameters[i] = cmd.CreateParameter();
                    parameters[i].ParameterName = names[i];
                    parameters[i].Value = values[i] ?? DBNull.Value;
                }
                cmd.CommandText = String.Format(sql, names);
                for (int i = 0; i < parameters.Length; i++)
                {
                    cmd.Parameters.Add(parameters[i]);
                }
            }
            else
            {
                cmd.CommandText = sql;
            }
            //Open Connection
            if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
            {
                if (string.IsNullOrEmpty(connection.ConnectionString))
                {
                    connection.ConnectionString = ConfigurationManager.ConnectionStrings[0].ConnectionString;
                }
                connection.Open();
            }

            return cmd;
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