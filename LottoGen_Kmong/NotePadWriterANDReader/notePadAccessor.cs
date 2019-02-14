using System.IO;

namespace LottoGen_Kmong.NotePadWriterANDReader
{
    public abstract class notePadAccessor
    {
        private int counter = 0;
        private string filePath;

        public int Counter { get => counter; set => counter = value; }
        public string FilePath { get => filePath; set => filePath = value; }

        protected bool CheckFileExist(string name)
        {
            return File.Exists(name);
        }
        protected void deleteFile(string name) { File.Delete(name); }

    }
}
