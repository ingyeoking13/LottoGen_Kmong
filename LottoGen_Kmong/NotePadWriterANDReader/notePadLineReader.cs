using System;
using System.Collections.Generic;
using System.IO;

namespace LottoGen_Kmong.NotePadWriterANDReader
{
    public class notePadLineReader 
    {
        public IEnumerable<IEnumerable<byte>> ReturnGame(StreamReader reader)
        {
            string line;
            while((line = reader.ReadLine()) != null)
            {
                string[] numbers = line.Split(' ');
                byte[] ret = new byte[numbers.Length];
                for(int i= 0; i<numbers.Length; i++ )
                {
                    ret[i] = byte.Parse(numbers[i]);
                }
                yield return ret;
            }
        }
    }
}
