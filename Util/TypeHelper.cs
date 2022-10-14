using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Util
{
    public static class TypeHelper
    {
        /// <summary>
        /// 判断是否内置类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsBulitInType(Type type)
        {
            return (type == typeof(object) || Type.GetTypeCode(type) != TypeCode.Object);
        }

        /// <summary>
        /// 检查是否包含无参公共构造函数
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool ExistNonParamPublicConstructor(Type type)
        {
            bool result = false;
            ConstructorInfo[] infoArray = type.GetConstructors();
            foreach (ConstructorInfo info in infoArray)
            {
                if (info.IsPublic && info.GetParameters().Length == 0)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }


        #region 类型分支判断
        /// <summary>
        /// 对不同分支类型, 执行不同操作的构建类
        /// <para>输入对象为null时, 使用类型模式 : 只判断类型, 无需将对象作为输入</para>
        /// <para>输入对象不为null时, 使用对象模式 : 判断输入对象的类型, 需要将对象作为输入</para>
        /// </summary>
        public class TypeSwitchBuilder
        {
            #region 输入对象
            private Type InputType { get; set; }
            private object InputObj { get; set; }

            #endregion
            public TypeSwitchBuilder(object inputObj)
            {
                InputObj = inputObj;
                InputType = inputObj?.GetType();
            }
            public TypeSwitchBuilder(Type inputType)
            {
                InputType = inputType;
                InputObj = null;
            }
            #region 构成


            #region 构成内容
            /// <summary>
            /// 类型模式的类型与Action对应字典
            /// </summary>
            private Dictionary<Type, Action> TypeModeActions = new Dictionary<Type, Action>();
            /// <summary>
            /// 对象模式的类型与Action对应字典
            /// </summary>
            private Dictionary<Type, Action<object>> ObjModeActions = new Dictionary<Type, Action<object>>();
            /// <summary>
            /// 默认情况下的执行方法 (类型未匹配与输入的类型匹配)
            /// </summary>
            private Action DefaultAction;
            #endregion
            /// <summary>
            /// 输入类型的情况下执行
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="doSomething"></param>
            /// <returns></returns>
            public TypeSwitchBuilder Case<T>(Action<T> doSomething)
            {
                if (doSomething == null) throw new ArgumentNullException(nameof(doSomething));
                Type type = typeof(T);
                if (!ObjModeActions.ContainsKey(type))
                {
                    ObjModeActions.Add(type, (obj) => doSomething.Invoke((T)obj));
                }
                else
                {
                    throw new ArgumentException($"输入的类型重复: {type.Name}", nameof(T));
                }
                return this;
            }
            /// <summary>
            /// 输入类型的情况下执行
            /// </summary>
            /// <param name="doSomething"></param>
            /// <returns></returns>
            public TypeSwitchBuilder Case<T>(Action doSomething)
            {
                if (doSomething == null) throw new ArgumentNullException(nameof(doSomething));
                Type type = typeof(T);
                if (!TypeModeActions.ContainsKey(type))
                {
                    TypeModeActions.Add(type, doSomething);
                }
                else
                {
                    throw new ArgumentException($"输入的类型重复: {type.Name}", nameof(T));
                }
                return this;
            }
            /// <summary>
            /// 默认情况下执行
            /// </summary>
            /// <param name="doSomething"></param>
            /// <returns></returns>
            public TypeSwitchBuilder Default(Action doSomething)
            {
                DefaultAction = doSomething;
                return this;
            }
            #endregion
            /// <summary>
            /// 执行
            /// </summary>
            public void Run()
            {
                if (InputType == null) throw new ArgumentNullException(nameof(InputType));

                if (InputObj == null)
                {
                    RunTypeMode();
                }
                else
                {
                    RunObjMode();
                }
            }
            private void RunTypeMode()
            {
                foreach (Type type in TypeModeActions.Keys)
                {
                    if (InputType.Equals(type))
                    {
                        TypeModeActions[type].Invoke();
                        return;
                    }
                }
                DefaultAction.Invoke();
            }
            private void RunObjMode()
            {
                foreach (Type type in ObjModeActions.Keys)
                {
                    if (InputType.Equals(type))
                    {
                        ObjModeActions[type].Invoke(InputObj);
                        return;
                    }
                }
                DefaultAction.Invoke();
            }
        }
        /// <summary>
        /// 取得对不同分支类型, 执行不同操作的构建类实例
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static TypeSwitchBuilder SwitchType(object obj)
        {
            return new TypeSwitchBuilder(obj);
        }
        /// <summary>
        /// 取得对不同分支类型, 执行不同操作的构建类实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static TypeSwitchBuilder SwitchType<T>()
        {
            return new TypeSwitchBuilder(typeof(T));
        }
        /// <summary>
        /// 取得对不同分支类型, 执行不同操作的构建类实例
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static TypeSwitchBuilder SwitchType(Type type)
        {
            return new TypeSwitchBuilder(type);
        }
        #endregion


    }
}
