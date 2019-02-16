using LottoGen_Kmong.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using LottoGen_Kmong.NotePadWriterANDReader;

namespace LottoGen_Kmong.NotePadWriterANDReader
{
    public class notePadWriter : notePadAccessor
    {
        private StreamWriter fileWriter;
        private static int HIT = (int)2e6;
        private int _hit = 0;
        private const long FILESIZEMAX = 214748364; // 200MB to Bytes

        public notePadWriter(string filePath)
        {
            CheckIfFileExistOrDeleteItAndSetIt(filePath);
        }

        public void CloseWriter()
        {
            fileWriter.Dispose();
        }

        public void WriteFile(IEnumerable<byte> list)
        {
            foreach (var i in list)
            {
                fileWriter.Write(i + " ");
            }
            fileWriter.WriteLine();
            _hit++;
            if (_hit > HIT)
            { 
                if (CheckFileSizeTooBig())
                {
                    IncreaseFileName();
                    Console.WriteLine("HIT 200MB!");
                    Console.WriteLine($"{FilePath}를 생성했습니다.");
                }
                _hit = 0;
            }
        }

        private bool CheckFileSizeTooBig()
        {
            return new FileInfo(FilePath).Length >= FILESIZEMAX;
        }

        private void CheckIfFileExistOrDeleteItAndSetIt(string name)
        {
            if (CheckFileExist(name)) deleteFile(name);
            FilePath = name;
            
            if (fileWriter != null) fileWriter.Dispose();
            fileWriter = new StreamWriter(name, true);
        }

        private void IncreaseFileName()
        {
            string prev = IntToStringConversionByBase.IntToStringConversion(Counter);
            Counter++;
            string added = IntToStringConversionByBase.IntToStringConversion(Counter);

            string newFilePath = FilePath.Substring(0, FilePath.IndexOf(".") - prev.Length)
            + added
            + ".txt";
            CheckIfFileExistOrDeleteItAndSetIt(newFilePath);
        }

    }
}
