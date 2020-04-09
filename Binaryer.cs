using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Compression;

namespace WriterReader
{
    class Binaryer
    {
        string pathBin;

        public Binaryer(string pathBin)
        {
            this.pathBin = pathBin;
        }
        
        public void BinWriteRandom()
        {
            BinaryWriter bw = new BinaryWriter(File.Open(pathBin, FileMode.OpenOrCreate));
            Random ran = new Random();
            for (int i = 0; i < 1_000_000; i++)
            {
                bw.Write(ran.Next(0, 100_000));
            }
            bw.Close();
        }

        public int BinReader(int n)
        {
            BinaryReader br = new BinaryReader(File.Open(pathBin, FileMode.OpenOrCreate));
            br.BaseStream.Seek(n, SeekOrigin.Begin);
            int num = br.ReadInt32();
            br.Close();
            return num;
        } 

        public void BinReWrite(int index, int num)
        {
            BinaryWriter bw2 = new BinaryWriter(File.Open(pathBin, FileMode.OpenOrCreate));
            bw2.Seek(index, SeekOrigin.Begin);
            bw2.Write(num);
            bw2.Close();
        }

        public Stream GetStream()
        {
            FileStream fs = new FileStream(pathBin, FileMode.OpenOrCreate);
            return fs;
        }
    }
}
