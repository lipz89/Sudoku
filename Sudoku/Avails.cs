using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku2
{
    /// <summary>
    /// 可选序列,封装一个整型的集合
    /// 提供再次打乱序列的方法
    /// </summary>
    public class Avails : List<int>
    {
        #region 私有字段

        private int index = 0;

        #endregion

        #region 属性

        /// <summary>
        /// 获取当前索引
        /// </summary>
        public int Index
        {
            get { return index; }
            set
            {
                if (value >= 0 && value < this.Count)
                    index = value;
            }
        }

        /// <summary>
        /// 获取当前索引对应的值
        /// </summary>
        public int Value
        {
            get { return this[Index]; }
        }

        /// <summary>
        /// 获取当前元素已经是序列最后一个元素
        /// </summary>
        public bool IsLast
        {
            get { return index == this.Count - 1; }
        }

        #endregion

        #region 构造方法

        /// <summary>
        /// 构造方法,初始化为1-9的无重复9个数字随机序列
        /// <param name="count">初始长度</param>
        /// <param name="isOrder">是否有序</param>
        /// </summary>
        public Avails(int count, bool isOrder)
            : base()
        {
            if (isOrder)
            {
                for (int i = 1; i <= count; i++)
                {
                    this.Add(i);
                }
            }
            else
            {
                RandomList(count);
            }
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 利用一个随机序列重新打乱当前集合的顺序
        /// </summary>
        public void Random()
        {
            //随机序列
            Avails rIndex = new Avails(this.Count, false);
            List<int> temp = new List<int>();
            for (int i = 0; i < rIndex.Count; i++)
                temp.Add(this[rIndex[i] - 1]);
            this.Clear();
            this.AddRange(temp);
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 生成1-9的无重复9个数字随机序列
        /// </summary>
        public void RandomList(int count)
        {
            Random rd = new Random(DateTime.Now.Millisecond);
            this.Clear();
            int j = 0;
            while (j < count)
            {
                int v = rd.Next(1, count + 1);
                if (!this.Contains(v))
                {
                    this.Add(v);
                    j++;
                }
            }
            Console.WriteLine(this.ToString());
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

        #endregion
    }
}
