using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test
{
    class Ava : List<int>
    {        /// <summary>
        /// 生成1-9的无重复9个数字随机序列
        /// </summary>
        public void RandomList(int count)
        {
            Random rd = new Random(DateTime.Now.Millisecond);
            this.Clear();
            int j = 0;
            while (j < count)
            {
                int v = rd.Next(count) + 1;
                if (!this.Contains(v))
                {
                    this.Add(v);
                    j++;
                }
            }
            //Console.WriteLine(this.ToString());
        }

        public override string ToString()
        {
            string str = "{";
            for (int i = 0; i < this.Count; i++)
            {
                str += this[i] + ",";
            }
            return str.Substring(0, str.Length - 1) + "}";
        }

        public void Las_vegas(int count, int n)
        {
            Random rd = new Random(DateTime.Now.Millisecond);
            this.Clear();
            int i = 0;
            for (i = 0; i < count; i++)
            {
                this.Add(0);
            }
            int j = 0;
            while (j < n)
            {
                int index = 0, v = 0;
                do
                {
                    index = rd.Next(count);
                } while (this[index] != 0);
                do
                {
                    v = rd.Next(count) + 1;
                } while (this.Contains(v));
                this[index] = v;
                j++;
            }
            for (j = 0, i = 1; j < count; )
            {
                if (this[j] != 0)
                {
                    j++;
                }
                else
                {
                    if (this.Contains(i))
                    {
                        i++;
                    }
                    else
                    {
                        this[j++] = i;
                    }
                }
            }
            //Console.WriteLine(this.ToString());
        }
        public void Las_vegas2(int count, int n)
        {
            Random rd = new Random(DateTime.Now.Millisecond);
            this.Clear();
            int j = 0;
            while (j < n)
            {
                int v = rd.Next(count) + 1;
                if (!this.Contains(v))
                {
                    this.Add(v);
                    j++;
                }
            }
            for (int i = 1; i <= count; i++)
            {
                if (!this.Contains(i))
                {
                    this.Add(i);
                }
            }
            //Console.WriteLine(this.ToString());
        }
    }
}
