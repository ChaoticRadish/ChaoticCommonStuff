using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Util
{
    public static class TypeHelper
    {

        /// <summary>
        /// 获取数组的类类型
        /// </summary>
        /// <param name="t">类型</param>
        /// <returns>类类型</returns>
        public static Type GetArrayElementType(this Type t)
        {
            if (!t.IsArray) return null;
            string name = t.FullName.Replace("[]", string.Empty);
            return t.Assembly.GetType(name);
        }
        /// <summary>
        /// 判断类型是否基于指定类型
        /// </summary>
        /// <param name="type"></param>
        /// <param name="baseType"></param>
        /// <returns></returns>
        public static bool As(this Type type, Type baseType)
        {
            if (type == null)
            {
                return false;
            }

            if (baseType.IsGenericTypeDefinition && type.IsGenericType && !type.IsGenericTypeDefinition)
            {
                type = type.GetGenericTypeDefinition();
            }

            if (type == baseType)
            {
                return true;
            }

            if (baseType.IsAssignableFrom(type))
            {
                return true;
            }

            bool flag = false;
            if (baseType.IsInterface)
            {
                if (type.GetInterface(baseType.FullName) != null)
                {
                    flag = true;
                }
                else if (type.GetInterfaces().Any((Type e) => (!e.IsGenericType || !baseType.IsGenericTypeDefinition) ? (e == baseType) : (e.GetGenericTypeDefinition() == baseType)))
                {
                    flag = true;
                }
            }

            if (!flag && type.Assembly.ReflectionOnly)
            {
                while (!flag && type != typeof(object))
                {
                    if (!(type == null))
                    {
                        if (type.FullName == baseType.FullName && type.AssemblyQualifiedName == baseType.AssemblyQualifiedName)
                        {
                            flag = true;
                        }

                        type = type.BaseType;
                    }
                }
            }

            return flag;
        }
        /// <summary>
        /// 判断类型是否列表
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsList(this Type type)
        {
            if (type != null && type.IsGenericType)
            {
                return type.As(typeof(IList<>));
            }

            return false;
        }
        /// <summary>
        /// 判断类型是否可枚举类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsEnumerable(this Type type)
        {
            if (type != null && type.IsGenericType)
            {
                return type.As(typeof(IEnumerable<>));
            }

            return false;
        }

        /// <summary>
        /// 判断类型是否字典
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsDictionary(this Type type)
        {
            if (type != null && type.IsGenericType)
            {
                return type.As(typeof(IDictionary<,>));
            }

            return false;
        }


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

        #region Expression
        /// <summary>
        /// 取得输入的 Expression 指向的属性 / 字段名
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="getProperty"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static string GetName<T, TProperty>(Expression<Func<T, TProperty>> getProperty)
        {
            if (getProperty == null)
            {
                throw new ArgumentNullException(nameof(getProperty));
            }
            MemberExpression member = getProperty.Body as MemberExpression;
            if (member == null)
            {
                throw new ArgumentException($"请为类型 \"{typeof(T).FullName}\" 指定一个字段或属性作为 Lambda 的主体", nameof(getProperty));
            }
            return member.Member.Name;
        }
        #endregion

        #region 类型分支判断
        /// <summary>
        /// 对不同分支类型, 执行不同操作的构建类
        /// <para>输入对象为null时, 使用类型模式 : 只判断类型, 无需将对象作为输入</para>
        /// <para>输入对象不为null时, 使用对象模式 : 判断输入对象的类型, 需要将对象作为输入</para>
        /// </summary>
        public class TypeSwitchBuilder
        {
            #region 输入对象
            /// <summary>
            /// 输入的类型
            /// </summary>
            public readonly Type InputType;
            /// <summary>
            /// 输入的对象
            /// </summary>
            private readonly object InputObj;
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

            #region 执行信息
            /// <summary>
            /// 执行信息
            /// </summary>
            public struct ExceptionInfo
            {
                /// <summary>
                /// 执行的类别
                /// </summary>
                public ExceptionTypeEnum ExceptionType { get; set; }
                /// <summary>
                /// 执行的类型
                /// </summary>
                public Type Type { get; set; }

                public override string ToString()
                {
                    switch (ExceptionType) 
                    {
                        default:
                            return $"{ExceptionType.GetDesc()}: {Type.FullName}";
                        case ExceptionTypeEnum.Default:
                            return ExceptionType.GetDesc();
                }
                }
            }
            public enum ExceptionTypeEnum
            {
                /// <summary>
                /// 等于类型
                /// </summary>
                [EnumHelper.Desc("等于类型")]
                EqualsType,
                /// <summary>
                /// 继承于类型
                /// </summary>
                [EnumHelper.Desc("继承于类型")]
                BaseType,
                /// <summary>
                /// 无匹配类型, 执行了默认方法
                /// </summary>
                [EnumHelper.Desc("默认方法")]
                Default,
            }
            #endregion
            #region 构成


            #region 构成内容
            /// <summary>
            /// 类型模式的类型与Action对应字典, 相等部分
            /// </summary>
            private Dictionary<Type, Action> TypeModeActions = new Dictionary<Type, Action>();
            /// <summary>
            /// 类型模式的类型与Action对应字典, 基类部分
            /// </summary>
            private Dictionary<Type, Action> TypeBaseModeActions = new Dictionary<Type, Action>();
            /// <summary>
            /// 对象模式的类型与Action对应字典, 相等部分
            /// </summary>
            private Dictionary<Type, Action<object>> ObjModeActions = new Dictionary<Type, Action<object>>();
            /// <summary>
            /// 对象模式的类型与Action对应字典, 基类部分
            /// </summary>
            private Dictionary<Type, Action<object>> ObjBaseModeActions = new Dictionary<Type, Action<object>>();
            /// <summary>
            /// 默认情况下的执行方法 (类型未匹配与输入的类型匹配)
            /// </summary>
            private Action DefaultAction;
            #endregion
            /// <summary>
            /// 输入类型是该类型的情况下执行
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
                    throw new ArgumentException($"Case 输入的类型重复: {type.Name}", nameof(T));
                }
                return this;
            }
            /// <summary>
            /// 输入类型是该类型的情况下执行
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
                    throw new ArgumentException($"Case 输入的类型重复: {type.Name}", nameof(T));
                }
                return this;
            }
            /// <summary>
            /// 输入类型继承于该类型的情况下执行, 会在Case之后判断及运行
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="doSomething"></param>
            /// <returns></returns>
            public TypeSwitchBuilder CaseBase<T>(Action<T> doSomething)
            {
                if (doSomething == null) throw new ArgumentNullException(nameof(doSomething));
                Type type = typeof(T);
                if (!ObjBaseModeActions.ContainsKey(type))
                {
                    ObjBaseModeActions.Add(type, (obj) => doSomething.Invoke((T)obj));
                }
                else
                {
                    throw new ArgumentException($"CaseBase 输入的类型重复: {type.Name}", nameof(T));
                }
                return this;
            }
            /// <summary>
            /// 输入类型继承于该类型的情况下执行, 会在Case之后判断及运行
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="doSomething"></param>
            /// <returns></returns>
            public TypeSwitchBuilder CaseBase<T>(Action doSomething)
            {
                if (doSomething == null) throw new ArgumentNullException(nameof(doSomething));
                Type type = typeof(T);
                if (!TypeBaseModeActions.ContainsKey(type))
                {
                    TypeBaseModeActions.Add(type, doSomething);
                }
                else
                {
                    throw new ArgumentException($"CaseBase 输入的类型重复: {type.Name}", nameof(T));
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
            /// <returns>最终是使用什么类型来执行的, 执行了Defalut的话, 则返回null</returns>
            /// <exception cref="ArgumentNullException"></exception>
            public ExceptionInfo Run()
            {
                if (InputType == null) throw new ArgumentNullException(nameof(InputType));

                if (InputObj == null)
                {
                    return RunTypeMode();
                }
                else
                {
                    return RunObjMode();
                }
            }
            private ExceptionInfo RunTypeMode()
            {
                foreach (Type type in TypeModeActions.Keys)
                {
                    if (InputType.Equals(type))
                    {
                        TypeModeActions[type].Invoke();
                        return new ExceptionInfo()
                        {
                            Type = type,
                            ExceptionType = ExceptionTypeEnum.EqualsType
                        };
                    }
                }
                foreach (Type type in TypeBaseModeActions.Keys)
                {
                    if (type.IsAssignableFrom(InputType))
                    {
                        TypeBaseModeActions[type].Invoke();
                        return new ExceptionInfo()
                        {
                            Type = type,
                            ExceptionType = ExceptionTypeEnum.BaseType,
                        };
                    }
                }
                DefaultAction?.Invoke();
                return new ExceptionInfo()
                {
                    Type = null,
                    ExceptionType = ExceptionTypeEnum.Default,
                };
            }
            private ExceptionInfo RunObjMode()
            {
                foreach (Type type in ObjModeActions.Keys)
                {
                    if (InputType.Equals(type))
                    {
                        ObjModeActions[type].Invoke(InputObj);
                        return new ExceptionInfo()
                        {
                            Type = type,
                            ExceptionType = ExceptionTypeEnum.EqualsType
                        };
                    }
                }
                foreach (Type type in ObjBaseModeActions.Keys)
                {
                    if (type.IsAssignableFrom(InputType))
                    {
                        ObjModeActions[type].Invoke(InputObj);
                        return new ExceptionInfo()
                        {
                            Type = type,
                            ExceptionType = ExceptionTypeEnum.BaseType,
                        };
                    }
                }
                DefaultAction?.Invoke();
                return new ExceptionInfo()
                {
                    Type = null,
                    ExceptionType = ExceptionTypeEnum.Default,
                };
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

        #region 属性/字段遍历
        /// <summary>
        /// 类型的属性/字段(公共的)遍历内容的帮助类
        /// </summary>
        public class PropertyErgodicBuilder
        {
            public PropertyErgodicBuilder(Type type)
            {
                TargetType = type;
            }
            #region 设置
            /// <summary>
            /// 遍历的目标类型
            /// </summary>
            public readonly Type TargetType;
            /// <summary>
            /// 目标对象
            /// </summary>
            public object Obj { get; private set; }
            /// <summary>
            /// 遍历范围
            /// </summary>
            public RangeEnum Range { get; private set; } = RangeEnum.All;

            /// <summary>
            /// 遍历范围枚举
            /// </summary>
            public enum RangeEnum
            {
                /// <summary>
                /// 字段及属性
                /// </summary>
                All,
                /// <summary>
                /// 仅属性
                /// </summary>
                OnlyProperty,
                /// <summary>
                /// 仅字段
                /// </summary>
                OnlyField,
            }
            #endregion


            /// <summary>
            /// 设置操作目标对象
            /// </summary>
            /// <param name="obj"></param>
            /// <returns></returns>
            public PropertyErgodicBuilder TargetObj(object obj)
            {
                Obj = obj;
                return this;
            }
            /// <summary>
            /// 设置遍历范围
            /// </summary>
            /// <param name="range"></param>
            /// <returns></returns>
            public PropertyErgodicBuilder SetRange(RangeEnum range)
            {
                Range = range;
                return this;
            }

            #region Actions
            /// <summary>
            /// 不传值也不取值的属性名与方法的字典
            /// </summary>
            private Dictionary<string, Action<Item>> NotValueActions = new Dictionary<string, Action<Item>>();
            /// <summary>
            /// 不传值也不取值的默认方法
            /// </summary>
            private Action<Item> NotValueDefaultAction;
            /// <summary>
            /// 传值的属性名与方法的字典
            /// </summary>
            private Dictionary<string, Action<Item, object>> GetValueActions = new Dictionary<string, Action<Item, object>>();
            /// <summary>
            /// 传值的属性名的默认方法
            /// </summary>
            private Action<Item, object> GetValueDefaultAction;
            /// <summary>
            /// 取值的属性名与方法的字典
            /// </summary>
            private Dictionary<string, Func<Item, SetCheckResult>> SetValueActions = new Dictionary<string, Func<Item, SetCheckResult>>();
            /// <summary>
            /// 取值的属性名的默认方法
            /// </summary>
            private Func<Item, SetCheckResult> SetValueDefaultAction;
            /// <summary>
            /// 修改值的属性名与方法的字典
            /// </summary>
            private Dictionary<string, Func<Item, object, SetCheckResult>> ChangeValueActions = new Dictionary<string, Func<Item, object, SetCheckResult>>();
            /// <summary>
            /// 修改值的属性名的默认方法
            /// </summary>
            private Func<Item, object, SetCheckResult> ChangeValueDefaultAction;

            #region 特定字段
            
            /// <summary>
            /// 将一个方法存入字典的帮助方法
            /// </summary>
            /// <typeparam name="TAction"></typeparam>
            /// <param name="dic">字典</param>
            /// <param name="key">键, 字段名/属性名</param>
            /// <param name="action"></param>
            /// <param name="keyStr">键的参数名字, 用于异常时显示</param>
            /// <param name="actionStr">方法的的参数名字, 用于异常时显示</param>
            private void AddIntoDic<TAction>(Dictionary<string, TAction> dic, string key, TAction action, string keyStr, string actionStr)
            {
                if (action == null) throw new ArgumentNullException(actionStr);
                if (!dic.ContainsKey(key))
                {
                    dic.Add(key, action);
                }
                else
                {
                    throw new ArgumentException($"输入的属性/字段名 {key} 已存在", keyStr);
                }
            }
            /// <summary>
            /// 存在输入名字的属性/字段时, 对该属性/字段做点什么
            /// </summary>
            /// <param name="name"></param>
            /// <param name="doSth"></param>
            /// <returns></returns>
            public PropertyErgodicBuilder Exist(string name, Action<Item> doSth)
            {
                AddIntoDic(NotValueActions, name, doSth, nameof(name), nameof(doSth));
                return this;
            }
            /// <summary>
            /// 存在输入名字的属性/字段时, 且设置的目标对象不为空, 传回属性值, 对该属性/字段做点什么
            /// </summary>
            /// <param name="name"></param>
            /// <param name="doSth"></param>
            /// <returns></returns>
            public PropertyErgodicBuilder ExistGet(string name, Action<Item, object> doSth)
            {
                AddIntoDic(GetValueActions, name, doSth, nameof(name), nameof(doSth));
                return this;
            }
            /// <summary>
            /// 存在输入名字的属性/字段时, 对该属性/字段做点什么, 并由调用方返回是否写入值的信息 (<see cref="SetCheckResult"/>)
            /// </summary>
            /// <param name="name"></param>
            /// <param name="checkFunc"></param>
            /// <returns></returns>
            public PropertyErgodicBuilder ExistSet(string name, Func<Item, SetCheckResult> checkFunc)
            {
                AddIntoDic(SetValueActions, name, checkFunc, nameof(name), nameof(checkFunc));
                return this;
            }
            /// <summary>
            /// 存在输入名字的属性/字段时, 且设置的目标对象不为空, 传回属性值, 对该属性/字段做点什么, 并由调用方返回是否写入值的信息 (<see cref="SetCheckResult"/>)
            /// </summary>
            /// <param name="name"></param>
            /// <param name="checkFunc"></param>
            /// <returns></returns>
            public PropertyErgodicBuilder ExistChange(string name, Func<Item, object, SetCheckResult> checkFunc)
            {
                AddIntoDic(ChangeValueActions, name, checkFunc, nameof(name), nameof(checkFunc));
                return this;
            }
            #region Expression版本, 用起来并没有更方便, 先注释掉了
            /*


            /// <summary>
            /// 存在输入名字的属性/字段时, 对该属性/字段做点什么
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <typeparam name="TProperty"></typeparam>
            /// <param name="getProperty"></param>
            /// <param name="doSth"></param>
            /// <returns></returns>
            public PropertyErgodicBuilder ExistExpr<T, TProperty>(Expression<Func<T, TProperty>> getProperty, Action<Item> doSth)
            {
                AddIntoDic(NotValueActions, GetName(getProperty), doSth, nameof(getProperty), nameof(doSth));
                return this;
            }

            /// <summary>
            /// 存在输入名字的属性/字段时, 且设置的目标对象不为空, 传回属性值, 对该属性/字段做点什么
            /// </summary>
            /// <param name = "name" ></ param >
            /// < param name="doSth"></param>
            /// <returns></returns>
            public PropertyErgodicBuilder ExistExprGet<T, TProperty>(Expression<Func<T, TProperty>> getProperty, Action<Item, object> doSth)
            {
                AddIntoDic(GetValueActions, GetName(getProperty), doSth, nameof(getProperty), nameof(doSth));
                return this;
            }

            /// <summary>
            /// 存在输入名字的属性/字段时, 对该属性/字段做点什么, 并由调用方返回是否写入值的信息 (<see cref="SetCheckResult"/>)
            /// </summary>
            /// <param name="name"></param>
            /// <param name="checkFunc"></param>
            /// <returns></returns>
            public PropertyErgodicBuilder ExistExprSet<T, TProperty>(Expression<Func<T, TProperty>> getProperty, Func<Item, SetCheckResult> checkFunc)
            {
                AddIntoDic(SetValueActions, GetName(getProperty), checkFunc, nameof(getProperty), nameof(checkFunc));
                return this;
            }

            /// <summary>
            /// 存在输入名字的属性/字段时, 且设置的目标对象不为空, 传回属性值, 对该属性/字段做点什么, 并由调用方返回是否写入值的信息 (<see cref="SetCheckResult"/>)
            /// </summary>
            /// <param name="name"></param>
            /// <param name="checkFunc"></param>
            /// <returns></returns>
            public PropertyErgodicBuilder ExistExprChange<T, TProperty>(Expression<Func<T, TProperty>> getProperty, Func<Item, object, SetCheckResult> checkFunc)
            {
                AddIntoDic(ChangeValueActions, GetName(getProperty), checkFunc, nameof(getProperty), nameof(checkFunc));
                return this;
            }

            */
            #endregion


            #endregion

            #region 默认方法
            /// <summary>
            /// 默认情况下对属性做什么
            /// </summary>
            /// <param name="doSth"></param>
            /// <returns></returns>
            public PropertyErgodicBuilder Default(Action<Item> doSth)
            {
                NotValueDefaultAction = doSth;
                return this;
            }
            /// <summary>
            /// 设置的目标对象不为空的默认情况下, 传入值, 并对属性做什么
            /// </summary>
            /// <param name="doSth"></param>
            /// <returns></returns>
            public PropertyErgodicBuilder DefaultGet(Action<Item, object> doSth)
            {
                GetValueDefaultAction = doSth;
                return this;
            }
            /// <summary>
            /// 设置的目标对象不为空的默认情况下对属性做什么, 并由调用方返回是否写入值的信息 (<see cref="SetCheckResult"/>)
            /// </summary>
            /// <param name="doSth"></param>
            /// <returns></returns>
            public PropertyErgodicBuilder DefaultSet(Func<Item, SetCheckResult> doSth)
            {
                SetValueDefaultAction = doSth;
                return this;
            }
            /// <summary>
            /// 设置的目标对象不为空的默认情况下, 传入值, 并对属性做什么, 并由调用方返回是否写入值的信息 (<see cref="SetCheckResult"/>)
            /// </summary>
            /// <param name="doSth"></param>
            /// <returns></returns>
            public PropertyErgodicBuilder DefaultChange(Func<Item, object, SetCheckResult> doSth)
            {
                ChangeValueDefaultAction = doSth;
                return this;
            }
            #endregion


            #endregion
            /// <summary>
            /// 检查是否设置值的返回结果
            /// </summary>
            public struct SetCheckResult
            {
                public bool Set { get; set; }
                public object Value { get; set; }
                /// <summary>
                /// 设置值
                /// </summary>
                /// <param name="obj"></param>
                /// <returns></returns>
                public static SetCheckResult SetObj(object obj)
                {
                    return new SetCheckResult()
                    {
                        Set = true,
                        Value = obj
                    };
                }
                /// <summary>
                /// 不设置值
                /// </summary>
                /// <returns></returns>
                public static SetCheckResult NoSet()
                {
                    return new SetCheckResult()
                    {
                        Set = false,
                        Value = null,
                    };
                }
            }
            public class Item
            {
                /// <summary>
                /// 属性/字段名
                /// </summary>
                public string Name { get; set; }
                /// <summary>
                /// 这是属性
                /// </summary>
                public bool IsProperty { get; set; }
                /// <summary>
                /// 这是字段
                /// </summary>
                public bool IsField { get; set; }

                public PropertyInfo PropertyInfo { get; set; }
                public FieldInfo FieldInfo { get; set; }

                /// <summary>
                /// 取得自定义特性
                /// </summary>
                /// <typeparam name="T"></typeparam>
                /// <returns></returns>
                public T GetCustomAttribute<T>() where T : Attribute
                {
                    return IsField ? FieldInfo.GetCustomAttribute<T>() : PropertyInfo.GetCustomAttribute<T>();
                }

                public static Item Property(PropertyInfo info)
                {
                    return new Item()
                    {
                        Name = info.Name,
                        FieldInfo = null,
                        PropertyInfo = info,
                        IsField = false,
                        IsProperty = true,
                    };
                }
                public static Item Field(FieldInfo info)
                {
                    return new Item()
                    {
                        Name = info.Name,
                        FieldInfo = info,
                        PropertyInfo = null,
                        IsField = true,
                        IsProperty = false,
                    };
                }
            }


            #region 执行
            public void Run()
            {
                switch (Range)
                {
                    case RangeEnum.All:
                        ErgodicProperty();
                        ErgodicField();
                        break;
                    case RangeEnum.OnlyProperty:
                        ErgodicProperty();
                        break;
                    case RangeEnum.OnlyField:
                        ErgodicField();
                        break;
                }
            }
            private void ErgodicProperty()
            {
                PropertyInfo[] propertyInfos = TargetType.GetProperties();
                foreach (PropertyInfo property in propertyInfos)
                {
                    Item item = Item.Property(property);
                    if (NotValueActions.ContainsKey(item.Name))
                    {
                        NotValueActions[item.Name].Invoke(item);
                    }
                    else
                    {
                        NotValueDefaultAction?.Invoke(item);
                    }

                    if (Obj == null) continue;

                    if (GetValueActions.ContainsKey(item.Name))
                    {
                        GetValueActions[item.Name].Invoke(item, property.GetValue(Obj));
                    }
                    else
                    {
                        GetValueDefaultAction?.Invoke(item, property.GetValue(Obj));
                    }

                    if (property.SetMethod == null) continue;

                    if (SetValueActions.ContainsKey(item.Name))
                    {
                        SetCheckResult result = SetValueActions[item.Name].Invoke(item);
                        if (result.Set)
                        {
                            property.SetValue(Obj, result.Value);
                        }
                    }
                    else if (SetValueDefaultAction != null)
                    {
                        SetCheckResult result = SetValueDefaultAction.Invoke(item);
                        if (result.Set)
                        {
                            property.SetValue(Obj, result.Value);
                        }
                    }

                    if (ChangeValueActions.ContainsKey(item.Name))
                    {
                        SetCheckResult result = ChangeValueActions[item.Name].Invoke(item, property.GetValue(Obj));
                        if (result.Set)
                        {
                            property.SetValue(Obj, result.Value);
                        }
                    }
                    else if (ChangeValueDefaultAction != null)
                    {
                        SetCheckResult result = ChangeValueDefaultAction.Invoke(item, property.GetValue(Obj));
                        if (result.Set)
                        {
                            property.SetValue(Obj, result.Value);
                        }
                    }
                }
            }
            private void ErgodicField()
            {
                FieldInfo[] fieldInfos = TargetType.GetFields();
                foreach (FieldInfo field in fieldInfos)
                {
                    Item item = Item.Field(field);
                    if (NotValueActions.ContainsKey(item.Name))
                    {
                        NotValueActions[item.Name].Invoke(item);
                    }
                    else
                    {
                        NotValueDefaultAction?.Invoke(item);
                    }

                    if (Obj == null) continue;

                    if (GetValueActions.ContainsKey(item.Name))
                    {
                        GetValueActions[item.Name].Invoke(item, field.GetValue(Obj));
                    }
                    else
                    {
                        GetValueDefaultAction?.Invoke(item, field.GetValue(Obj));
                    }

                    if (SetValueActions.ContainsKey(item.Name))
                    {
                        SetCheckResult result = SetValueActions[item.Name].Invoke(item);
                        if (result.Set)
                        {
                            field.SetValue(Obj, result.Value);
                        }
                    }
                    else if (SetValueDefaultAction != null)
                    {
                        SetCheckResult result = SetValueDefaultAction.Invoke(item);
                        if (result.Set)
                        {
                            field.SetValue(Obj, result.Value);
                        }
                    }

                    if (ChangeValueActions.ContainsKey(item.Name))
                    {
                        SetCheckResult result = ChangeValueActions[item.Name].Invoke(item, field.GetValue(Obj));
                        if (result.Set)
                        {
                            field.SetValue(Obj, result.Value);
                        }
                    }
                    else if (ChangeValueDefaultAction != null)
                    {
                        SetCheckResult result = ChangeValueDefaultAction.Invoke(item, field.GetValue(Obj));
                        if (result.Set)
                        {
                            field.SetValue(Obj, result.Value);
                        }
                    }
                }
            }
            #endregion
        }

        /// <summary>
        /// 遍历目标属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static PropertyErgodicBuilder ErgodicBuilder<T>(T obj = null)
            where T : class
        {
            return ErgodicBuilder(typeof(T)).TargetObj(obj);

        }
        public static PropertyErgodicBuilder ErgodicBuilder<T>()
        {
            return ErgodicBuilder(typeof(T));
        }
        /// <summary>
        /// 遍历目标类型的属性
        /// </summary>
        /// <param name="type"></param>
        public static PropertyErgodicBuilder ErgodicBuilder(Type type)
        {
            return new PropertyErgodicBuilder(type);
        }
        #endregion
    }
}
