using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace WriterReader
{
    class Streamer
    {
        string pat;

        public Streamer(string pat)
        {
            this.pat = pat;   
        }

        public void WriteLineRandom()
        {
            StreamWriter sw = new StreamWriter(File.Open(pat, FileMode.OpenOrCreate));
            
            int len = 1_000_000;
            Random ran = new Random();
            int nums = 0;
            for (int i = 0; i < len; i++)
            {
                nums = ran.Next(0, 100_000);
                sw.Write(nums + "\n");
            }

            sw.Close();
        }

        public void WriteByLine(string[] AllLines)
        {
            StreamWriter sw2 = new StreamWriter(File.Open(pat, FileMode.OpenOrCreate));

            for (int i = 0; i < AllLines.Length; i++)
            {
                sw2.Write(AllLines[i] + "\n");
            }

            sw2.Close();
        }

        public void ReplaceLine(int num, int lineIndex)
        {
            string[] AllFileLine = ReadByLine();
            AllFileLine[lineIndex] = num.ToString();
            WriteByLine(AllFileLine);
        }


        public string[] ReadByLine()
        {
            StreamReader sr = new StreamReader(File.Open(pat, FileMode.OpenOrCreate));
            string[] AllLine = new string[1_000_000];
            for (int i = 0; i < 1_000_000; i++)
            {
                AllLine[i] = sr.ReadLine();
            }
            sr.Close();
            return AllLine;
        }
        
    }
}
