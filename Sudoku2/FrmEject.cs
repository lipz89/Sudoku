using System;
using System.Drawing;
using System.Windows.Forms;

namespace Sudoku2
{
    public partial class FrmEject : Form
    {
        private Pane pane;

        public FrmEject(Form owner)
        {
            InitializeComponent();
            this.Owner = owner;
        }

        public void Show(Pane p, bool limit)
        {
            int x = this.Owner.Left + (this.Owner.Width - this.Width) / 2;
            int y = this.Owner.Top + (this.Owner.Height - this.Height) / 2;
            this.Location = new Point(x, y);
            this.pane = p;
            for (int i = 1; i <= 9; i++)
            {
                this.panel1.Controls["button" + i].Visible = !limit || p.Optional.Contains(i);
            }
            this.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string txt = btn.Text;
            if (txt != "关闭")
            {
                int Number = 0;
                if (int.TryParse(txt, out Number))
                {
                    pane.SetNumber(Number, true);
                    pane.Invalidate();
                }
            }
            this.Visible = false;
        }

        private void FrmEject_Leave(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void FrmEject_KeyPress(object sender, KeyPressEventArgs e)
        {
            //只接收0-9的数字键
            if (e.KeyChar > 47 && e.KeyChar < 58)
            {
                pane.SetNumber(e.KeyChar - 48, true);
                this.Visible = false;
            }
            else if (e.KeyChar == 27)
            {
                button10.Focus();
                this.Visible = false;
            }
        }

        private void FrmEject_VisibleChanged(object sender, EventArgs e)
        {
            if (!this.Visible)
            {
                this.Owner.Activate();
            }
        }
    }
}
