using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //Compare();
            //GetMinSNum(81);
            Matrix matrix = new Matrix();
            matrix.Create(40);
            Console.WriteLine("解法:" + matrix.GetAnswerCount());
            //matrix.CreateLas_vegas(0);
            Console.Read();
        }

        private static void GetMinSNum(int num)
        {
            Ava ava = new Ava();
            for (int i = 0; i < num; i++)
            {
                DateTime dt = DateTime.Now;
                for (int j = 0; j < 1000; j++)
                {
                    ava.Las_vegas2(num, i);
                }
                DateTime dt1 = DateTime.Now;
                Console.WriteLine(i + ":   " + dt1.Subtract(dt).TotalMilliseconds);
            }
            Console.Read();
        }

        private static void Compare()
        {
            for (int i = 0; i < 100; i++)
            {
                NewMethod(81, 11);
            }
            Console.Read();
        }

        private static void NewMethod(int num, int snum)
        {
            Ava ava = new Ava();
            int count = 600;
            DateTime dt = DateTime.Now;
            for (int i = 0; i < count; i++)
            {
                ava.RandomList(num);
            }
            DateTime dt1 = DateTime.Now;
            for (int i = 0; i < count; i++)
            {
                ava.Las_vegas(num, snum);
            }
            DateTime dt2 = DateTime.Now;
            for (int i = 0; i < count; i++)
            {
                ava.Las_vegas2(num, snum);
            }
            DateTime dt3 = DateTime.Now;

            Console.WriteLine(dt1.Subtract(dt).TotalMilliseconds + "---" + dt2.Subtract(dt1).TotalMilliseconds + "---" + dt3.Subtract(dt2).TotalMilliseconds);
        }
    }
}
