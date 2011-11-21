
using System;
using System.Collections;
using System.Collections.Generic;

namespace Src
{
    /// <summary>
    /// �����������
    /// </summary>
    public interface IParameterChecker
    {
        /// <summary>
        /// ����Ƿ�Ϊ null
        /// </summary>
        /// <returns><see cref="IMoreCheckAction"/></returns>
        IMoreCheckAction IsNull();

        /// <summary>
        /// �������Ϊ false
        /// </summary>
        /// <returns><see cref="IMoreCheckAction"/></returns>
        IMoreCheckAction IsFalse();

        /// <summary>
        /// �������Ϊ��
        /// </summary>
        /// <returns><see cref="IMoreCheckAction"/></returns>
        IMoreCheckAction IsEmpty();

        /// <summary>
        /// ���������Ŀ��������
        /// </summary>
        /// <typeparamref name="T">ָ���ıȽ�����</typeparamref>
        /// <param name="value">��Ҫ�Ƚϵ�ֵ</param>
        /// <returns><see cref="IMoreCheckAction"/></returns>
        IMoreCheckAction IsEqual<T>(T value) where T : IComparable;

        /// <summary>
        /// ���������Ŀ��������
        /// </summary>
        /// <typeparamref name="T">ָ���ıȽ�����</typeparamref>
        /// <param name="value">��Ҫ�Ƚϵ�ֵ</param>
        /// <returns><see cref="IMoreCheckAction"/></returns>
        IMoreCheckAction IsNotEqual<T>(T value) where T : IComparable;

        /// <summary>
        /// ���С��ָ����ֵ
        /// </summary>
        /// <typeparamref name="T">ָ���ıȽ�����</typeparamref>
        /// <param name="value">ָ����ֵ</param>
        /// <returns><see cref="IMoreCheckAction"/></returns>
        IMoreCheckAction IsLessThan<T>(T value) where T : IComparable;

        /// <summary>
        /// ���С�ڻ����ָ����ֵ
        /// </summary>
        /// <typeparam name="T">ָ���ıȽ�����</typeparam>
        /// <param name="value">ָ����ֵ</param>
        /// <returns><see cref="IMoreCheckAction"/></returns>
        IMoreCheckAction IsLessOrEqualThan<T>(T value) where T : IComparable;

        /// <summary>
        /// �������ָ����ֵ
        /// </summary>
        /// <typeparam name="T">ָ���ıȽ�����</typeparam>
        /// <param name="value">ָ����ֵ</param>
        /// <returns><see cref="IMoreCheckAction"/></returns>
        IMoreCheckAction IsGreaterOrEqualThan<T>(T value) where T : IComparable;

        /// <summary>
        /// ������ڻ����ָ����ֵ
        /// </summary>
        /// <typeparam name="T">ָ���ıȽ�����</typeparam>
        /// <param name="value">ָ����ֵ</param>
        /// <returns><see cref="IMoreCheckAction"/></returns>
        IMoreCheckAction IsGreaterThan<T>(T value) where T : IComparable;


        /// <summary>
        /// �������Ϊ true
        /// </summary>
        /// <returns><see cref="IMoreCheckAction"/></returns>
        IMoreCheckAction IsTrue();

        /// <summary>
        /// �����ָ�����͵�ʵ��
        /// </summary>
        /// <typeparam name="T">ָ��������</typeparam>
        /// <returns><see cref="IMoreCheckAction"/></returns>
        IMoreCheckAction IsNotInstanceOf<T>();

        /// <summary>
        /// ����ڼ�����
        /// </summary>
        /// <param name="enumerable">���϶���</param>
        /// <returns><see cref="IMoreCheckAction"/></returns>
        IMoreCheckAction InArray(IEnumerable enumerable);

        /// <summary>
        /// ����ڼ�����
        /// </summary>
        /// <typeparam name="T">ָ��������</typeparam>
        /// <param name="enumerable">���϶���</param>
        /// <returns><see cref="IMoreCheckAction"/></returns>
        IMoreCheckAction InArray<T>(IEnumerable<T> enumerable);

        /// <summary>
        /// ������ڼ�����
        /// </summary>
        /// <param name="enumerable">���϶���</param>
        /// <returns><see cref="IMoreCheckAction"/></returns>
        IMoreCheckAction NotInArray(IEnumerable enumerable);

        /// <summary>
        /// ������ڼ�����
        /// </summary>
        /// <typeparam name="T">ָ��������</typeparam>
        /// <param name="enumerable">���϶���</param>
        /// <returns><see cref="IMoreCheckAction"/></returns>
        IMoreCheckAction NotInArray<T>(IEnumerable<T> enumerable);
    }
}