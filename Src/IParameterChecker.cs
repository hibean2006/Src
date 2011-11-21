
using System;
using System.Collections;
using System.Collections.Generic;

namespace Src
{
    /// <summary>
    /// 参数检查内容
    /// </summary>
    public interface IParameterChecker
    {
        /// <summary>
        /// 检查是否为 null
        /// </summary>
        /// <returns><see cref="IMoreCheckAction"/></returns>
        IMoreCheckAction IsNull();

        /// <summary>
        /// 如果条件为 false
        /// </summary>
        /// <returns><see cref="IMoreCheckAction"/></returns>
        IMoreCheckAction IsFalse();

        /// <summary>
        /// 如果对象为空
        /// </summary>
        /// <returns><see cref="IMoreCheckAction"/></returns>
        IMoreCheckAction IsEmpty();

        /// <summary>
        /// 如果对象与目标对象相等
        /// </summary>
        /// <typeparamref name="T">指定的比较类型</typeparamref>
        /// <param name="value">需要比较的值</param>
        /// <returns><see cref="IMoreCheckAction"/></returns>
        IMoreCheckAction IsEqual<T>(T value) where T : IComparable;

        /// <summary>
        /// 如果对象与目标对象不相等
        /// </summary>
        /// <typeparamref name="T">指定的比较类型</typeparamref>
        /// <param name="value">需要比较的值</param>
        /// <returns><see cref="IMoreCheckAction"/></returns>
        IMoreCheckAction IsNotEqual<T>(T value) where T : IComparable;

        /// <summary>
        /// 如果小于指定的值
        /// </summary>
        /// <typeparamref name="T">指定的比较类型</typeparamref>
        /// <param name="value">指定的值</param>
        /// <returns><see cref="IMoreCheckAction"/></returns>
        IMoreCheckAction IsLessThan<T>(T value) where T : IComparable;

        /// <summary>
        /// 如果小于或等于指定的值
        /// </summary>
        /// <typeparam name="T">指定的比较类型</typeparam>
        /// <param name="value">指定的值</param>
        /// <returns><see cref="IMoreCheckAction"/></returns>
        IMoreCheckAction IsLessOrEqualThan<T>(T value) where T : IComparable;

        /// <summary>
        /// 如果大于指定的值
        /// </summary>
        /// <typeparam name="T">指定的比较类型</typeparam>
        /// <param name="value">指定的值</param>
        /// <returns><see cref="IMoreCheckAction"/></returns>
        IMoreCheckAction IsGreaterOrEqualThan<T>(T value) where T : IComparable;

        /// <summary>
        /// 如果大于或等于指定的值
        /// </summary>
        /// <typeparam name="T">指定的比较类型</typeparam>
        /// <param name="value">指定的值</param>
        /// <returns><see cref="IMoreCheckAction"/></returns>
        IMoreCheckAction IsGreaterThan<T>(T value) where T : IComparable;


        /// <summary>
        /// 如果条件为 true
        /// </summary>
        /// <returns><see cref="IMoreCheckAction"/></returns>
        IMoreCheckAction IsTrue();

        /// <summary>
        /// 如果是指定类型的实例
        /// </summary>
        /// <typeparam name="T">指定的类型</typeparam>
        /// <returns><see cref="IMoreCheckAction"/></returns>
        IMoreCheckAction IsNotInstanceOf<T>();

        /// <summary>
        /// 如果在集合中
        /// </summary>
        /// <param name="enumerable">集合对象</param>
        /// <returns><see cref="IMoreCheckAction"/></returns>
        IMoreCheckAction InArray(IEnumerable enumerable);

        /// <summary>
        /// 如果在集合中
        /// </summary>
        /// <typeparam name="T">指定的类型</typeparam>
        /// <param name="enumerable">集合对象</param>
        /// <returns><see cref="IMoreCheckAction"/></returns>
        IMoreCheckAction InArray<T>(IEnumerable<T> enumerable);

        /// <summary>
        /// 如果不在集合中
        /// </summary>
        /// <param name="enumerable">集合对象</param>
        /// <returns><see cref="IMoreCheckAction"/></returns>
        IMoreCheckAction NotInArray(IEnumerable enumerable);

        /// <summary>
        /// 如果不在集合中
        /// </summary>
        /// <typeparam name="T">指定的类型</typeparam>
        /// <param name="enumerable">集合对象</param>
        /// <returns><see cref="IMoreCheckAction"/></returns>
        IMoreCheckAction NotInArray<T>(IEnumerable<T> enumerable);
    }
}