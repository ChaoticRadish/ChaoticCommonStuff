using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsTest.Tests
{
    public partial class SwitchTypeTest001 : TestFormBase
    {
        public SwitchTypeTest001()
        {
            InitializeComponent();
        }

        public override void TestContent()
        {
            base.TestContent();

            List<Type> waitCheckType = new List<Type>()
            {
                typeof(string), typeof(SwitchTypeTest001), typeof(int), typeof(short), typeof(内容), typeof(List<string>)
            };
            List<object> waitCheckObj = new List<object>()
            {
                "111", 222, 33u, 44L, 0x555, true, this, 内容.对象模式, new { index = 1 }, new List<string>(){"111", "22", "3"}
            };

            Log(内容.类型模式, "开始");
            foreach (Type type in waitCheckType)
            {
                Util.TypeHelper.SwitchType(type)
                    .Case<int>(() =>
                    {
                        Log(内容.类型模式,  "输入: " + type.FullName + " 执行过程: " + typeof(int).FullName);
                    })
                    .Case<string>(() =>
                    {
                        Log(内容.类型模式, "输入: " + type.FullName + " 执行过程: " + typeof(string).FullName);
                    })
                    .Case<bool>(() =>
                    {
                        Log(内容.类型模式, "输入: " + type.FullName + " 执行过程: " + typeof(bool).FullName);
                    })
                    .Case<short>(() =>
                    {
                        Log(内容.类型模式, "输入: " + type.FullName + " 执行过程: " + typeof(short).FullName);
                    })
                    .Case<内容>(() =>
                    {
                        Log(内容.类型模式, "输入: " + type.FullName + " 执行过程: " + typeof(内容).FullName);
                    })
                    .Default(() =>
                    {
                        Log(内容.类型模式, "默认调用");
                    })
                    .Run();
            }
            Log(内容.对象模式, "开始");
            foreach (object forObj in waitCheckObj)
            {
                Util.TypeHelper.TypeSwitchBuilder.ExceptionInfo exceInfo = Util.TypeHelper.SwitchType(forObj)
                    .Case<int>((obj) =>
                    {
                        Log(内容.类型模式, "输入: " + forObj.GetType().FullName + " 执行过程: " + obj.GetType().FullName + ": " + obj.ToString());
                    })
                    .Case<string>((obj) =>
                    {
                        Log(内容.类型模式, "输入: " + forObj.GetType().FullName + " 执行过程: " + obj.GetType().FullName + ": " + obj.ToString());
                    })
                    .Case<bool>((obj) =>
                    {
                        Log(内容.类型模式, "输入: " + forObj.GetType().FullName + " 执行过程: " + obj.GetType().FullName + ": " + obj.ToString());
                    })
                    .Case<short>((obj) =>
                    {
                        Log(内容.类型模式, "输入: " + forObj.GetType().FullName + " 执行过程: " + obj.GetType().FullName + ": " + obj.ToString());
                    })
                    .Case<内容>((obj) =>
                    {
                        Log(内容.类型模式, "输入: " + forObj.GetType().FullName + " 执行过程: " + obj.GetType().FullName + ": " + obj.ToString());
                    })
                    /*.Default(() =>
                    {
                        Log(内容.类型模式, "默认调用: " + forObj.GetType().FullName + " 值: "+ forObj.ToString());
                    })*/ 
                    .Run();
                Log(内容.类型模式, $"执行了: {exceInfo}");
            }
        }
        public enum 内容
        {
            类型模式,
            对象模式
        }
    }
}
