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
        static List<ulong> AmicableNumbers = new List<ulong>();
        static string FileName = "AmicableNumbers.txt";

        static void Main(string[] args)
        {
            Load();
            
            StreamWriter sw = new StreamWriter(FileName, true, Encoding.GetEncoding("Shift_JIS"));

            Console.CancelKeyPress += (sender, e) =>
            {
                sw.Close();
            };

            for (ulong i = AmicableNumbers.Count == 0 ? 1ul : AmicableNumbers[AmicableNumbers.Count - 2]; i <= ulong.MaxValue; i++)
            {
                ulong divisorsSum1 = GetDivisorSum(i);
                ulong divisorsSum2 = GetDivisorSum(divisorsSum1);

                if(i == divisorsSum2 && i != divisorsSum1 && AmicableNumbers.Where(n => n == divisorsSum2).ToArray().Length == 0)
                {
                    AmicableNumbers.Add(divisorsSum2);
                    AmicableNumbers.Add(divisorsSum1);

                    Console.WriteLine("{0} {1}:{2}", DateTime.Now.ToString("HH:mm:ss"), divisorsSum2, divisorsSum1);
                    sw.WriteLine("{0}:{1}", divisorsSum2, divisorsSum1);
                }
            }

            sw.Close();

            return;
        }

        public static void Load()
        {
            if (File.Exists(FileName))
            {
                using (var sr = new StreamReader(FileName))
                {
                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        var str = line.Split(':');

                        str.Select((n, m) => { AmicableNumbers.Add(ulong.Parse(n)); Console.Write(n); if (m == 0) Console.Write(":"); m++; return n; }).ToArray();

                        Console.WriteLine();
                    }
                }
            }
        }

        public static ulong GetDivisorSum(ulong value)
        {
            ulong divisorSum = 0;

            for(ulong i = 1; i < value; i++)
            {
                if(value % i == 0)
                {
                    divisorSum += i;
                }
            }

            return divisorSum;
        }
    }
}
