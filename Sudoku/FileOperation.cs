using System;
using System.Collections.Generic;
using System.Linq;
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
                //StreamReader sr = new StreamReader(fs);
                BinaryReader br = new BinaryReader(fs, Encoding.ASCII);
                byte[] hof = br.ReadBytes(4);
                string h = Encoding.ASCII.GetString(hof);
                if (h == ".sdk")
                {
                    int[,] matrix = new int[9, 9];
                    for (int i = 0; i < 81; i++)
                    {
                        matrix[i / 9, i % 9] = br.ReadByte();
                    }
                    byte[] eof = br.ReadBytes(3);
                    br.Close();
                    fs.Close();
                    string e = Encoding.ASCII.GetString(eof);
                    if (e != "eof")
                    {
                        throw new Exception("文件可能被损坏！");
                    }
                    return matrix;
                }
                else
                {
                    throw new Exception("文件头不正确。");
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
                //StreamWriter sw = new StreamWriter(fs);
                BinaryWriter bw = new BinaryWriter(fs, Encoding.ASCII);
                bw.Write(Encoding.ASCII.GetBytes(".sdk"));
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        bw.Write((byte)matrix[i, j]);
                    }
                }
                bw.Write(Encoding.ASCII.GetBytes("eof"));
                bw.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
