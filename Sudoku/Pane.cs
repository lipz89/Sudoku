using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Sudoku2
{
    /// <summary>
    /// 一个数独方格
    /// </summary>
    public sealed partial class Pane : UserControl
    {

        #region 构造方法

        /// <summary>
        /// 构造方法
        /// </summary>
        public Pane()
        {
            InitializeComponent();
        }

        #endregion

        #region 私有字段

        private int number = 0;
        private bool readOnly = false;
        private bool showOptional = false;
        private Avails optional = new Avails(9, true);

        #endregion

        #region 属性

        /// <summary>
        /// 获取或设置数字值
        /// </summary>
        public int Number
        {
            get { return number; }
        }

        /// <summary>
        /// 获取或设置数字值是否只读
        /// </summary>
        public bool ReadOnly
        {
            get { return readOnly; }
            set { readOnly = value; }
        }

        /// <summary>
        /// 获取或设置是否显示可选序列
        /// </summary>
        public bool ShowOptional
        {
            get { return showOptional; }
            set
            {
                showOptional = value;
                //重绘,根据value值绘制或清除可选序列
                if (number == 0)
                {
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// 获取或设置可选序列
        /// </summary>
        public Avails Optional
        {
            get { return optional; }
            set
            {
                optional = value;
                //显示可选序列并且可选序列改变时,重绘控件
                if (number == 0 && showOptional)
                {
                    this.Invalidate();
                }
            }
        }

        #endregion

        #region 委托,事件

        private event EventHandler<NumberChangeEventArgs> numberChanged;

        /// <summary>
        /// 数字值改变时触发的事件
        /// </summary>
        public event EventHandler<NumberChangeEventArgs> NumberChanged
        {
            add { numberChanged += value; }
            remove { numberChanged -= value; }
        }

        #endregion

        #region 公共方法
        /// <summary>
        /// 设置方格的值
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="isKeyEvent">是键盘输入事件</param>
        public void SetNumber(int value, bool isKeyEvent)
        {
            //是允许的值时接收
            if (value >= 0 && value < 10 && value != number)
            {
                number = value;
                //调用事件
                numberChanged(this, new NumberChangeEventArgs(isKeyEvent));
            }
        }

        #endregion

        #region 重写的方法

        /// <summary>
        /// 重写的方法，让控件的keyDown事件处理方向键
        /// </summary>
        /// <param name="msg">通过引用传递的 System.Windows.Forms.Message，它表示要处理的窗口消息。</param>
        /// <param name="keyData">要处理的键</param>
        /// <returns> 如果字符已由控件处理，则为 true；否则为 false。</returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //激发keyDown事件
            OnKeyDown(new KeyEventArgs(keyData));
            //处理方向键
            if (keyData == Keys.Up || keyData == Keys.Left || keyData == Keys.Down || keyData == Keys.Right)
                return true;
            //其他键的处理方法与父类方法一样
            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// 绘制控件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Brush brush = new SolidBrush(ForeColor);
            //禁用状态绘制为灰色
            if (!base.Enabled)
            {
                brush = new SolidBrush(SystemColors.ControlDark);
            }
            if (number > 0)
            {
                g.DrawString(number.ToString(), this.Font, brush, 5, 4);
            }
            else if (showOptional)
            {
                Font f = new Font("宋体", 8);
                //画出可选序列
                for (int i = 0; i < optional.Count; i++)
                {
                    int w = i % 3;
                    int h = i / 3;
                    g.DrawString(optional[i].ToString(), f, brush, w * 8 + 1, h * 8);
                }
            }
            brush.Dispose();
            g.Dispose();
        }

        /// <summary>
        /// 键盘事件,只接收数字键和退格键
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            if (!readOnly)
            {
                //只接收0-9的数字键
                if (e.KeyChar > 47 && e.KeyChar < 58)
                {
                    SetNumber(e.KeyChar - 48, true);
                }
                //为退格键清除值
                else if (e.KeyChar == 8)
                {
                    SetNumber(0, true);
                }
            }
        }

        #endregion
    }

    public class NumberChangeEventArgs : EventArgs
    {
        public NumberChangeEventArgs(bool isKeyEvent)
        {
            this.isKeyEvent = isKeyEvent;
        }

        private bool isKeyEvent;

        public bool IsKeyEvent
        {
            get { return isKeyEvent; }
        }
    }
}
