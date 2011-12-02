/**
 * file depends: DynamicMethodBuilder.cs 
 * 
 * Haibin Zou=>zhb
 * hibean2006@126.com
 * 2011-12-01 Created 
 * */
using System.Data;
using System.Reflection.Emit;

namespace Src
{
    /// <summary>
    /// 实现辅助类
    /// </summary>
    public class EntityHelper<T> where T : class ,new()
    {
        private CopyAction<T> copyFromRecordAction;
        /// <summary>
        /// 由<see cref="IDataRecord"/>(<see cref="IDataReader"/>)转换的数据类型
        /// </summary>
        /// <param name="record"><see cref="IDataRecord"/>(<see cref="IDataReader"/>)对象</param>
        /// <returns>查找<see cref="record"/>中的值，并对属性进行赋值</returns>
        public T ConvertFrom(IDataRecord record)
        {
            //复杂类型
            T result = new T();
            if (copyFromRecordAction == null)
            {
                DynamicMethod dm = new DynamicMethodBuilder().CreateCopyAction<T>(record);
                copyFromRecordAction =(CopyAction<T>)dm.CreateDelegate(typeof(CopyAction<T>)) ;
            }
            copyFromRecordAction(result, record);
            return result;
        }


        /// <summary>
        /// 复制委托定义
        /// </summary>
        /// <typeparam name="TModel">复制类型</typeparam>
        /// <param name="model">模型实例</param>
        /// <param name="record"><see cref="IDataRecord"/>(<see cref="IDataReader"/>)的实例</param>
        private delegate void CopyAction<in TModel>(TModel model, IDataRecord record) where TModel : class,new();

        
    }

    
}