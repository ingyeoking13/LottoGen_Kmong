using LottoGen_Kmong.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using LottoGen_Kmong.NotePadWriterANDReader;

namespace LottoGen_Kmong.ExcelWrapper
{
    public class notePadWriter : notePadAccessor
    {
        private StreamWriter file;
        private static int HIT = (int)2e6;
        private int _hit = 0;
        private const long FILESIZEMAX = 214748364; // 200MB to Bytes

        public notePadWriter(string filePath)
        {
            CheckIfFileExistOrDeleteItAndSetIt(filePath);
        }

        ~notePadWriter()
        {
            file.Dispose();
        }

        public void WriteFile(IEnumerable<byte> list)
        {
            foreach (var i in list)
            {
                file.Write(i + " ");
            }
            file.WriteLine();
            _hit++;
            if (_hit > HIT)
            { 
                if (CheckFileSizeTooBig())
                {
                    Console.WriteLine("HIT 200MB!");
                    IncreaseFileName();
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

            if (file != null) file.Dispose();
            file = new StreamWriter(name, true);
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
