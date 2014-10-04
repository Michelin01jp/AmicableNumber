using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AmicableNumber
{
    class Program
    {
        static List<ulong> AmicableNumbers;

        static void Main(string[] args)
        {
            AmicableNumbers = new List<ulong>();

            StreamWriter sw = new StreamWriter("AmicableNumbers.txt", true, Encoding.GetEncoding("Shift_JIS"));

            Console.CancelKeyPress += (sender, e) =>
            {
                sw.Close();
            };

            for(ulong i = 1; i <= ulong.MaxValue; i++)
            {
                ulong divisorsSum1 = Sum(GetDivisors(i));
                ulong divisorsSum2 = Sum(GetDivisors(divisorsSum1));

                if(i == divisorsSum2 && i != divisorsSum1 && AmicableNumbers.Where(n => n == divisorsSum2).ToArray().Length == 0)
                {
                    AmicableNumbers.Add(divisorsSum2);
                    AmicableNumbers.Add(divisorsSum1);

                    Console.WriteLine("out {0}:{1}", divisorsSum2, divisorsSum1);
                    sw.WriteLine("{0}:{1}", divisorsSum2, divisorsSum1);
                }
            }

            sw.Close();

            return;
        }

        public static List<ulong> GetDivisors(ulong value)
        {
            List<ulong> divisors = new List<ulong>();

            for(ulong i = 1; i < value; i++)
            {
                if(value % i == 0)
                {
                    divisors.Add(i);
                }
            }

            return divisors;
        }

        public static ulong Sum(List<ulong> values)
        {
            ulong x = 0;

            foreach(var n in values)
            {
                x += n;
            }

            return x;
        }
    }
}
