using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsTest.Model
{
    /// <summary>
    /// 通用的测试类型实体, 包含常见基本类型的属性, 各一个
    /// </summary>
    internal class TestModel001
    {
        public int ValueInt { get; set; }
        public long ValueLong { get; set; }
        public short ValueShort { get; set; }
        public bool ValueBool { get; set; }
        public byte ValueByte { get; set; }
        public char ValueChar { get; set; }
        public double ValueDouble { get; set; }
        public float ValueFloat { get; set; }
        public string? ValueString { get; set; }

        public static List<TestModel001> Random(int minCount = 10, int maxCount = 20)
        {
            return Util.Random.RandomObjectHelper.GetList<TestModel001>(null, minCount, maxCount);
        }
    }
}
