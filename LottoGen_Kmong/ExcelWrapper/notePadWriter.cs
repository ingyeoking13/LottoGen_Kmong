using System.Collections.Generic;
using System.IO;

namespace LottoGen_Kmong.ExcelWrapper
{
    public class notePadWriter
    {
        private string filePath;
        StreamWriter file;
        public notePadWriter(string filePath)
        {
            this.filePath = filePath;
            if ( File.Exists(filePath))
            {
                File.Delete(filePath);
            } 
            file = new StreamWriter(filePath, true);
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
        }
    }
}
