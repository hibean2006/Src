/**
 * file depends: IDbHelper.cs
 *                      IDbHelperEx.cs
 *                      DbConnectionNotFoundException.cs
 *                      DynamicMethodBuilder.cs
 *                      EntityHelper.cs
 * 
 * Haibin Zou=>zhb
 * hibean2006@126.com
 * 2011-12-02 Created 
 * */

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace Src
{
    /// <summary>
    /// <see cref="IDbHelperEx"/>的默认实现
    /// <seealso cref="DbHelper"/>
    /// <seealso cref="IDbHelper"/>
    /// </summary>
    public class DbHelperEx : DbHelper, IDbHelperEx
    {
        #region Create
        /// <summary>
        /// 创建<see cref="IDbHelper"/>对象
        /// <remarks>
        /// 使用<see cref="ConfigurationManager.ConnectionStrings"/>取到第一个数据库连接信息，如果不存在，则抛出异常
        /// </remarks>
        /// </summary>
        public new static IDbHelperEx Create()
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
        public new static IDbHelperEx Create(string provider, string connectionString)
        {
            DbHelperEx dbHelperEx = new DbHelperEx(provider);
            dbHelperEx.Connect(connectionString);
            return dbHelperEx;
        }
        #endregion

        /// <summary>
        /// 初始化<see cref="DbHelperEx"/>的实例
        /// </summary>
        /// <param name="provider"><see cref="DbProviderFactories.GetFactory(string)"/></param>
        protected DbHelperEx(string provider)
            : base(provider)
        {
        }

        #region Implementation of IDbHelperEx

        public T FirstObject<T>(string sql, params object[] values) where T : class, new()
        {
            T result = null;
            ReadFirst(delegate(IDataRecord record)
                          {
                              result = new EntityHelper<T>().ConvertFrom(record);
                          }, sql, values);
            return result ?? new T();
        }

        public T FirstValue<T>(string sql, params object[] values)
        {
            object result = null;
            ReadFirst(delegate(IDataRecord record)
                          {
                              if (record.FieldCount == 0)
                              {
                                  throw new ArgumentException("sql 语句中没有 select 列");
                              }
                              result = Convert.ChangeType(record[0], typeof(T));
                          }, sql, values);
            return (T)result;
        }

        public IEnumerable<T> GetObjects<T>(string sql, params object[] values) where T : class ,new()
        {
            EntityHelper<T> entityHelper = new EntityHelper<T>();
            foreach (var record in ExecuteReader(sql, values))
            {
                yield return entityHelper.ConvertFrom(record);
            }
        }

        public IEnumerable<T> GetValues<T>(string sql, params object[] values)
        {
            foreach (var record in ExecuteReader(sql, values))
            {
                if (record.FieldCount == 0)
                {
                    throw new ArgumentException("sql 语句中没有 select 列");
                }
                yield return (T)Convert.ChangeType(record[0], typeof(T));
            }
        }

        #endregion
    }
}