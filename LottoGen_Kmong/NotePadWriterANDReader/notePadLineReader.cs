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
                string[] numbers = line.Split('\t', ' ');
                List<byte> ret = new List<byte>();
                for(int i= 0; i<numbers.Length; i++ )
                {
                    if (numbers[i] == "" || numbers[i] == null || numbers[i] == Environment.NewLine)
                    {
                        continue;
                    }
                    ret.Add(byte.Parse(numbers[i]));
                }
                yield return ret;
            }
        }
    }
}
