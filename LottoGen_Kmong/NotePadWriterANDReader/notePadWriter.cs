using LottoGen_Kmong.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using LottoGen_Kmong.NotePadWriterANDReader;
using LottoGen_Kmong.Services;

namespace LottoGen_Kmong.NotePadWriterANDReader
{
    public class notePadWriter : notePadAccessor
    {
        private StreamWriter fileWriter;
        private static int HIT = (int)1e6;
        private int _hit = 0;

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
            if (_hit == HIT) 
            { 
                IncreaseFileName();
                Logger.getInstance().writerFile($"HIT {_hit} 갯수!");
                Logger.getInstance().writerFile($"{FilePath}를 생성했습니다.");
                _hit = 0;
            }
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
