using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Sudoku2
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Length > 0)
            {
                if (args[0].EndsWith(".sdk"))
                {
                    Application.Run(new FrmGame(args[0]));
                }
            }
            else
            {
                Application.Run(new FrmGame(null));
            }
        }
    }
}
