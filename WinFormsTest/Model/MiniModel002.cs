using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsTest.Model
{
    /// <summary>
    /// 非常小巧的测试实体
    /// </summary>
    internal class MiniModel002
    {
        public int Id { get; set; }
        public char A { get; set; }
        public char B { get; set; }
        public char C { get; set; }

        public override string ToString()
        {
            string output = $"{Id}. [{A}] [{B}] [{C}]";
            return output;
        }
        public static List<MiniModel002> Random(int minCount = 10, int maxCount = 20)
        {
            return Util.Random.RandomObjectHelper.GetList<MiniModel002>(null, minCount, maxCount);
        }
    }
}
