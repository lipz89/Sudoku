using System;
using System.Text;
using System.IO;

namespace Sudoku2
{
    class FileOperation
    {
        /// <summary>
        /// 加载游戏
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public int[,] LoadMatrix(string fileName)
        {
            try
            {
                FileStream fs = new FileStream(fileName, FileMode.Open);
                StreamReader br = new StreamReader(fs, Encoding.Unicode);
                string data = br.ReadToEnd();
                br.Close();
                fs.Close();
                bool flag = data.StartsWith(".sdk") && data.EndsWith(".eof") && data.Length == 89;
                if (flag)
                {
                    data = data.Substring(4, 81);
                    int[,] matrix = new int[9, 9];
                    for (int i = 0; i < 81; i++)
                    {
                        matrix[i / 9, i % 9] = (int)data[i] - 48;
                    }
                    return matrix;
                }
                else
                {
                    throw new Exception("文件可能被损坏！");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 保存游戏
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="matrix"></param>
        public void SaveMatrix(string fileName, int[,] matrix)
        {
            try
            {
                FileStream fs = new FileStream(fileName, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs, Encoding.Unicode);
                sw.Write(".sdk");
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        sw.Write(matrix[i, j]);
                    }
                }
                sw.Write(".eof");
                sw.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
