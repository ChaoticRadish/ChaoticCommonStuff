using ChaoticWinformControl;
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

namespace WinFormsTest.Tests
{
    public partial class AssemblyTest001 : TestFormBase
    {
        public AssemblyTest001()
        {
            InitializeComponent();
        }

        public override void TestContent()
        {
            base.TestContent();

            Type[] allType = Assembly.GetExecutingAssembly().GetTypes();

            Task.Run(() =>
            {
                foreach (Type type in allType)
                {
                    this.AutoInvoke(() =>
                    {
                        if (type.IsEnum)
                        {
                            Log(阶段.所有类型_枚举, type.FullName, Color.Red);
                        }
                        else if (type.IsGenericType)
                        {
                            Log(阶段.所有类型_泛型, type.FullName, Color.Orange);
                        }
                        else if (typeof(Form).IsAssignableFrom(type))
                        {
                            Log(阶段.所有类型_窗口, type.FullName, Color.Blue);
                        }
                        else
                        {
                            Log(阶段.所有类型_一般, type.FullName, Color.Green);
                        }
                    });
                    Thread.Sleep(50);
                }
            });
        }

        enum 阶段
        {
            所有类型_窗口,
            所有类型_枚举,
            所有类型_泛型,
            所有类型_一般
        }
    }
}
