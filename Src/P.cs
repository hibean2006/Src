/**
 * file depends:    IMoreCheckAction.cs
 *                          IParameterChecker.cs
 *                          IParameterPicker.cs                      
 * 
 * Haibin Zou=>zhb
 * hibean2006@126.com
 * 2011-11-19 Created  
 * */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Src
{
    /// <summary>
    /// 参数检查
    /// </summary>
    public static class P
    {
        /// <summary>
        /// 参数检查失败时，抛出异常
        /// </summary>
        public static IParameterPicker Throw()
        {
            return new ParameterPicker(Mode.Throw);
        }

        /// <summary>
        /// 参数检查失败时，执行指定的方法
        /// </summary>
        /// <param name="action">失败时，执行的方法</param>
        public static IParameterPicker Do(Action<string> action)
        {
            return new ParameterPicker(Mode.Other, action);
        }

        /// <summary>
        /// 参数检查失败时，执行<see cref="Debug.Assert(bool)"/>
        /// </summary>
        public static IParameterPicker Assert()
        {
            return new ParameterPicker(Mode.Assert);
        }

        /// <summary>
        /// 参数检查失败时，记录日志
        /// </summary>
        public static IParameterPicker Log()
        {
            return new ParameterPicker(Mode.Log);
        }

        /// <summary>
        /// 参数检查失败时，断言失败，并记录日志
        /// </summary>
        public static IParameterPicker ThrowAndLog()
        {
            return new ParameterPicker(Mode.Throw | Mode.Log);
        }

        /// <summary>
        /// 参数检查失败时，抛出异常，并记录日志
        /// </summary>
        public static IParameterPicker AssertAndLog()
        {
            return new ParameterPicker(Mode.Assert | Mode.Log);
        }

        /// <summary>
        /// 参数检查失败时，执行指定动作，并记录日志
        /// </summary>
        public static IParameterPicker ActionAndLog(Action<string> action)
        {
            return new ParameterPicker(Mode.Other | Mode.Log, action);
        }

        private class ParameterPicker : IParameterPicker
        {
            private readonly Mode mode;
            private readonly Action<string> action;

            public ParameterPicker(Mode mode)
                : this(mode, null)
            {
            }

            public ParameterPicker(Mode mode, Action<string> action)
            {
                this.mode = mode;
                this.action = action;
            }
            #region Implementation of IParameterPicker

            public Action<string> Action
            {
                get { return action; }
            }

            public Mode ProcessMode
            {
                get { return mode; }
            }

            public IParameterChecker When(object target, string pName)
            {
                return new ParameterChecker(this, pName, target);
            }

            #endregion
        }

        private class ParameterChecker : IParameterChecker, IMoreCheckAction
        {
            private readonly ParameterPicker picker;
            private readonly object target;
            private readonly string pName;

            public ParameterChecker(ParameterPicker picker, string pName, object target)
            {
                this.picker = picker;
                this.target = target;
                this.pName = pName;
            }
            #region Implementation of IParameterChecker<T>

            public IMoreCheckAction IsNull()
            {
                if (target is ValueType) return this;
                if (target == null)
                {
                    Deal("不能为 null");
                }
                return this;
            }

            private void Deal(string des)
            {
                const string Msg = "参数检查失败：参数对象{0}{1}";
                string msg = string.Format((IFormatProvider)null, Msg, pName, des);
                if (Mode.Other == (picker.ProcessMode & Mode.Other))
                {
                    if (picker.Action != null)
                    {
                        picker.Action(msg);
                    }
                }
                if (Mode.Log == (picker.ProcessMode & Mode.Log))
                {
                    StackTrace trace = new StackTrace(2);
                    string content = string.Format("{2}----{0}\r\n{1}\r\n", msg, trace,
                                                   DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    File.AppendAllText(FileHelper.GetPhysicalPath("~error.err"), content);
                }
                if (Mode.Assert == (picker.ProcessMode & Mode.Assert))
                {
                    Debug.Fail(msg);
                }
                if (Mode.Throw == (picker.ProcessMode & Mode.Throw))
                {
                    throw new ArgumentException(msg);
                }
            }

            public IMoreCheckAction IsFalse()
            {
                if (target is bool)
                {
                    bool temp = Convert.ToBoolean(target);
                    if (!temp)
                    {
                        Deal("结果为 false");
                    }
                }
                return this;
            }

            public IMoreCheckAction IsEmpty()
            {
                if ((target as string) == string.Empty)
                {
                    Deal("不能为 \"\"");
                }
                if ((target as DBNull) == DBNull.Value)
                {
                    Deal("不能为 DBNull.Value");
                }
                return this;
            }

            public IMoreCheckAction IsEqual<T>(T value) where T : IComparable
            {
                if (target == null && (object)value == null)
                {
                    Deal("不能为 null");
                }
                else if (value.CompareTo(target) == 0)
                {
                    Deal(string.Format("不能为 {0}", value));
                }
                return this;
            }

            public IMoreCheckAction IsNotEqual<T>(T value) where T : IComparable
            {
                if (target != null && (object)value == null)
                {
                    Deal("必须为 null");
                }
                else if (value.CompareTo(target) != 0)
                {
                    Deal(string.Format("必须为 {0}", value));
                }
                return this;
            }

            public IMoreCheckAction IsLessThan<T>(T value) where T : IComparable
            {
                if ((object)value == null)
                {
                    throw new ArgumentNullException("value", "比较对象不能为 null");
                }
                if (value.CompareTo(target) > 0)
                {
                    Deal(string.Format("不能小于 {0}", value));
                }
                return this;
            }

            public IMoreCheckAction IsLessOrEqualThan<T>(T value) where T : IComparable
            {
                if ((object)value == null)
                {
                    throw new ArgumentNullException("value", "比较对象不能为 null");
                }
                if (value.CompareTo(target) >= 0)
                {
                    Deal(string.Format("不能小于或等于 {0}", value));
                }
                return this;
            }

            public IMoreCheckAction IsGreaterOrEqualThan<T>(T value) where T : IComparable
            {
                if ((object)value == null)
                {
                    throw new ArgumentNullException("value", "比较对象不能为 null");
                }
                if (value.CompareTo(target) <= 0)
                {
                    Deal(string.Format("不能大于或等于 {0}", value));
                }
                return this;
            }

            public IMoreCheckAction IsGreaterThan<T>(T value) where T : IComparable
            {
                if ((object)value == null)
                {
                    throw new ArgumentNullException("value", "比较对象不能为 null");
                }
                if (value.CompareTo(target) < 0)
                {
                    Deal(string.Format("不能大于 {0}", value));
                }
                return this;
            }


            public IMoreCheckAction IsTrue()
            {
                throw new NotImplementedException();
            }

            public IMoreCheckAction IsNotInstanceOf<T>()
            {
                if (!(target is T))
                {
                    Deal(string.Format("应为 {0} 类型", typeof(T).FullName));
                }
                return this;
            }

            public IMoreCheckAction InArray(IEnumerable enumerable)
            {
                if (enumerable == null)
                {
                    throw new ArgumentNullException("enumerable", "集合对象不能为 null");
                }
                StringBuilder builder = new StringBuilder();
                bool result = true;
                foreach (object val in enumerable)
                {
                    builder.AppendFormat("{0},", val);
                    if (target == val)
                    {
                        result = false;
                    }
                }
                if (builder.Length == 0)
                {
                    throw new ArgumentException("enumerable 对象不能为空集合");
                }
                builder.Length--;
                if (!result)
                {
                    Deal(string.Format("不能为 [{0}] 中的一员", builder));
                }
                return this;
            }

            public IMoreCheckAction InArray<T>(IEnumerable<T> enumerable)
            {
                return InArray((IEnumerable)enumerable);
            }

            public IMoreCheckAction NotInArray(IEnumerable enumerable)
            {
                if (enumerable == null)
                {
                    throw new ArgumentNullException("enumerable", "集合对象不能为 null");
                }
                StringBuilder builder = new StringBuilder();
                foreach (object val in enumerable)
                {
                    builder.AppendFormat("{0},", val);
                    if (target == val)
                    {
                        return this;
                    }
                }
                if (builder.Length == 0)
                {
                    throw new ArgumentException("enumerable 对象不能为空集合");
                }
                builder.Length--;
                Deal(string.Format("必须为 [{0}] 中的一员", builder));
                return this;
            }

            public IMoreCheckAction NotInArray<T>(IEnumerable<T> enumerable)
            {
                return NotInArray((IEnumerable)enumerable);
            }

            #endregion

            #region Implementation of IMoreCheckAction<T>

            public IParameterChecker Or(object anotherTarget, string _pName)
            {
                return new ParameterChecker(picker, _pName, anotherTarget);
            }

            public IParameterChecker Or()
            {
                return new ParameterChecker(picker, pName, target);
            }

            #endregion
        }

        /// <summary>
        /// 处理模式
        /// </summary>
        [Flags]
        private enum Mode
        {
            /// <summary>
            /// 抛出异常
            /// </summary>
            Throw = 1,

            /// <summary>
            /// 断言失败
            /// </summary>
            Assert = 2,

            /// <summary>
            /// 记录日志
            /// </summary>
            Log = 4,

            /// <summary>
            /// 其它，具体动作由第二个参数决定
            /// </summary>
            Other = 8
        }
    }
}