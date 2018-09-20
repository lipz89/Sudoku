using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.ComponentModel;

namespace Sudoku2
{
    /// <summary>
    /// 游戏
    /// </summary>
    public class Game : Panel
    {
        #region 私有字段

        private Color fontNormalColor = Color.FromArgb(0, 178, 255);
        private Color backNormalColor = Color.BurlyWood;
        private Color fontFocusColor = Color.DarkGoldenrod;
        private Color backFocusColor = Color.White;
        private Color fontErrorColor = Color.FromArgb(255, 214, 255);
        private Color backOnlyColor = Color.FromArgb(0, 192, 192);
        private Matrix matrix;
        //private int empty;
        private int row = 0, col = 0;
        private List<Pane> errorList = new List<Pane>();
        private bool isFinished = false, showClew = false;
        private Pane[,] panes;
        private int[,] clone;
        private Pane current = null;
        private List<Step> hisStep = new List<Step>();
        private int stepIndex = -1;
        private bool eject = false;
        private FrmEject frmEject;
        private Thread thFinish;
        private bool limit = false;
        private bool isWrite = false;

        #endregion

        #region 构造方法

        /// <summary>
        /// 构造方法
        /// </summary>
        public Game()
        {
            //允许跨线程访问操作
            CheckForIllegalCrossThreadCalls = false;
            this.BackColor = Color.FromArgb(51, 161, 224);
            //加载方格
            LoadPane();
        }

        #endregion

        #region 委托,事件

        private event EventHandler edit;
        private event EventHandler creating;

        /// <summary>
        /// 编辑任何一个格子的内容时触发的事件
        /// </summary>
        public event EventHandler Edit
        {
            add { edit += value; }
            remove { edit -= value; }
        }

        /// <summary>
        /// 数独矩阵创建过程中触发的事件
        /// </summary>
        public event EventHandler Creating
        {
            add { creating += value; }
            remove { creating -= value; }
        }

        #endregion

        #region 属性
        /// <summary>
        /// 可以重做
        /// </summary>
        [Browsable(false)]
        public bool CanReDo
        {
            get { return hisStep.Count > stepIndex + 1; }
        }
        /// <summary>
        /// 可以撤销
        /// </summary>
        [Browsable(false)]
        public bool CanUbDo
        {
            get { return stepIndex > -1; }
        }

        [Browsable(false)]
        public int[,] Clone
        {
            get { return clone; }
        }

        /// <summary>
        /// 获取或设置空格数目
        /// </summary>
        [Browsable(false)]
        public int Empty
        {
            get { return matrix.Empty; }
        }

        /// <summary>
        /// 获取或设置不可编辑格子字体颜色
        /// </summary>
        public Color FontColor
        {
            get { return fontNormalColor; }
            set { fontNormalColor = value; }
        }

        /// <summary>
        /// 获取或设置普通格子背景颜色
        /// </summary>
        public Color NormalColor
        {
            get { return backNormalColor; }
            set { backNormalColor = value; }
        }

        /// <summary>
        /// 获取或设置拥有焦点的格子的背景颜色
        /// </summary>
        public Color FocusColor
        {
            get { return backFocusColor; }
            set { backFocusColor = value; }
        }

        /// <summary>
        /// 获取或设置可编辑格子字体颜色
        /// </summary>
        public Color FocusFontColor
        {
            get { return fontFocusColor; }
            set { fontFocusColor = value; }
        }

        /// <summary>
        /// 获取或设置错误格子字体颜色
        /// </summary>
        public Color ErrorColor
        {
            get { return fontErrorColor; }
            set { fontErrorColor = value; }
        }

        /// <summary>
        /// 获取或设置与当前格子相关联的格子（同行，同列，或同宫的格子）
        /// </summary>
        public Color OnlyColor
        {
            get { return backOnlyColor; }
            set { backOnlyColor = value; }
        }

        /// <summary>
        /// 获取是否完成游戏
        /// </summary>
        [Browsable(false)]
        public bool IsFinished
        {
            get { return isFinished; }
        }

        /// <summary>
        /// 获取或设置游戏是否显示提示
        /// </summary>
        public bool ShowClew
        {
            get { return showClew; }
            set
            {
                showClew = value;
                //改变每个方格是否显示提示
                foreach (Pane pane in panes)
                {
                    pane.ShowOptional = showClew;
                }
            }
        }

        /// <summary>
        /// 获取或设置是否弹出
        /// </summary>
        [Browsable(false)]
        public bool Eject
        {
            get { return eject; }
            set { eject = value; }
        }
        /// <summary>
        /// 获取或设置是否弹出时限制不能输入的数字
        /// </summary>
        [Browsable(false)]
        public bool Limit
        {
            get { return limit; }
            set { limit = value; }
        }

        #endregion

        #region 入口方法

        public void EmptyGame()
        {
            this.Enabled = false;
            PaneLostFocus();
            //创建数独矩阵
            matrix = new Matrix();
            matrix.Creating += new EventHandler(matrix_Creating);
            matrix.CreateEmpty();
            //this.empty = matrix.Empty;
            clone = matrix.CloneMatrix();
            FillPane();
            this.Enabled = true;
            isWrite = true;
        }

        /// <summary>
        /// 创建新的游戏
        /// </summary>
        /// <param name="empty">加载游戏时置空的格子数</param>
        public void LoadGame(int empty)
        {
            this.Enabled = false;
            PaneLostFocus();
            //创建数独矩阵
            matrix = new Matrix();
            matrix.Creating += new EventHandler(matrix_Creating);
            matrix.Create(empty);
            //this.empty = matrix.Empty;
            clone = matrix.CloneMatrix();
            FillPane();
            this.Enabled = true;
            isWrite = false;
        }
        /// <summary>
        /// 从数组中加载游戏
        /// </summary>
        /// <param name="m"></param>
        public void LoadMatrix(int[,] m)
        {
            this.Enabled = false;
            PaneLostFocus();
            matrix = new Matrix();
            matrix.LoadMatrix(m);
            //empty = matrix.Empty;
            clone = matrix.CloneMatrix();
            FillPane();
            this.Enabled = true;
        }

        public void ReDo()
        {
            if (CanReDo)
            {
                Step step = hisStep[stepIndex + 1];
                int r = step.Row, c = step.Col;
                Pane pane = panes[r, c];
                PaneGotFocus(pane, false);
                pane.SetNumber(step.TNumber, false);

                stepIndex++;
            }
        }

        public void UnDo()
        {
            if (CanUbDo)
            {
                Step step = hisStep[stepIndex];
                int r = step.Row, c = step.Col;
                Pane pane = panes[r, c];
                PaneGotFocus(pane, false);
                pane.SetNumber(step.FNumber, false);

                stepIndex--;
            }
        }

        public void Auto()
        {
            if (!isWrite)
            {
                this.matrix.LoadMatrix(this.clone);
            }
            this.matrix.Auto();
            this.FillPane();
        }
        #endregion

        #region 私有方法

        /// <summary>
        /// 加载pane方格
        /// </summary>
        void LoadPane()
        {
            this.Controls.Clear();
            panes = new Pane[9, 9];
            int h = 2;
            for (int r = 0; r < 9; r++)
            {
                int w = 2;
                for (int c = 0; c < 9; c++)
                {
                    Pane pane = new Pane();
                    pane.Location = new Point(c * 29 + w, r * 29 + h);
                    pane.Name = "pane_" + r + "_" + c;
                    pane.Font = new Font("黑体", 15, FontStyle.Bold);
                    //为方格设置事件
                    pane.KeyDown += new KeyEventHandler(pane_KeyDown);
                    pane.KeyPress += new KeyPressEventHandler(pane_KeyPress);
                    pane.NumberChanged += new EventHandler<NumberChangeEventArgs>(pane_NumberChanged);
                    pane.NumberChanged += new EventHandler<NumberChangeEventArgs>(GetOptional);
                    pane.MouseClick += new MouseEventHandler(pane_MouseClick);
                    panes[r, c] = pane;
                    this.Controls.Add(pane);

                    if (c % 3 == 2)
                        w++;
                }
                if (r % 3 == 2)
                    h++;
            }
        }

        /// <summary>
        /// 键盘事件,只接收数字键和退格键
        /// </summary>
        /// <param name="e"></param>
        void pane_KeyPress(object sender, KeyPressEventArgs e)
        {
            Pane pane = sender as Pane;
            if (e.KeyChar > 47 && e.KeyChar < 58)
            {
                pane.SetNumber(e.KeyChar - 48, true);
            }
            //为退格键清除值
            else if (e.KeyChar == 8)
            {
                pane.SetNumber(0, false);
            }
        }

        /// <summary>
        /// 格子拥有焦点时,键盘按键按下,移动焦点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void pane_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PaneGotFocus(panes[row, col], true);
            }
            switch (e.KeyCode)
            {
                case Keys.Up:
                    row = row < 1 ? 8 : row - 1;
                    break;
                case Keys.Left:
                    col = col < 1 ? 8 : col - 1;
                    break;
                case Keys.Down:
                    row = row > 7 ? 0 : row + 1;
                    break;
                case Keys.Right:
                    col = col > 7 ? 0 : col + 1;
                    break;
                default:
                    return;
            }
            PaneGotFocus(panes[row, col], false);
        }

        /// <summary>
        /// 重置方格的可选序列
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void GetOptional(object sender, EventArgs e)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    int r = row / 3 * 3;
                    int c = col / 3 * 3;
                    if (i == row || j == col || (i >= r && i < r + 3 && j >= c && j < c + 3))
                    {
                        panes[i, j].Optional = matrix.GetOptional(i, j);
                    }
                }
            }
        }

        /// <summary>
        /// 格子改变内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void pane_NumberChanged(object sender, NumberChangeEventArgs e)
        {
            if (this.Enabled)
            {
                if (e.IsKeyEvent)
                {
                    matrix[row, col] = e.TNumber;
                    if (e.FNumber == 0 && e.TNumber > 0)
                        matrix.Empty--;
                    else if (e.FNumber > 0 && e.TNumber == 0)
                        matrix.Empty++;
                    if (hisStep.Count > stepIndex + 1)
                    {
                        hisStep.RemoveRange(stepIndex + 1, hisStep.Count - stepIndex - 1);
                    }
                    hisStep.Add(new Step(row, col, e.FNumber, e.TNumber));
                    stepIndex = hisStep.Count - 1;
                }
                CheckError();
                CheckStatu();
                //将pane格子的NumberChanged事件传递给窗体,调用窗体的方法
                edit(this, EventArgs.Empty);
            }
        }
        /// <summary>
        /// 检查对应当前格子的错误格子
        /// </summary>
        private void CheckError()
        {
            CheckPanes(row, col);
            int count = errorList.Count;
            for (int i = 0; i < count; i++)
            {
                String[] ns = errorList[i].Name.Split('_');
                int r = int.Parse(ns[1]);
                int c = int.Parse(ns[2]);
                if (!CheckPanes(r, c))
                {
                    errorList.Remove(errorList[i]);
                    i--;
                    count--;
                }
            }
            ChangePaneColor(panes[row, col].Number);
        }

        private void ChangePaneColor(int number)
        {
            if (number != 0)
            {
                foreach (Pane p in panes)
                {
                    if (errorList.Contains(p))
                        p.ForeColor = fontErrorColor;
                    else if (p.Number == number)
                        p.ForeColor = fontFocusColor;
                    else
                        p.ForeColor = fontNormalColor;
                }
            }
        }
        /// <summary>
        /// 检查对应行列的错误格子
        /// </summary>
        /// <param name="ro"></param>
        /// <param name="co"></param>
        /// <returns></returns>
        private bool CheckPanes(int ro, int co)
        {
            if (panes[ro, co].Number == 0)
                return false;
            bool flag = false;
            for (int r = 0; r < 9; r++)
            {
                if (r != ro && panes[r, co].Number == panes[ro, co].Number)
                {
                    flag = true;
                    if (!errorList.Contains(panes[r, co]))
                        errorList.Add(panes[r, co]);

                }
            }
            for (int c = 0; c < 9; c++)
            {
                if (c != co && panes[ro, c].Number == panes[ro, co].Number)
                {
                    flag = true;
                    if (!errorList.Contains(panes[ro, c]))
                        errorList.Add(panes[ro, c]);

                }
            }
            for (int r = ro / 3 * 3; r < ro / 3 * 3 + 3; r++)
            {
                for (int c = co / 3 * 3; c < co / 3 * 3 + 3; c++)
                {
                    if (r != ro && c != co && panes[r, c].Number == panes[ro, co].Number)
                    {
                        flag = true;
                        if (!errorList.Contains(panes[r, c]))
                            errorList.Add(panes[r, c]);

                    }
                }
            }
            if (flag)
            {
                if (!errorList.Contains(panes[ro, co]))
                    errorList.Add(panes[ro, co]);
            }
            return flag;
        }

        /// <summary>
        /// 将矩阵中的数字填充到格子中
        /// </summary>
        void FillPane()
        {
            isFinished = this.matrix.Empty == 0;
            for (int r = 0; r < 9; r++)
            {
                for (int c = 0; c < 9; c++)
                {
                    Pane pane = panes[r, c];
                    pane.ForeColor = fontNormalColor;
                    if (!isFinished)
                        pane.Optional = matrix.GetOptional(r, c);
                    int number = matrix[r, c];
                    if (clone[r, c] > 0)
                    {
                        pane.BackColor = backOnlyColor;
                        pane.SetNumber(number, false);
                        pane.ReadOnly = true;
                    }
                    else
                    {
                        pane.BackColor = backNormalColor;
                        pane.ReadOnly = false;
                        pane.SetNumber(number, false);
                    }
                }
            }
            if (!isFinished)
            {
                errorList.Clear();
                PaneGotFocus(panes[4, 4], false);
                hisStep.Clear();
                stepIndex = -1;
            }
            else
            {
                thFinish = new Thread(Finish);
                thFinish.Start();
            }
        }

        /// <summary>
        /// 检查状态,检查剩余空格数目,
        /// </summary>
        void CheckStatu()
        {
            //empty = 0;
            //foreach (Pane pane in panes)
            //{
            //    if (pane.Number == 0)
            //        empty++;
            //}
            //完成游戏,调用线程
            if (Empty == 0 && errorList.Count == 0)
            {
                isFinished = true;
                thFinish = new Thread(Finish);
                thFinish.Start();
            }
        }

        /// <summary>
        /// 完成游戏调用线程的方法
        /// </summary>
        void Finish()
        {
            try
            {
                //改变每个格子的字体颜色和背景颜色
                ((FrmGame)this.Parent).menuStrip1.Enabled = false;
                foreach (Pane pane in panes)
                {
                    pane.ReadOnly = true;
                    pane.BackColor = backNormalColor;
                }
                Avails ava = new Avails(81, false);
                //利用一个随机序列改变每个格子的字体颜色
                foreach (int i in ava)
                {
                    int x = (i - 1) / 9;
                    int y = (i - 1) % 9;
                    panes[x, y].ForeColor = GerColor(fontNormalColor, 96);
                    Thread.Sleep(30);
                }
                ava.Random();
                //利用另一个随机序列改变每个格子的背景颜色
                foreach (int i in ava)
                {
                    int x = (i - 1) / 9;
                    int y = (i - 1) % 9;
                    panes[x, y].BackColor = GerColor(backNormalColor, -48);
                    Thread.Sleep(30);
                }
                ((FrmGame)this.Parent).menuStrip1.Enabled = true;
                Thread.CurrentThread.Abort();
                Thread.CurrentThread.Join();
            }
            catch { }
        }

        /// <summary>
        /// 获取一个颜色
        /// </summary>
        /// <param name="bc">基础颜色</param>
        /// <param name="degree">分值改变数,正数位加,负数为减</param>
        /// <returns>放回颜色</returns>
        Color GerColor(Color bc, int degree)
        {
            int r = Math.Min(Math.Max(bc.R + degree, 10), 255);
            int g = Math.Min(Math.Max(bc.G + degree, 10), 255);
            int b = Math.Min(Math.Max(bc.B + degree, 10), 255);
            return Color.FromArgb(r, g, b);
        }
        /// <summary>
        /// 点击格子获取焦点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void pane_MouseClick(object sender, MouseEventArgs e)
        {
            if (isFinished)
                return;
            PaneGotFocus(sender as Pane, e.Button == MouseButtons.Left);
        }
        /// <summary>
        /// 设置焦点到目标格子上
        /// </summary>
        /// <param name="pane"></param>
        void PaneGotFocus(Pane pane, bool isUser)
        {
            if (isFinished || pane == null)
                return;
            PaneLostFocus();
            String[] ns = pane.Name.Split('_');
            row = int.Parse(ns[1]);
            col = int.Parse(ns[2]);
            pane.Focus();
            current = pane;

            ChangePaneColor(pane.Number);
            pane.BackColor = backFocusColor;

            if (eject && !pane.ReadOnly && isUser)
            {
                if (frmEject == null)
                {
                    frmEject = new FrmEject(this.FindForm());
                }
                frmEject.Show(pane, this.limit);
            }
        }
        /// <summary>
        /// 将当前格子的焦点移除
        /// </summary>
        void PaneLostFocus()
        {
            if (isFinished || current == null)
                return;
            if (current.ReadOnly)
            {
                current.BackColor = backOnlyColor;
            }
            else
            {
                current.BackColor = backNormalColor;
            }
        }

        /// <summary>
        /// 数独创建事件传递到窗体,数独的创建激发窗体中的方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void matrix_Creating(object sender, EventArgs e)
        {
            creating(sender, e);
        }

        #endregion

        #region 重写的方法

        /// <summary>
        /// 重写绘制控件方法,区分启用和禁用状态
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            if (Enabled)
            {
                g.Clear(BackColor);
            }
            else
            {
                g.Clear(SystemColors.ControlDark);
            }
        }

        #endregion
    }

    struct Step
    {
        public Step(int r, int c, int fn, int tn)
        {
            this.row = r;
            this.col = c;
            this.fNumber = fn;
            this.tNumber = tn;
        }

        private int row;
        private int col;
        private int fNumber;
        private int tNumber;

        public int TNumber
        {
            get { return tNumber; }
        }

        public int Row
        {
            get { return row; }
        }

        public int Col
        {
            get { return col; }
        }

        public int FNumber
        {
            get { return fNumber; }
        }
    }
}
