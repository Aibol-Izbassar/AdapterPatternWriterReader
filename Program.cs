using System;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;

namespace WriterReader
{
    class Program
    {
     
        static void Main(string[] args)
        {
            int n1 = 200_000;
            int n2 = 700_000;

            string pathStream = "wordStream.txt";
            string pathBinary = "wordBinary.txt";

            //************StreamReader ********* StreamWriter*********
            Console.WriteLine("********Stream Reader // Stream Writer**********");
            Streamer streamer = new Streamer(pathStream);
            
            streamer.WriteLineRandom();
            string[] result = streamer.ReadByLine();

            Console.WriteLine("До замены в Stream 1: " + result[n1]);
            Console.WriteLine("До замены в Stream 2: " + result[n2]);

            streamer.ReplaceLine(777, n1);
            streamer.ReplaceLine(888, n2);

            result = streamer.ReadByLine();

            Console.WriteLine("После замены в Stream 1: " + result[n1]);
            Console.WriteLine("После замены в Stream 2: " + result[n2]);
            //StreamReader ********* StreamWriter*********


            //************BinaryReader ********* BinaryWriter*********
            Console.WriteLine("********Binary Reader // Binary Writer**********");
            Binaryer bin = new Binaryer(pathBinary);
            
            bin.BinWriteRandom();


            Console.WriteLine("До замены в Binary 1: " + bin.BinReader(n1));
            Console.WriteLine("До замены в Binary 2: " + bin.BinReader(n2));

            bin.BinReWrite(n1, 888);
            bin.BinReWrite(n2, 777);

            Console.WriteLine("После замены в Binary 1: " + bin.BinReader(n1));
            Console.WriteLine("После замены в Binary 2: " + bin.BinReader(n2));




            //************with Gzip************
            string[] allFile = new string[1_000_000];
            Random ran = new Random();

            FileStream fs = new FileStream("Gzip.txt", FileMode.OpenOrCreate);
            GZipStream gzp = new GZipStream(fs, CompressionMode.Compress);
            StreamWriter sw = new StreamWriter(gzp);
            
            for (int i = 0; i < 1_000_000; i++)
            {
                sw.WriteLine(ran.Next(100000));
            }
            sw.Close();

            FileStream fs2 = new FileStream("Gzip.txt", FileMode.OpenOrCreate);
            GZipStream gzpDec = new GZipStream(fs2, CompressionMode.Decompress);
            StreamReader Sr2 = new StreamReader(gzpDec);

            for (int i = 0; i < 1_000_000; i++)
            {
                allFile[i] = Sr2.ReadLine();
            }
            Sr2.Close();

            Console.WriteLine("До замены строки в Gzip: " + allFile[200_000]);
            Console.WriteLine("До замены строки в GZip: " + allFile[700_000]);
            int replace1 = 777;
            int replace2 = 888;
            allFile[n1] = replace1.ToString();
            allFile[n2] = replace2.ToString();

            fs = new FileStream("Gzip.txt", FileMode.OpenOrCreate);
            gzp = new GZipStream(fs, CompressionMode.Compress);
            sw = new StreamWriter(gzp);

            for (int i = 0; i < 1_000_000; i++)
            {
                sw.WriteLine(allFile[i]);
            }
            sw.Close();

            //проверка 
            fs2 = new FileStream("Gzip.txt", FileMode.OpenOrCreate);
            gzpDec = new GZipStream(fs2, CompressionMode.Decompress);
            Sr2 = new StreamReader(gzpDec);

            for (int i = 0; i < 1_000_000; i++)
            {
                allFile[i] = Sr2.ReadLine();
            }
            Sr2.Close();

            Console.WriteLine("После замены строки в Gzip: " + allFile[200_000]);
            Console.WriteLine("После замены строки в GZip: " + allFile[700_000]);

        }
    }
}
