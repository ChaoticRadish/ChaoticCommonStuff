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
    internal class MiniModel001
    {
        public int Id { get; set; }
        public string? Str { get; set; }

        public override string ToString()
        {
            string output = Id + ". ";
            if (Str == null) output += "null";
            else
            {
                output += $"[Length {Str.Length} ]";
                if (Str.Length > 10)
                {
                    output += Str.Substring(0, 7) + "...";
                }
                else
                {
                    output += Str;
                }
            }
            return output;
        }
        public static List<MiniModel001> Random(int minCount = 10, int maxCount = 20)
        {
            return Util.Random.RandomObjectHelper.GetList<MiniModel001>(null, minCount, maxCount);
        }
    }
}
