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
    /// ���ݿ������
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
        /// ���������ݿ�
        /// </summary>
        /// <param name="connectionString">�����ַ���</param>
        IDbHelper Connect(string connectionString);

        /// <summary>
        /// ִ������
        /// <seealso cref="IDbCommand.ExecuteNonQuery"/>
        /// </summary>
        /// <param name="sql">sql��䣬ʹ��{0}ռλ����<see cref="IDbHelper"/>ʾ��</param>
        /// <param name="values">����ֵ</param>
        /// <returns>����Ӱ�������</returns>
        int ExecuteCommand(string sql, params object[] values);

        /// <summary>
        /// ִ�ж�ȡ����
        /// <seealso cref="IDbCommand.ExecuteReader()"/>
        /// </summary>
        /// <param name="sql">sql��䣬ʹ��{0}ռλ����<see cref="IDbHelper"/>ʾ��</param>
        /// <param name="values">����ֵ</param>
        /// <returns>���з�������</returns>
        IEnumerable<IDataRecord> ExecuteReader(string sql, params object[] values);

        /// <summary>
        /// ��ȡ��һ������
        /// </summary>
        /// <param name="sql">sql��䣬ʹ��{0}ռλ����<see cref="IDbHelper"/>ʾ��</param>
        /// <param name="values">����ֵ</param>
        /// <returns>����������ʱ�����ص�һ�У����򷵻�<c>null</c></returns>
        object[] First(string sql, params object[] values);

        /// <summary>
        /// ��ȡ��һ�м�¼
        /// </summary>
        /// <param name="readAction">��ȡ����</param>
        /// <param name="sql">sql��䣬ʹ��{0}ռλ����<see cref="IDbHelper"/>ʾ��</param>
        /// <param name="values">����ֵ</param>
        void ReadFirst(Action<IDataRecord> readAction, string sql, params object[] values);

        /// <summary>
        /// ִ�ж�ȡ������¼�ĵ�һ��
        /// <seealso cref="IDbCommand.ExecuteScalar"/>
        /// </summary>
        /// <param name="sql">sql��䣬ʹ��{0}ռλ����<see cref="IDbHelper"/>ʾ��</param>
        /// <param name="values">����ֵ</param>
        /// <returns>������¼�ĵ�һ��</returns>
        object ExecuteScalar(string sql, params object[] values);

        /// <summary>
        /// ��ʼ����
        /// </summary>
        /// <returns><see cref="IDbConnection.BeginTransaction()"/></returns>
        void BeginTransaction();

        /// <summary>
        /// �ύ����
        /// </summary>
        /// <returns><see cref="IDbTransaction.Commit()"/></returns>
        void Commit();

        /// <summary>
        /// �ع�����
        /// </summary>
        /// <returns><see cref="IDbTransaction.Rollback()"/></returns>
        void Rollback();


        /// <summary>
        /// �Ƿ�Oracle���ݿ�
        /// </summary>
        bool IsOracle { get; }

        /// <summary>
        /// ��ʼ������Sql�����ͬ�����ǲ���ֵ��ͬ��
        /// </summary>
        /// <param name="sql">sql��䣬ʹ��{0}ռλ����<see cref="IDbHelper"/>ʾ��</param>
        /// <param name="parameterCount">�����ĸ���</param>
        /// <returns><see cref="IBatchCommand"/>��ʵ��</returns>
        IBatchCommand BeginBatch(string sql, int parameterCount);
    }

    /// <summary>
    /// ����ִ��ͬһ����
    /// </summary>
    public interface IBatchCommand : IDisposable
    {
        /// <summary>
        /// ִ�����<see cref="IDbCommand.ExecuteNonQuery"/>
        /// </summary>
        /// <returns>Ӱ�������</returns>
        int Execute(params object[] values);
    }
}