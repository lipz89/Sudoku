namespace Sudoku2
{
    partial class FrmGame
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGame));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmiGame = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNew = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSave = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiEasy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNormal = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDifficult = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiSet = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiHero = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUnDo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiReDo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiClew = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAboutSudoku = new System.Windows.Forms.ToolStripMenuItem();
            this.tssState = new System.Windows.Forms.StatusStrip();
            this.lblDiff = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblCreate = new System.Windows.Forms.ToolStripStatusLabel();
            this.pbStatus = new System.Windows.Forms.ToolStripProgressBar();
            this.lblTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.mGame = new Sudoku2.Game();
            this.menuStrip1.SuspendLayout();
            this.tssState.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiGame,
            this.tsmiEdit,
            this.tsmiAbout});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(289, 25);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsmiGame
            // 
            this.tsmiGame.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiNew,
            this.tsmiLoad,
            this.tsmiSave,
            this.toolStripSeparator1,
            this.tsmiEasy,
            this.tsmiNormal,
            this.tsmiDifficult,
            this.toolStripSeparator3,
            this.tsmiSet,
            this.tsmiHero,
            this.toolStripSeparator2,
            this.tsmiExit});
            this.tsmiGame.Name = "tsmiGame";
            this.tsmiGame.Size = new System.Drawing.Size(61, 21);
            this.tsmiGame.Text = "游戏(&G)";
            // 
            // tsmiNew
            // 
            this.tsmiNew.Name = "tsmiNew";
            this.tsmiNew.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.tsmiNew.Size = new System.Drawing.Size(189, 22);
            this.tsmiNew.Text = "新游戏(&N)";
            this.tsmiNew.Click += new System.EventHandler(this.tsmiNew_Click);
            // 
            // tsmiLoad
            // 
            this.tsmiLoad.Name = "tsmiLoad";
            this.tsmiLoad.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.tsmiLoad.Size = new System.Drawing.Size(189, 22);
            this.tsmiLoad.Text = "读取游戏(&O)";
            this.tsmiLoad.Click += new System.EventHandler(this.tsmiLoad_Click);
            // 
            // tsmiSave
            // 
            this.tsmiSave.Name = "tsmiSave";
            this.tsmiSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.tsmiSave.Size = new System.Drawing.Size(189, 22);
            this.tsmiSave.Text = "保存游戏(&S)";
            this.tsmiSave.Click += new System.EventHandler(this.tsmiSave_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(186, 6);
            // 
            // tsmiEasy
            // 
            this.tsmiEasy.Checked = true;
            this.tsmiEasy.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsmiEasy.Name = "tsmiEasy";
            this.tsmiEasy.Size = new System.Drawing.Size(189, 22);
            this.tsmiEasy.Tag = "0";
            this.tsmiEasy.Text = "初级(&B)";
            this.tsmiEasy.Click += new System.EventHandler(this.tsmiDifficult_Click);
            // 
            // tsmiNormal
            // 
            this.tsmiNormal.Name = "tsmiNormal";
            this.tsmiNormal.Size = new System.Drawing.Size(189, 22);
            this.tsmiNormal.Tag = "1";
            this.tsmiNormal.Text = "中级(&I)";
            this.tsmiNormal.Click += new System.EventHandler(this.tsmiDifficult_Click);
            // 
            // tsmiDifficult
            // 
            this.tsmiDifficult.Name = "tsmiDifficult";
            this.tsmiDifficult.Size = new System.Drawing.Size(189, 22);
            this.tsmiDifficult.Tag = "2";
            this.tsmiDifficult.Text = "高级(&E)";
            this.tsmiDifficult.Click += new System.EventHandler(this.tsmiDifficult_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(186, 6);
            this.toolStripSeparator3.Visible = false;
            // 
            // tsmiSet
            // 
            this.tsmiSet.Name = "tsmiSet";
            this.tsmiSet.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.tsmiSet.Size = new System.Drawing.Size(189, 22);
            this.tsmiSet.Text = "游戏设置(&C)";
            this.tsmiSet.Visible = false;
            this.tsmiSet.Click += new System.EventHandler(this.tsmiSet_Click);
            // 
            // tsmiHero
            // 
            this.tsmiHero.Name = "tsmiHero";
            this.tsmiHero.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.tsmiHero.Size = new System.Drawing.Size(189, 22);
            this.tsmiHero.Text = "高分排行(&H)";
            this.tsmiHero.Visible = false;
            this.tsmiHero.Click += new System.EventHandler(this.tsmiHero_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(186, 6);
            // 
            // tsmiExit
            // 
            this.tsmiExit.Name = "tsmiExit";
            this.tsmiExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.tsmiExit.Size = new System.Drawing.Size(189, 22);
            this.tsmiExit.Text = "退出(&X)";
            this.tsmiExit.Click += new System.EventHandler(this.tsmiExit_Click);
            // 
            // tsmiEdit
            // 
            this.tsmiEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiUnDo,
            this.tsmiReDo,
            this.tsmiClew});
            this.tsmiEdit.Name = "tsmiEdit";
            this.tsmiEdit.Size = new System.Drawing.Size(59, 21);
            this.tsmiEdit.Text = "编辑(&E)";
            // 
            // tsmiUnDo
            // 
            this.tsmiUnDo.Enabled = false;
            this.tsmiUnDo.Name = "tsmiUnDo";
            this.tsmiUnDo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.tsmiUnDo.Size = new System.Drawing.Size(183, 22);
            this.tsmiUnDo.Text = "撤销(&Z)";
            this.tsmiUnDo.Click += new System.EventHandler(this.tsmiUnDo_Click);
            // 
            // tsmiReDo
            // 
            this.tsmiReDo.Enabled = false;
            this.tsmiReDo.Name = "tsmiReDo";
            this.tsmiReDo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.tsmiReDo.Size = new System.Drawing.Size(183, 22);
            this.tsmiReDo.Text = "重做(&Y)";
            this.tsmiReDo.Click += new System.EventHandler(this.tsmiReDo_Click);
            // 
            // tsmiClew
            // 
            this.tsmiClew.Name = "tsmiClew";
            this.tsmiClew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.tsmiClew.Size = new System.Drawing.Size(183, 22);
            this.tsmiClew.Text = "显示提示(&T)";
            this.tsmiClew.Click += new System.EventHandler(this.tsmiClew_Click);
            // 
            // tsmiAbout
            // 
            this.tsmiAbout.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAboutSudoku});
            this.tsmiAbout.Name = "tsmiAbout";
            this.tsmiAbout.Size = new System.Drawing.Size(60, 21);
            this.tsmiAbout.Text = "关于(A)";
            // 
            // tsmiAboutSudoku
            // 
            this.tsmiAboutSudoku.Name = "tsmiAboutSudoku";
            this.tsmiAboutSudoku.Size = new System.Drawing.Size(149, 22);
            this.tsmiAboutSudoku.Text = "关于数独(&A)...";
            // 
            // tssState
            // 
            this.tssState.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblDiff,
            this.lblCreate,
            this.pbStatus,
            this.lblTime});
            this.tssState.Location = new System.Drawing.Point(0, 295);
            this.tssState.Name = "tssState";
            this.tssState.Size = new System.Drawing.Size(289, 26);
            this.tssState.SizingGrip = false;
            this.tssState.TabIndex = 12;
            this.tssState.Text = "0000";
            // 
            // lblDiff
            // 
            this.lblDiff.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.lblDiff.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.lblDiff.Name = "lblDiff";
            this.lblDiff.Size = new System.Drawing.Size(36, 21);
            this.lblDiff.Text = "初级";
            // 
            // lblCreate
            // 
            this.lblCreate.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.lblCreate.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.lblCreate.Name = "lblCreate";
            this.lblCreate.Size = new System.Drawing.Size(60, 21);
            this.lblCreate.Text = "正在加载";
            // 
            // pbStatus
            // 
            this.pbStatus.AutoSize = false;
            this.pbStatus.Maximum = 81;
            this.pbStatus.Name = "pbStatus";
            this.pbStatus.Size = new System.Drawing.Size(100, 20);
            // 
            // lblTime
            // 
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(48, 21);
            this.lblTime.Text = "00：00";
            this.lblTime.Visible = false;
            // 
            // mGame
            // 
            this.mGame.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.mGame.ErrorColor = System.Drawing.Color.Red;
            this.mGame.FocusColor = System.Drawing.Color.White;
            this.mGame.FocusFontColor = System.Drawing.Color.Green;
            this.mGame.FontColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.mGame.Location = new System.Drawing.Point(12, 27);
            this.mGame.Name = "mGame";
            this.mGame.NormalColor = System.Drawing.Color.BurlyWood;
            this.mGame.ReadOnlyColor = System.Drawing.Color.BurlyWood;
            this.mGame.ShowClew = false;
            this.mGame.Size = new System.Drawing.Size(266, 266);
            this.mGame.TabIndex = 13;
            this.mGame.Edit += new System.EventHandler(this.mGame_Edit);
            this.mGame.Creating += new System.EventHandler(this.mGame_Creating);
            // 
            // FrmGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(289, 321);
            this.Controls.Add(this.mGame);
            this.Controls.Add(this.tssState);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "FrmGame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数独";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tssState.ResumeLayout(false);
            this.tssState.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiGame;
        private System.Windows.Forms.ToolStripMenuItem tsmiNew;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiEasy;
        private System.Windows.Forms.ToolStripMenuItem tsmiNormal;
        private System.Windows.Forms.ToolStripMenuItem tsmiDifficult;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tsmiExit;
        private System.Windows.Forms.ToolStripMenuItem tsmiAbout;
        private System.Windows.Forms.ToolStripMenuItem tsmiAboutSudoku;
        private System.Windows.Forms.StatusStrip tssState;
        private System.Windows.Forms.ToolStripStatusLabel lblDiff;
        private System.Windows.Forms.ToolStripStatusLabel lblCreate;
        private System.Windows.Forms.ToolStripProgressBar pbStatus;
        private Game mGame;
        private System.Windows.Forms.ToolStripMenuItem tsmiClew;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem tsmiLoad;
        private System.Windows.Forms.ToolStripMenuItem tsmiSave;
        private System.Windows.Forms.ToolStripStatusLabel lblTime;
        private System.Windows.Forms.ToolStripMenuItem tsmiSet;
        private System.Windows.Forms.ToolStripMenuItem tsmiHero;
        private System.Windows.Forms.ToolStripMenuItem tsmiEdit;
        private System.Windows.Forms.ToolStripMenuItem tsmiUnDo;
        private System.Windows.Forms.ToolStripMenuItem tsmiReDo;
    }
}

