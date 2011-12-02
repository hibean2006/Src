/**
 * Haibin Zou=>zhb
 * hibean2006@126.com
 * 2011-11-18 Created 
 * */

using System;
using System.Collections.Generic;
using System.Data;

namespace Src
{
    /// <summary>
    /// 数据库操作类
    /// </summary>
    /// <example>
    /// //create table01(col01,--int
    /// //col02--varchar
    /// //)
    /// using(IDbHelper helper = DbHelper.Create())
    /// {
    ///     int affectRow = helper.ExecuteCommand("insert into table01(col01,col02) values({0},{1})", 20, "test");
    /// //sqlserver: sql=> insert into table01(col01, col02) values(@p0,@p1)
    /// //oracle : sql=> insert into table02(col01, col02) values(:p0, :p1)
    /// }
    /// </example>
    public interface IDbHelper : IDisposable
    {
        /// <summary>
        /// 连接至数据库
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        IDbHelper Connect(string connectionString);

        /// <summary>
        /// 执行命令
        /// <seealso cref="IDbCommand.ExecuteNonQuery"/>
        /// </summary>
        /// <param name="sql">sql语句，使用{0}占位，见<see cref="IDbHelper"/>示例</param>
        /// <param name="values">参数值</param>
        /// <returns>返回影响的行数</returns>
        int ExecuteCommand(string sql, params object[] values);

        /// <summary>
        /// 执行读取命令
        /// <seealso cref="IDbCommand.ExecuteReader()"/>
        /// </summary>
        /// <param name="sql">sql语句，使用{0}占位，见<see cref="IDbHelper"/>示例</param>
        /// <param name="values">参数值</param>
        /// <returns>逐行返回数据</returns>
        IEnumerable<IDataRecord> ExecuteReader(string sql, params object[] values);

        /// <summary>
        /// 读取第一行数据
        /// </summary>
        /// <param name="sql">sql语句，使用{0}占位，见<see cref="IDbHelper"/>示例</param>
        /// <param name="values">参数值</param>
        /// <returns>当存在数据时，返回第一行，否则返回<c>null</c></returns>
        object[] First(string sql, params object[] values);

        /// <summary>
        /// 读取第一行记录
        /// </summary>
        /// <param name="readAction">读取动作</param>
        /// <param name="sql">sql语句，使用{0}占位，见<see cref="IDbHelper"/>示例</param>
        /// <param name="values">参数值</param>
        void ReadFirst(Action<IDataRecord> readAction, string sql, params object[] values);

        /// <summary>
        /// 执行读取首条记录的第一项
        /// <seealso cref="IDbCommand.ExecuteScalar"/>
        /// </summary>
        /// <param name="sql">sql语句，使用{0}占位，见<see cref="IDbHelper"/>示例</param>
        /// <param name="values">参数值</param>
        /// <returns>首条记录的第一项</returns>
        object ExecuteScalar(string sql, params object[] values);

        /// <summary>
        /// 开始事务
        /// </summary>
        /// <returns><see cref="IDbConnection.BeginTransaction()"/></returns>
        void BeginTransaction();

        /// <summary>
        /// 提交事务
        /// </summary>
        /// <returns><see cref="IDbTransaction.Commit()"/></returns>
        void Commit();

        /// <summary>
        /// 回滚事务
        /// </summary>
        /// <returns><see cref="IDbTransaction.Rollback()"/></returns>
        void Rollback();


        /// <summary>
        /// 是否Oracle数据库
        /// </summary>
        bool IsOracle { get; }

        /// <summary>
        /// 开始批处理（Sql语句相同，但是参数值不同）
        /// </summary>
        /// <param name="sql">sql语句，使用{0}占位，见<see cref="IDbHelper"/>示例</param>
        /// <param name="parameterCount">参数的个数</param>
        /// <returns><see cref="IBatchCommand"/>的实例</returns>
        IBatchCommand BeginBatch(string sql, int parameterCount);
    }

    /// <summary>
    /// 批量执行同一命令
    /// </summary>
    public interface IBatchCommand : IDisposable
    {
        /// <summary>
        /// 执行语句<see cref="IDbCommand.ExecuteNonQuery"/>
        /// </summary>
        /// <returns>影响的行数</returns>
        int Execute(params object[] values);
    }
}