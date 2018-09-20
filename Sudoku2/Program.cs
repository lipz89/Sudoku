using System;
using System.Windows.Forms;
using System.Threading;
using Sudoku2.Properties;
using System.Runtime.InteropServices;
using System.Text;

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
            MutexOnly.ShowMutexOnly(Run, args);
        }

        private static void Run(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Length > 0 && args[0].EndsWith(".sdk"))
            {
                Application.Run(new FrmGame(args[0]));
            }
            else
            {
                Application.Run(new FrmGame(null));
            }
        }
    }
}
