using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Util.Random;
using WinFormsTest.Model;

namespace WinFormsTest.Tests
{
    public partial class GenericsMethodTest001 : TestFormBase
    {
        public GenericsMethodTest001()
        {
            InitializeComponent();
        }

        public override void TestContent()
        {
            base.TestContent();



            //object result = InvokeGenericFunc(this, TestMethodInfo,
            //    new object[] { RandomObjectHelper.GetObject<TestModel001>() },
            //    typeof(TestModel001));

            // Test test = new Test(this);
            // test.Run();

            void LogDeclaringType<T>(Action<T> action)
            {
                Log("LogDeclaringType", action.Method.DeclaringType);
            }


            LogDeclaringType<MiniModel001>((i) => { Log("测试", i); });
            LogDeclaringType<MiniModel001>((i) => { });

            SetFunc<MiniModel001>((list) =>
            {
                Log("泛型参数", $"list.GetType(): {list.GetType()}");
                string s = Util.String.StringHelper.Concat(list.Select(i => i.ToString()).ToArray());
                Log("Func", s);
                return s;
            });

            List<MiniModel001> randomList = RandomObjectHelper.GetList<MiniModel001>();
            Log("泛型参数", $"randomList.GetType(): {randomList.GetType()}");

            object result = TestMethodInfo!.Invoke(this, new object[] { randomList });

            Log("Done", result);

            StackTrace stack = new StackTrace();
            for (int i = 0; i < stack.FrameCount; i++)
            {
                Log("stack", $"{i}. {stack.GetFrame(i).GetMethod()}");
            }

        }

        private MethodInfo? TestMethodInfo { get; set; }

        private void Action<T>(Action<T> action)
        {
            Log("泛型参数", $"action.Method.DeclaringType: {action.Method.DeclaringType}");
        }
        private void SetFunc<T>(Func<List<T>, string> func)
        {
            Log("泛型参数", $"func.Method.DeclaringType: {func.Method.DeclaringType}");
            Log("泛型参数", $"List<T>: {typeof(List<T>)}");
            Log("泛型参数", $"Func<List<T>, string>: {typeof(Func<List<T>, string>)}");
            Log("泛型参数", $"func.Method: {func.Method}");

            TestMethodInfo = func.Method;
        }
        
        class Test
        {
            private readonly GenericsMethodTest001 form;

            public Test(GenericsMethodTest001 form)
            {
                this.form = form;
            }

            public void Run()
            {
                form.SetFunc<MiniModel001>((list) =>
                {
                    form.Log("泛型参数", $"list.GetType(): {list.GetType()}");
                    string s = Util.String.StringHelper.Concat(list.Select(i => i.ToString()).ToArray());
                    form.Log("Func", s);
                    return s;
                });

            }
        }

    }
}
