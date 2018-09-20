using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Sudoku2
{
    public partial class FrmGame : Form
    {
        #region 私有字段

        private int level = 0;
        private Thread thLoad;
        private int empty;
        private int timeCount = 0;
        private System.Timers.Timer tmr = new System.Timers.Timer();

        #endregion

        #region 构造方法

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmGame(string file)
        {
            InitializeComponent();

            tmr.AutoReset = true;
            tmr.Interval = 1000;
            tmr.Elapsed += new System.Timers.ElapsedEventHandler(tmr_Elapsed);

            CheckForIllegalCrossThreadCalls = false;
            if (!string.IsNullOrEmpty(file))
            {
                RunAndLoad(file);
            }
            else
            {
                thLoad = new Thread(LoadGame);
                thLoad.Start();
            }
        }
        /// <summary>
        /// 游戏进行时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tmr_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            timeCount++;
            lblTime.Text = "时间　" + (timeCount / 60).ToString("D2") + ":" + (timeCount % 60).ToString("D2");
        }

        #endregion

        #region 私有方法
        /// <summary>
        /// 从文件中加载游戏
        /// </summary>
        /// <param name="fileName"></param>
        private void RunAndLoad(string fileName)
        {
            LoadMatrix(fileName);
            StartGame();
        }
        /// <summary>
        /// 开始游戏
        /// </summary>
        private void StartGame()
        {
            pbStatus.Width = 0;
            lblCreate.Text = "剩余　" + mGame.Empty.ToString("D2");
            lblTime.Visible = true;
            lblTime.Text = "时间　00:00";
            SetUnRe();
            timeCount = 0;
            tmr.Start();
        }

        /// <summary>
        /// 加载游戏
        /// </summary>
        private void LoadGame()
        {
            lblTime.Visible = false;
            pbStatus.Value = 0;
            lblCreate.Text = "正在加载";
            pbStatus.Width = 100;

            menuStrip1.Enabled = false;
            Random rd = new Random();
            empty = rd.Next(7) + 36 + level * 7;
            mGame.LoadGame(empty);
            menuStrip1.Enabled = true;
            StartGame();
            Thread.CurrentThread.Abort();
        }

        /// <summary>
        /// 新游戏菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiNew_Click(object sender, EventArgs e)
        {
            if (Confirm())
            {
                LoadNewGame();
            }
        }
        /// <summary>
        /// 是否放弃当前未完成的游戏
        /// </summary>
        /// <returns></returns>
        private bool Confirm()
        {
            if (!mGame.IsFinished)
                return MessageBox.Show("游戏未完成，是否放弃？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes;
            return true;
        }

        /// <summary>
        /// 游戏菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiDifficult_Click(object sender, EventArgs e)
        {
            if (Confirm())
            {
                level = Convert.ToInt32((sender as ToolStripMenuItem).Tag);
                SetLevel();
                LoadNewGame();
            }
        }
        /// <summary>
        /// 创建新的游戏
        /// </summary>
        private void LoadNewGame()
        {
            thLoad = new Thread(LoadGame);
            thLoad.Start();
        }
        /// <summary>
        /// 游戏难度
        /// </summary>
        private void SetLevel()
        {
            if (level == 0)
            {
                tsmiEasy.Checked = true;
                tsmiNormal.Checked = tsmiDifficult.Checked = false;
                lblDiff.Text = "初级";
            }
            else if (level == 1)
            {
                tsmiEasy.Checked = tsmiDifficult.Checked = false;
                tsmiNormal.Checked = true;
                lblDiff.Text = "中级";
            }
            else if (level == 2)
            {
                tsmiEasy.Checked = tsmiNormal.Checked = false;
                tsmiDifficult.Checked = true;
                lblDiff.Text = "高级";
            }
        }

        /// <summary>
        /// 退出菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 游戏状态改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mGame_Edit(object sender, EventArgs e)
        {
            lblCreate.Text = "剩余　" + mGame.Empty.ToString("D2");
            if (mGame.IsFinished)
            {
                lblCreate.Text = "游戏完成";
                tmr.Stop();
                tsmiReDo.Enabled = false;
                tsmiUnDo.Enabled = false;
            }
            else
            {
                SetUnRe();
            }
        }

        /// <summary>
        /// 游戏加载中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mGame_Creating(object sender, EventArgs e)
        {
            pbStatus.Value++;
            if (pbStatus.Value == pbStatus.Maximum)
                pbStatus.Value = 0;
        }

        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Confirm())
            {
                if (thLoad != null)
                    thLoad.Abort();
            }
            else
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 显示提示菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiClew_Click(object sender, EventArgs e)
        {
            mGame.ShowClew = !mGame.ShowClew;
            tsmiClew.Checked = !tsmiClew.Checked;
        }

        #endregion
        /// <summary>
        /// 加载游戏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;
            ofd.AddExtension = true;
            ofd.DefaultExt = ".sdk";
            ofd.Filter = "数度文件|*.sdk";
            ofd.Title = "选择要打开的数度文件";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                LoadMatrix(ofd.FileName);
            }
        }
        /// <summary>
        /// 从文件中加载游戏
        /// </summary>
        /// <param name="fileName"></param>
        private void LoadMatrix(string fileName)
        {
            try
            {
                FileOperation fo = new FileOperation();
                mGame.LoadMatrix(fo.LoadMatrix(fileName));
                //MessageBox.Show("成功读取数度文件： " + ofd.FileName + " ！", "提示");
                empty = mGame.Empty;
                level = (empty - 36) / 7;
                SetLevel();
                StartGame();
            }
            catch (Exception ex)
            {
                MessageBox.Show("读取数独文件错误：\n" + ex.Message, "提示");
            }
        }
        /// <summary>
        /// 保存游戏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.AddExtension = true;
            sfd.CheckPathExists = true;
            sfd.DefaultExt = ".sdk";
            sfd.Filter = "数度文件|*.sdk";
            sfd.Title = "选择数度保存路径";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileOperation fo = new FileOperation();
                    fo.SaveMatrix(sfd.FileName, mGame.Clone);
                    //MessageBox.Show("成功保存数独为： " + sfd.FileName + " ！", "提示");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("保存数独为文件错误：\n" + ex.Message, "提示");
                }
            }
        }

        private void tsmiHero_Click(object sender, EventArgs e)
        {

        }

        private void tsmiSet_Click(object sender, EventArgs e)
        {

        }

        private void tsmiUnDo_Click(object sender, EventArgs e)
        {
            mGame.UnDo();
            SetUnRe();
        }

        private void tsmiReDo_Click(object sender, EventArgs e)
        {
            mGame.ReDo();
            SetUnRe();
        }

        private void SetUnRe()
        {
            tsmiReDo.Enabled = mGame.CanReDo;
            tsmiUnDo.Enabled = mGame.CanUbDo;
        }
    }
}
