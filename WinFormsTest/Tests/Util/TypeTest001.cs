using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Util;

namespace WinFormsTest.Tests
{
    public partial class TypeTest001 : TestFormBase
    {
        public TypeTest001()
        {
            InitializeComponent();
        }

        public override void TestContent()
        {
            base.TestContent();

            LogColor(nameof(TestClass.INT_NOR), Color.Red);
            LogColor(nameof(TestClass.INT_OR), Color.Blue);
            LogColor(nameof(TestClass.INT_PP), Color.DarkGreen);


            Log("测试1", "开始");
            TestClass temp = new TestClass();
            Type type = typeof(TestClass);
            foreach (FieldInfo fieldInfo in type.GetFields())
            {
                Log(fieldInfo.Name, fieldInfo.GetValue(temp));
                Log(fieldInfo.Name, fieldInfo.IsInitOnly);
                try
                {
                    Log(fieldInfo.Name, "尝试设置值");
                    fieldInfo.SetValue(temp, 15);
                    Log(fieldInfo.Name, "尝试设置成功: " + fieldInfo.GetValue(temp).ToString());
                }
                catch (Exception ex)
                {
                    Log(fieldInfo.Name, ex);
                }
            }
            Log("测试2", "遍历, 类型");
            Action<TypeHelper.PropertyErgodicBuilder.Item> action = (item) =>
            {
                Log(item.Name, "是字段: " + item.IsField);
                Log(item.Name, "是属性: " + item.IsProperty);
                if (item.IsField)
                {
                    Log(item.Name, item.GetCustomAttribute<TestAttribute>());
                }
            };
            TypeHelper.ErgodicBuilder<TestClass>()
                .Exist(nameof(TestClass.INT_OR), action)
                .Exist(nameof(TestClass.INT_NOR), action)
                .Exist(nameof(TestClass.INT_PP), action)
                .Run();
            Log("测试3", "遍历, 对象");
            TestClass temp2 = new TestClass();
            TypeHelper.ErgodicBuilder(temp2)
                .SetRange(TypeHelper.PropertyErgodicBuilder.RangeEnum.All)
                .ExistSet(nameof(TestClass.INT_NOR), (item) =>
                {
                    Log(item.Name, "设置值为100");
                    return TypeHelper.PropertyErgodicBuilder.SetCheckResult.SetObj(100);
                })
                .ExistGet(nameof(TestClass.INT_OR), (item, obj) =>
                {
                    Log(item.Name, "存在旧值: " + obj);
                })
                .ExistChange(nameof(TestClass.INT_PP), (item, obj) =>
                {
                    Log(item.Name, "存在旧值: " + obj);
                    int i = (int)obj;
                    Log(item.Name, "强制转换为int: " + i);
                    i += 11;
                    Log(item.Name, "设置新值: " + i);
                    return TypeHelper.PropertyErgodicBuilder.SetCheckResult.SetObj(i);
                })
                .DefaultChange((item, obj) =>
                {
                    Log(item.Name, "默认情况");
                    if (item.IsProperty && item.PropertyInfo.PropertyType == typeof(string))
                    {
                        Log(item.Name, "是字符串属性: " + obj);
                        return TypeHelper.PropertyErgodicBuilder.SetCheckResult.SetObj(obj + "ver 2.0");
                    }
                    else
                    {
                        return TypeHelper.PropertyErgodicBuilder.SetCheckResult.NoSet();
                    }
                })
                .Run();
            Log("测试3", "遍历后对象的数据: " + Newtonsoft.Json.JsonConvert.SerializeObject(temp2));
            
        }


        public class TestClass
        {
            [Test]
            public int INT_NOR = 5;
            public readonly int INT_OR = 10;

            [Test]
            public int INT_PP { get; private set; } = -5;

            public string? STR_PU_1 { get; set; } = "GET SET";
            public string? STR_PU_2 { get; private set; } = "GET PRIVATE SET";
            public string? STR_PU_3 { get; } = "GET";
            private string? STR_PRI { get; set; } = "???";
        }
        public class TestAttribute : Attribute
        {

        }
    }
}
