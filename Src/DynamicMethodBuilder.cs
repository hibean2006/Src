/**
 * file depends: 
 * 
 * Haibin Zou=>zhb
 * hibean2006@126.com
 * 2011-12-01 Created 
 * */
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Reflection.Emit;

namespace Src
{
    /// <summary>
    /// 动态方法创建
    /// </summary>
    public class DynamicMethodBuilder
    {
        #region Emit 方法封装
        private void Add() { il.Emit(OpCodes.Add); }
        private void Add_Ovf() { il.Emit(OpCodes.Add_Ovf); }
        private void Add_Ovf_Un() { il.Emit(OpCodes.Add_Ovf_Un); }
        private void And() { il.Emit(OpCodes.And); }
        private void Arglist() { il.Emit(OpCodes.Arglist); }
        private void Beq() { il.Emit(OpCodes.Beq); }
        private void Beq_S() { il.Emit(OpCodes.Beq_S); }
        private void Bge() { il.Emit(OpCodes.Bge); }
        private void Bge_S() { il.Emit(OpCodes.Bge_S); }
        private void Bge_Un() { il.Emit(OpCodes.Bge_Un); }
        private void Bge_Un_S() { il.Emit(OpCodes.Bge_Un_S); }
        private void Bgt() { il.Emit(OpCodes.Bgt); }
        private void Bgt_S() { il.Emit(OpCodes.Bgt_S); }
        private void Bgt_Un() { il.Emit(OpCodes.Bgt_Un); }
        private void Bgt_Un_S() { il.Emit(OpCodes.Bgt_Un_S); }
        private void Ble() { il.Emit(OpCodes.Ble); }
        private void Ble_S() { il.Emit(OpCodes.Ble_S); }
        private void Ble_Un() { il.Emit(OpCodes.Ble_Un); }
        private void Ble_Un_S() { il.Emit(OpCodes.Ble_Un_S); }
        private void Blt() { il.Emit(OpCodes.Blt); }
        private void Blt_S(Label lbl) { il.Emit(OpCodes.Blt_S, lbl); }
        private void Blt_Un() { il.Emit(OpCodes.Blt_Un); }
        private void Blt_Un_S() { il.Emit(OpCodes.Blt_Un_S); }
        private void Bne_Un() { il.Emit(OpCodes.Bne_Un); }
        private void Bne_Un_S() { il.Emit(OpCodes.Bne_Un_S); }
        private void Box() { il.Emit(OpCodes.Box); }
        private void Br() { il.Emit(OpCodes.Br); }
        private void Br_S(Label lbl) { il.Emit(OpCodes.Br_S, lbl); }
        private void Break() { il.Emit(OpCodes.Break); }
        private void Brfalse() { il.Emit(OpCodes.Brfalse); }
        private void Brfalse_S() { il.Emit(OpCodes.Brfalse_S); }
        private void Brtrue() { il.Emit(OpCodes.Brtrue); }
        private void Brtrue_S(Label label) { il.Emit(OpCodes.Brtrue_S, label); }
        private void Call(MethodInfo methodInfo) { il.Emit(OpCodes.Call, methodInfo); }
        private void Calli() { il.Emit(OpCodes.Calli); }
        private void Callvirt(MethodInfo methodInfo) { il.Emit(OpCodes.Callvirt, methodInfo); }
        private void Castclass() { il.Emit(OpCodes.Castclass); }
        private void Ceq() { il.Emit(OpCodes.Ceq); }
        private void Cgt() { il.Emit(OpCodes.Cgt); }
        private void Cgt_Un() { il.Emit(OpCodes.Cgt_Un); }
        private void Ckfinite() { il.Emit(OpCodes.Ckfinite); }
        private void Clt() { il.Emit(OpCodes.Clt); }
        private void Clt_Un() { il.Emit(OpCodes.Clt_Un); }
        private void Constrained() { il.Emit(OpCodes.Constrained); }
        private void Conv_I() { il.Emit(OpCodes.Conv_I); }
        private void Conv_I1() { il.Emit(OpCodes.Conv_I1); }
        private void Conv_I2() { il.Emit(OpCodes.Conv_I2); }
        private void Conv_I4() { il.Emit(OpCodes.Conv_I4); }
        private void Conv_I8() { il.Emit(OpCodes.Conv_I8); }
        private void Conv_Ovf_I() { il.Emit(OpCodes.Conv_Ovf_I); }
        private void Conv_Ovf_I_Un() { il.Emit(OpCodes.Conv_Ovf_I_Un); }
        private void Conv_Ovf_I1() { il.Emit(OpCodes.Conv_Ovf_I1); }
        private void Conv_Ovf_I1_Un() { il.Emit(OpCodes.Conv_Ovf_I1_Un); }
        private void Conv_Ovf_I2() { il.Emit(OpCodes.Conv_Ovf_I2); }
        private void Conv_Ovf_I2_Un() { il.Emit(OpCodes.Conv_Ovf_I2_Un); }
        private void Conv_Ovf_I4() { il.Emit(OpCodes.Conv_Ovf_I4); }
        private void Conv_Ovf_I4_Un() { il.Emit(OpCodes.Conv_Ovf_I4_Un); }
        private void Conv_Ovf_I8() { il.Emit(OpCodes.Conv_Ovf_I8); }
        private void Conv_Ovf_I8_Un() { il.Emit(OpCodes.Conv_Ovf_I8_Un); }
        private void Conv_Ovf_U() { il.Emit(OpCodes.Conv_Ovf_U); }
        private void Conv_Ovf_U_Un() { il.Emit(OpCodes.Conv_Ovf_U_Un); }
        private void Conv_Ovf_U1() { il.Emit(OpCodes.Conv_Ovf_U1); }
        private void Conv_Ovf_U1_Un() { il.Emit(OpCodes.Conv_Ovf_U1_Un); }
        private void Conv_Ovf_U2() { il.Emit(OpCodes.Conv_Ovf_U2); }
        private void Conv_Ovf_U2_Un() { il.Emit(OpCodes.Conv_Ovf_U2_Un); }
        private void Conv_Ovf_U4() { il.Emit(OpCodes.Conv_Ovf_U4); }
        private void Conv_Ovf_U4_Un() { il.Emit(OpCodes.Conv_Ovf_U4_Un); }
        private void Conv_Ovf_U8() { il.Emit(OpCodes.Conv_Ovf_U8); }
        private void Conv_Ovf_U8_Un() { il.Emit(OpCodes.Conv_Ovf_U8_Un); }
        private void Conv_R_Un() { il.Emit(OpCodes.Conv_R_Un); }
        private void Conv_R4() { il.Emit(OpCodes.Conv_R4); }
        private void Conv_R8() { il.Emit(OpCodes.Conv_R8); }
        private void Conv_U() { il.Emit(OpCodes.Conv_U); }
        private void Conv_U1() { il.Emit(OpCodes.Conv_U1); }
        private void Conv_U2() { il.Emit(OpCodes.Conv_U2); }
        private void Conv_U4() { il.Emit(OpCodes.Conv_U4); }
        private void Conv_U8() { il.Emit(OpCodes.Conv_U8); }
        private void Cpblk() { il.Emit(OpCodes.Cpblk); }
        private void Cpobj() { il.Emit(OpCodes.Cpobj); }
        private void Div() { il.Emit(OpCodes.Div); }
        private void Div_Un() { il.Emit(OpCodes.Div_Un); }
        private void Dup() { il.Emit(OpCodes.Dup); }
        private void Endfilter() { il.Emit(OpCodes.Endfilter); }
        private void Endfinally() { il.Emit(OpCodes.Endfinally); }
        private void Initblk() { il.Emit(OpCodes.Initblk); }
        private void Initobj() { il.Emit(OpCodes.Initobj); }
        private void Isinst() { il.Emit(OpCodes.Isinst); }
        private void Jmp() { il.Emit(OpCodes.Jmp); }
        private void Ldarg() { il.Emit(OpCodes.Ldarg); }
        private void Ldarg_0() { il.Emit(OpCodes.Ldarg_0); }
        private void Ldarg_1() { il.Emit(OpCodes.Ldarg_1); }
        private void Ldarg_2() { il.Emit(OpCodes.Ldarg_2); }
        private void Ldarg_3() { il.Emit(OpCodes.Ldarg_3); }
        private void Ldarg_S() { il.Emit(OpCodes.Ldarg_S); }
        private void Ldarga() { il.Emit(OpCodes.Ldarga); }
        private void Ldarga_S() { il.Emit(OpCodes.Ldarga_S); }
        private void Ldc_I4() { il.Emit(OpCodes.Ldc_I4); }
        private void Ldc_I4_0() { il.Emit(OpCodes.Ldc_I4_0); }
        private void Ldc_I4_1() { il.Emit(OpCodes.Ldc_I4_1); }
        private void Ldc_I4_2() { il.Emit(OpCodes.Ldc_I4_2); }
        private void Ldc_I4_3() { il.Emit(OpCodes.Ldc_I4_3); }
        private void Ldc_I4_4() { il.Emit(OpCodes.Ldc_I4_4); }
        private void Ldc_I4_5() { il.Emit(OpCodes.Ldc_I4_5); }
        private void Ldc_I4_6() { il.Emit(OpCodes.Ldc_I4_6); }
        private void Ldc_I4_7() { il.Emit(OpCodes.Ldc_I4_7); }
        private void Ldc_I4_8() { il.Emit(OpCodes.Ldc_I4_8); }
        private void Ldc_I4_M1() { il.Emit(OpCodes.Ldc_I4_M1); }
        private void Ldc_I4_S() { il.Emit(OpCodes.Ldc_I4_S); }
        private void Ldc_I8() { il.Emit(OpCodes.Ldc_I8); }
        private void Ldc_R4() { il.Emit(OpCodes.Ldc_R4); }
        private void Ldc_R8() { il.Emit(OpCodes.Ldc_R8); }
        private void Ldelem() { il.Emit(OpCodes.Ldelem); }
        private void Ldelem_I() { il.Emit(OpCodes.Ldelem_I); }
        private void Ldelem_I1() { il.Emit(OpCodes.Ldelem_I1); }
        private void Ldelem_I2() { il.Emit(OpCodes.Ldelem_I2); }
        private void Ldelem_I4() { il.Emit(OpCodes.Ldelem_I4); }
        private void Ldelem_I8() { il.Emit(OpCodes.Ldelem_I8); }
        private void Ldelem_R4() { il.Emit(OpCodes.Ldelem_R4); }
        private void Ldelem_R8() { il.Emit(OpCodes.Ldelem_R8); }
        private void Ldelem_Ref() { il.Emit(OpCodes.Ldelem_Ref); }
        private void Ldelem_U1() { il.Emit(OpCodes.Ldelem_U1); }
        private void Ldelem_U2() { il.Emit(OpCodes.Ldelem_U2); }
        private void Ldelem_U4() { il.Emit(OpCodes.Ldelem_U4); }
        private void Ldelema() { il.Emit(OpCodes.Ldelema); }
        private void Ldfld() { il.Emit(OpCodes.Ldfld); }
        private void Ldflda() { il.Emit(OpCodes.Ldflda); }
        private void Ldftn() { il.Emit(OpCodes.Ldftn); }
        private void Ldind_I() { il.Emit(OpCodes.Ldind_I); }
        private void Ldind_I1() { il.Emit(OpCodes.Ldind_I1); }
        private void Ldind_I2() { il.Emit(OpCodes.Ldind_I2); }
        private void Ldind_I4() { il.Emit(OpCodes.Ldind_I4); }
        private void Ldind_I8() { il.Emit(OpCodes.Ldind_I8); }
        private void Ldind_R4() { il.Emit(OpCodes.Ldind_R4); }
        private void Ldind_R8() { il.Emit(OpCodes.Ldind_R8); }
        private void Ldind_Ref() { il.Emit(OpCodes.Ldind_Ref); }
        private void Ldind_U1() { il.Emit(OpCodes.Ldind_U1); }
        private void Ldind_U2() { il.Emit(OpCodes.Ldind_U2); }
        private void Ldind_U4() { il.Emit(OpCodes.Ldind_U4); }
        private void Ldlen() { il.Emit(OpCodes.Ldlen); }
        private void Ldloc() { il.Emit(OpCodes.Ldloc); }
        private void Ldloc_0() { il.Emit(OpCodes.Ldloc_0); }
        private void Ldloc_1() { il.Emit(OpCodes.Ldloc_1); }
        private void Ldloc_2() { il.Emit(OpCodes.Ldloc_2); }
        private void Ldloc_3() { il.Emit(OpCodes.Ldloc_3); }
        private void Ldloc_S() { il.Emit(OpCodes.Ldloc_S); }
        private void Ldloca() { il.Emit(OpCodes.Ldloca); }
        private void Ldloca_S() { il.Emit(OpCodes.Ldloca_S); }
        private void Ldnull() { il.Emit(OpCodes.Ldnull); }
        private void Ldobj() { il.Emit(OpCodes.Ldobj); }
        private void Ldsfld() { il.Emit(OpCodes.Ldsfld); }
        private void Ldsflda() { il.Emit(OpCodes.Ldsflda); }
        private void Ldstr(string str) { il.Emit(OpCodes.Ldstr, str); }
        private void Ldtoken() { il.Emit(OpCodes.Ldtoken); }
        private void Ldvirtftn() { il.Emit(OpCodes.Ldvirtftn); }
        private void Leave() { il.Emit(OpCodes.Leave); }
        private void Leave_S() { il.Emit(OpCodes.Leave_S); }
        private void Localloc() { il.Emit(OpCodes.Localloc); }
        private void MarkLabel(Label label) { il.MarkLabel(label); }
        private void Mkrefany() { il.Emit(OpCodes.Mkrefany); }
        private void Mul() { il.Emit(OpCodes.Mul); }
        private void Mul_Ovf() { il.Emit(OpCodes.Mul_Ovf); }
        private void Mul_Ovf_Un() { il.Emit(OpCodes.Mul_Ovf_Un); }
        private void Neg() { il.Emit(OpCodes.Neg); }
        private void Newarr() { il.Emit(OpCodes.Newarr); }
        private void Newobj(ConstructorInfo con) { il.Emit(OpCodes.Newobj, con); }
        private void Nop() { il.Emit(OpCodes.Nop); }
        private void Not() { il.Emit(OpCodes.Not); }
        private void Or() { il.Emit(OpCodes.Or); }
        private void Pop() { il.Emit(OpCodes.Pop); }
        private void Prefix1() { il.Emit(OpCodes.Prefix1); }
        private void Prefix2() { il.Emit(OpCodes.Prefix2); }
        private void Prefix3() { il.Emit(OpCodes.Prefix3); }
        private void Prefix4() { il.Emit(OpCodes.Prefix4); }
        private void Prefix5() { il.Emit(OpCodes.Prefix5); }
        private void Prefix6() { il.Emit(OpCodes.Prefix6); }
        private void Prefix7() { il.Emit(OpCodes.Prefix7); }
        private void Prefixref() { il.Emit(OpCodes.Prefixref); }
        private void Readonly() { il.Emit(OpCodes.Readonly); }
        private void Refanytype() { il.Emit(OpCodes.Refanytype); }
        private void Refanyval() { il.Emit(OpCodes.Refanyval); }
        private void Rem() { il.Emit(OpCodes.Rem); }
        private void Rem_Un() { il.Emit(OpCodes.Rem_Un); }
        private void Ret() { il.Emit(OpCodes.Ret); }
        private void Rethrow() { il.Emit(OpCodes.Rethrow); }
        private void Shl() { il.Emit(OpCodes.Shl); }
        private void Shr() { il.Emit(OpCodes.Shr); }
        private void Shr_Un() { il.Emit(OpCodes.Shr_Un); }
        private void Sizeof() { il.Emit(OpCodes.Sizeof); }
        private void Starg() { il.Emit(OpCodes.Starg); }
        private void Starg_S() { il.Emit(OpCodes.Starg_S); }
        private void Stelem() { il.Emit(OpCodes.Stelem); }
        private void Stelem_I() { il.Emit(OpCodes.Stelem_I); }
        private void Stelem_I1() { il.Emit(OpCodes.Stelem_I1); }
        private void Stelem_I2() { il.Emit(OpCodes.Stelem_I2); }
        private void Stelem_I4() { il.Emit(OpCodes.Stelem_I4); }
        private void Stelem_I8() { il.Emit(OpCodes.Stelem_I8); }
        private void Stelem_R4() { il.Emit(OpCodes.Stelem_R4); }
        private void Stelem_R8() { il.Emit(OpCodes.Stelem_R8); }
        private void Stelem_Ref() { il.Emit(OpCodes.Stelem_Ref); }
        private void Stfld() { il.Emit(OpCodes.Stfld); }
        private void Stind_I() { il.Emit(OpCodes.Stind_I); }
        private void Stind_I1() { il.Emit(OpCodes.Stind_I1); }
        private void Stind_I2() { il.Emit(OpCodes.Stind_I2); }
        private void Stind_I4() { il.Emit(OpCodes.Stind_I4); }
        private void Stind_I8() { il.Emit(OpCodes.Stind_I8); }
        private void Stind_R4() { il.Emit(OpCodes.Stind_R4); }
        private void Stind_R8() { il.Emit(OpCodes.Stind_R8); }
        private void Stind_Ref() { il.Emit(OpCodes.Stind_Ref); }
        private void Stloc() { il.Emit(OpCodes.Stloc); }
        private void Stloc_0() { il.Emit(OpCodes.Stloc_0); }
        private void Stloc_1() { il.Emit(OpCodes.Stloc_1); }
        private void Stloc_2() { il.Emit(OpCodes.Stloc_2); }
        private void Stloc_3() { il.Emit(OpCodes.Stloc_3); }
        private void Stloc_S() { il.Emit(OpCodes.Stloc_S); }
        private void Stobj() { il.Emit(OpCodes.Stobj); }
        private void Stsfld() { il.Emit(OpCodes.Stsfld); }
        private void Sub() { il.Emit(OpCodes.Sub); }
        private void Sub_Ovf() { il.Emit(OpCodes.Sub_Ovf); }
        private void Sub_Ovf_Un() { il.Emit(OpCodes.Sub_Ovf_Un); }
        private void Switch() { il.Emit(OpCodes.Switch); }
        private void Tailcall() { il.Emit(OpCodes.Tailcall); }
        private void Throw() { il.Emit(OpCodes.Throw); }
        private void Unaligned() { il.Emit(OpCodes.Unaligned); }
        private void Unbox() { il.Emit(OpCodes.Unbox); }
        private void Unbox_Any() { il.Emit(OpCodes.Unbox_Any); }
        private void Volatile() { il.Emit(OpCodes.Volatile); }
        private void Xor() { il.Emit(OpCodes.Xor); }

        #endregion

        private ILGenerator il;
        #region 常用 MethodInfo

        readonly ConstructorInfo argurmentNullException_Ctor = typeof(ArgumentNullException).GetConstructor(new Type[] { typeof(string) });
        readonly MethodInfo dataRecord_GetOrdinal = typeof(IDataRecord).GetMethod("GetOrdinal", new Type[] { typeof(string) });
        readonly MethodInfo dataRecord_GetValue = typeof(IDataRecord).GetMethod("GetValue", new Type[] { typeof(int) });
        readonly MethodInfo dataRecord_IsDBNull = typeof(IDataRecord).GetMethod("IsDBNull", new Type[] { typeof(int) });
        #endregion

        /// <summary>
        /// 创建一个从<see cref="IDataRecord"/>(<see cref="IDataReader"/>)对象中复制数据的动态方法
        /// </summary>
        /// <typeparam name="T">需要赋值的对象类型</typeparam>
        /// <param name="record">取出的数据</param>
        /// <returns>返回创建之后的动态方法</returns>
        public DynamicMethod CreateCopyAction<T>(IDataRecord record)
        {
            if (record == null) throw new ArgumentNullException("record");
            Fields fields = new Fields(record);
            DynamicMethod dm = new DynamicMethod("Dynamic", null, new Type[] { typeof(T), typeof(IDataRecord) }, typeof(DynamicMethodBuilder), true);
            il = dm.GetILGenerator();
            dm.DefineParameter(0, ParameterAttributes.In, "model");
            dm.DefineParameter(1, ParameterAttributes.In, "record");
            #region Emits
            il.DeclareLocal(typeof(Int32));
            il.DeclareLocal(typeof(bool));

            Nop();
            Ldarg_0();
            Emit_CheckParameterNull("model");

            Ldarg_1();
            Emit_CheckParameterNull("record");

            PropertyInfo[] propertyInfos = typeof(T).GetProperties();
            foreach (var pInfo in propertyInfos)
            {
                if (fields.Contains(pInfo.Name))
                {
                    Emit_SetPropertyValue(pInfo);
                }
            }

            il.Emit(OpCodes.Ret);
            #endregion

            return dm;
        }

        private class Fields
        {
            private List<string> names;
            public Fields(IDataRecord record)
            {
                names = new List<string>(record.FieldCount);
                for (int i = 0; i < record.FieldCount; i++)
                {
                    names.Add(record.GetName(i));
                }
            }
            public bool Contains(string field)
            {
                for (int i = 0; i < names.Count; i++)
                {
                    if (string.Compare(names[i], field, true) == 0) return true;
                }
                return false;
            }
        }

        private void Emit_SetPropertyValue(PropertyInfo pInfo)
        {
            //如果包含有属性的列，则进行赋值
            if (!pInfo.CanWrite) return;
            if (!(IsBasicType(pInfo.PropertyType) || IsNullableType(pInfo.PropertyType))) return;
            Ldarg_1();
            Ldstr(pInfo.Name);
            Callvirt(dataRecord_GetOrdinal);
            Stloc_0();
            Ldloc_0();
            Ldc_I4_0();
            Label lbl = il.DefineLabel();
            Blt_S(lbl);
            Ldarg_1();
            Ldloc_0();
            Callvirt(dataRecord_IsDBNull);
            Label lbl2 = il.DefineLabel();
            Br_S(lbl2);
            MarkLabel(lbl);
            Ldc_I4_1();
            MarkLabel(lbl2);
            Stloc_1();
            Ldloc_1();
            Label lbl3 = il.DefineLabel();
            Brtrue_S(lbl3);
            Nop();
            Ldarg_0();
            Ldarg_1();
            Ldloc_0();
            Callvirt(dataRecord_GetValue);
            Call(Convert_To(pInfo.PropertyType));
            Callvirt(pInfo.GetSetMethod());
            Nop();
            Nop();
            MarkLabel(lbl3);
        }

        private bool IsNullableType(Type propertyType)
        {
            var type = Nullable.GetUnderlyingType(propertyType);
            if (type == null) return false;
            foreach (var basicType in BasicTypes())
            {
                if (type == basicType) return true;
            }
            return false;
        }

        private bool IsBasicType(Type type)
        {
            if (type == typeof(string)) return true;
            foreach (var basicType in BasicTypes())
            {
                if (type == basicType) return true;
            }
            return false;
        }

        private IEnumerable<Type> BasicTypes()
        {
            yield return typeof(int);
            yield return typeof(DateTime);
            yield return typeof(bool);
            yield return typeof(decimal);
            yield return typeof(short);
            yield return typeof(ushort);
            yield return typeof(long);
            yield return typeof(ulong);
            yield return typeof(byte);
            yield return typeof(char);
            yield return typeof(double);
            yield return typeof(sbyte);
            yield return typeof(float);
        }

        private MethodInfo Convert_To(Type type)
        {
            Type innerType = null;
            if (type.IsGenericType)
            {
                innerType = Nullable.GetUnderlyingType(type);
            }
            return typeof(Convert).GetMethod("To" + (innerType ?? type).Name, new Type[] { typeof(object) });
        }

        private void Emit_CheckParameterNull(string pName)
        {
            //if(pName==null) throw new ArgumentNullException("pName");
            Ldnull();
            Ceq();
            Ldc_I4_0();
            Ceq();
            Stloc_1();
            Ldloc_1();
            Label lbl = il.DefineLabel();
            Brtrue_S(lbl);
            Ldstr(pName);
            Newobj(argurmentNullException_Ctor);
            Throw();
            MarkLabel(lbl);
        }
    }
}