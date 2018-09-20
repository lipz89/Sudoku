using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Sudoku2
{
    /// <summary>
    /// 数独核心类
    /// 其实就是一个封装的矩阵
    /// 其中包括构造一个符合数独规则的矩阵,验证行列宫
    /// 提供为一个被置空的格子提供可选序列
    /// </summary>
    public class Matrix
    {
        #region 私有字段

        private int[,] matrix;
        private int[] binRow;
        private int[] binCol;
        private int[] binNine;
        private bool isError = false;
        private Avails[,] avas;
        private int answerCount = 0;
        private Avails avaFull;
        private double diff = 0;
        private int empty = 0;

        #endregion

        #region 构造方法

        /// <summary>
        /// 构造方法
        /// </summary>
        public Matrix()
        {
            matrix = new int[9, 9];
            binRow = new int[9];
            binCol = new int[9];
            binNine = new int[9];
        }

        #endregion

        #region 属性,索引器

        /// <summary>
        /// 获取上一次对本数独元素的赋值是否有错误
        /// </summary>
        public bool IsError
        {
            get { return isError; }
            set { isError = value; }
        }

        /// <summary>
        /// 索引器,获取或设置指定行列的值
        /// </summary>
        /// <param name="r">行</param>
        /// <param name="c">列</param>
        /// <returns>指定行列的值</returns>
        public int this[int r, int c]
        {
            get { return matrix[r, c]; }
            set
            {
                int n = r / 3 * 3 + c / 3;
                //将值写入二进制状态,该过程中验证该值是否在行列宫冲突,如果冲突,该值不被写入二进制状态
                FillValue(r, c, n, value);
            }
        }

        /// <summary>
        /// 难度系数
        /// </summary>
        public double Diff
        {
            get { return diff; }
            set { diff = value; }
        }
        /// <summary>
        /// 数独的空格数
        /// </summary>
        public int Empty
        {
            get { return empty; }
            set { empty = value; }
        }

        #endregion

        #region 委托,事件

        private event EventHandler creating;

        /// <summary>
        /// 创建数独矩阵时发生的事件
        /// </summary>
        public event EventHandler Creating
        {
            add { creating += value; }
            remove { creating -= value; }
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 创建数独,并将部分格子置空
        /// </summary>
        /// <param name="num"></param>
        public void Create(int empty)
        {
            matrix = new int[9, 9];
            this.empty = empty;
            //生成一个数独
            CreateMatrix();
            //PrintMatrix();
            //实例一个无序的序列
            EmptyMatrix();
        }
        /// <summary>
        /// 从数组中加载游戏
        /// </summary>
        /// <param name="m"></param>
        public void LoadMatrix(int[,] m)
        {
            empty = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    this[i, j] = m[i, j];
                    if (m[i, j] == 0)
                        empty++;
                }
            }
        }
        /// <summary>
        /// 复制游戏的初始化状态
        /// </summary>
        /// <returns></returns>
        public int[,] CloneMatrix()
        {
            int empty = 0;
            int[,] clone = new int[9, 9];
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    clone[i, j] = matrix[i, j];
                    if (this[i, j] == 0)
                        empty++;
                }
            }
            return clone;
        }

        /// <summary>
        /// 获取指定行列的可选序列,该序列中的值不与所在行列宫中的任何一个值冲突
        /// </summary>
        /// <param name="r"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public Avails GetOptional(int r, int c)
        {
            //初始化一个随机的可选序列,随机是为了防止多次生成一样的数独
            Avails ava = new Avails(9, true);
            //将本行出现过的值从序列中删除
            for (var i = 0; i < 9; i++)
            {
                if (i == c)
                    continue;
                if (ava.Contains(matrix[r, i]))
                    ava.Remove(matrix[r, i]);
            }
            //将本列出现过的值从序列中删除
            for (var i = 0; i < 9; i++)
            {
                if (i == r)
                    continue;
                if (ava.Contains(matrix[i, c]))
                    ava.Remove(matrix[i, c]);
            }
            //将本宫出现过的值从序列中删除
            for (int i = r / 3 * 3; i < r / 3 * 3 + 3; i++)
            {
                for (int j = c / 3 * 3; j < c / 3 * 3 + 3; j++)
                {
                    if (i == r && j == c)
                    {
                        continue;
                    }
                    if (ava.Contains(matrix[i, j]))
                        ava.Remove(matrix[i, j]);
                }
            }
            return ava;
        }

        #endregion

        #region 私有方法
        /// <summary>
        /// 挖洞操作
        /// </summary>
        private void EmptyMatrix()
        {
            avaFull = new Avails(81, false);

            for (int i = 0; i < empty; )
            {
                for (int j = 0; j < avaFull.Count; j++)
                {
                    int v = avaFull[j] - 1;
                    int r = v / 9;
                    int c = v % 9;
                    int t = matrix[r, c];
                    this[r, c] = 0;
                    avaFull.Remove(v + 1);
                    if (i > 3 && GetAnswerCount() > 1)
                    {
                        avaFull.Insert(j, v + 1);
                        this[r, c] = t;
                    }
                    else
                    {
                        i++;
                        break;
                    }
                    creating(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// 打印数独
        /// </summary>
        private void PrintMatrix()
        {
            for (int c = 0; c < 9; c++)
            {
                if (c == 0)
                    Console.WriteLine("┏━┯━┯━┳━┯━┯━┳━┯━┯━┓");
                else if (c % 3 == 0)
                    Console.WriteLine("┣━┿━┿━╋━┿━┿━╋━┿━┿━┫");
                else
                    Console.WriteLine("┠─┼─┼─╂─┼─┼─╂─┼─┼─┨");
                for (int r = 0; r < 9; r++)
                {
                    Console.Write(r % 3 == 0 ? "┃" : "│");
                    Console.Write(matrix[c, r] == 0 ? "  " : " " + (matrix[c, r]));
                }
                Console.WriteLine("┃");
                if (c == 8)
                    Console.WriteLine("┗━┷━┷━┻━┷━┷━┻━┷━┷━┛");
            }
        }

        /// <summary>
        /// 用递归尝试填充单元格
        /// </summary>
        /// <param name="rc"></param>
        private void GetRCValue(int rc)
        {
            if (rc >= 80)
            {
                answerCount++;
                return;
            }
            if (avaFull.Contains(rc + 1))
            {
                GetRCValue(rc + 1);
            }
            else
            {
                int r = rc / 9;
                int c = rc % 9;
                Avails ava = GetOptional(r, c);
                for (int i = 0; i < ava.Count; i++)
                {
                    int value = ava[i];
                    this[r, c] = value;
                    if (!isError)
                    {
                        GetRCValue(rc + 1);
                        this[r, c] = 0;
                    }
                    else
                    {
                        GetRCValue(rc + 1);
                    }
                }
            }
        }

        /// <summary>
        /// 计算数度正解数
        /// </summary>
        /// <returns></returns>
        private int GetAnswerCount()
        {
            answerCount = 0;
            GetRCValue(0);
            return answerCount;
        }

        /// <summary>
        /// 利用回溯生成符合规则的数独
        /// </summary>
        private void CreateMatrix()
        {
            //初始化81个可选序列,对应81个矩阵元素
            avas = new Avails[9, 9];
            isError = false;
            int rc = 0;
            while (rc < 81)
            {
                int r = rc / 9;
                int c = rc % 9;
                //获取一个可选值
                int value = RandInt(r, c);
                //值不为0
                if (value > 0)
                {
                    this[r, c] = value;
                    rc++;
                }
                //可选值为0,回溯
                else
                {
                    rc--;
                }
                creating(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// 为指定行列选择一个可选的值
        /// </summary>
        /// <param name="r">行</param>
        /// <param name="c">列</param>
        /// <returns>可选值</returns>
        private int RandInt(int r, int c)
        {
            if (matrix[r, c] == 0)
            {
                //初始化一个随机的可选序列,随机是为了防止多次生成一样的数独
                avas[r, c] = new Avails(9, false);
                //将本行出现在本列之前的值从序列中删除
                for (var i = 0; i < 9; i++)
                    if (avas[r, c].Contains(matrix[r, i]))
                        avas[r, c].Remove(matrix[r, i]);
                //将本列出现在本行之上的值从序列中删除
                for (var i = 0; i < 9; i++)
                    if (avas[r, c].Contains(matrix[i, c]))
                        avas[r, c].Remove(matrix[i, c]);
                //将本宫出现在本格子之前的值从序列中删除
                for (int i = r / 3 * 3; i < r / 3 * 3 + 3; i++)
                    for (int j = c / 3 * 3; j < c / 3 * 3 + 3; j++)
                    {
                        //只需要遍历到当前格子
                        if (i == r && j == c)
                        {
                            i = 10;
                            j = 10;
                            break;
                        }
                        if (avas[r, c].Contains(matrix[i, j]))
                            avas[r, c].Remove(matrix[i, j]);
                    }
                //可选序列为空,返回 0
                if (avas[r, c].Count == 0)
                    return 0;
                //avas[r, c].Random();
            }
            else
            {
                //上次选择的是最后一个可选值,将值清空,返回 0
                if (avas[r, c].IsLast)
                {
                    this[r, c] = 0;
                    return 0;
                }
                //回溯的时候从可选序列中选择下一个值
                avas[r, c].Index++;
            }
            //返回可选序列当前选定值
            return avas[r, c].Value;
        }

        #region 位运算操作

        /// <summary>
        /// 用位运算 并 将指定值从所在的行列宫二进制状态中清空
        /// </summary>
        /// <param name="r">所在行</param>
        /// <param name="c">所在列</param>
        /// <param name="n">所在宫</param>
        private void ClearValue(int r, int c, int n)
        {
            isError = false;
            int v = matrix[r, c];
            binRow[r] &= ~(1 << (v - 1));
            binCol[c] &= ~(1 << (v - 1));
            binNine[n] &= ~(1 << (v - 1));
            matrix[r, c] = 0;
        }

        /// <summary>
        /// 用位运算 或 将指定值放进所在行列宫二进制状态中
        /// </summary>
        /// <param name="r">所在行</param>
        /// <param name="c">所在列</param>
        /// <param name="n">所在宫</param>
        /// <param name="v">要放进的值</param>
        private void FillValue(int r, int c, int n, int v)
        {
            //清空该格子
            ClearValue(r, c, n);
            if (v > 0 && v < 10)
            {
                //验证要填入的数字是否被允许
                Valid(r, c, n, v);
                //不被允许则不填入
                if (isError)
                    return;
                binRow[r] |= (1 << (v - 1));
                binCol[c] |= (1 << (v - 1));
                binNine[n] |= (1 << (v - 1));
                matrix[r, c] = v;
            }
        }

        /// <summary>
        /// 验证本次赋值操作是否有误
        /// </summary>
        /// <param name="r">行</param>
        /// <param name="c">列</param>
        /// <param name="n">宫</param>
        /// <param name="value">值</param>
        private void Valid(int r, int c, int n, int value)
        {
            isError = !(ValidRow(r, value) && ValidCol(c, value) && ValidNine(n, value));
        }

        /// <summary>
        /// 用位运算 异或 判断指定行是否不存在指定值
        /// </summary>
        /// <param name="r">行</param>
        /// <param name="value">值</param>
        /// <returns>TRUE表示不存在,FALSE表示存在 </returns>
        private bool ValidRow(int r, int value)
        {
            return ((binRow[r] & (1 << (value - 1))) >> (value - 1) == 0);
        }

        /// <summary>
        /// 用位运算 异或 判断指定列是否不存在指定值
        /// </summary>
        /// <param name="c">列</param>
        /// <param name="value">值</param>
        /// <returns>TRUE表示不存在,FALSE表示存在 </returns>
        private bool ValidCol(int c, int value)
        {
            return ((binCol[c] & (1 << (value - 1))) >> (value - 1) == 0);
        }

        /// <summary>
        /// 用位运算 异或 判断指定宫是否不存在指定值
        /// </summary>
        /// <param name="n">宫</param>
        /// <param name="value">值</param>
        /// <returns>TRUE表示不存在,FALSE表示存在 </returns>
        private bool ValidNine(int n, int value)
        {
            return ((binNine[n] & (1 << (value - 1))) >> (value - 1) == 0);
        }

        #endregion

        #endregion
    }
}
