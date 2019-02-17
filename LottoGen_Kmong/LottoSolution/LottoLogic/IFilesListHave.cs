using LottoGen_Kmong.NotePadWriterANDReader;
using System;
using System.Collections.Generic;
using System.IO;

namespace LottoGen_Kmong.LottoLogic
{
    public interface IFilesListHave
    {
        IEnumerable<string> Files { get; }
        notePadLineReader lineReader { get; }
    }

    public class NullFileListHave : IFilesListHave
    {
        public IEnumerable<string> Files => throw new NotImplementedException();
        public notePadLineReader lineReader => throw new NotImplementedException();
    }

    public class FileListHave : IFilesListHave
    {
        public IEnumerable<string> Files { get;  }
        public notePadLineReader lineReader { get; }

        public FileListHave(IEnumerable<string> files)
        {
            Files = files;
            lineReader = new notePadLineReader();
        }
    }

    public class FileListHaveDeletable : IFilesListHave
    {
        public IEnumerable<string> Files { get; }
        public notePadLineReader lineReader { get; }

        public FileListHaveDeletable(IEnumerable<string> files)
        {
            Files = files;
            lineReader = new notePadLineReader();
        }
        ~FileListHaveDeletable()
        {
            var iter = Files.GetEnumerator();

            while (iter.MoveNext()) 
            {
                File.Delete(iter.Current);
            }
        }
    }
}
