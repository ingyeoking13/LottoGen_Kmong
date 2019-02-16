using LottoGen_Kmong.NotePadWriterANDReader;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace LottoGen_Kmong.Helper
{
    public class WriterAndFileMover
    {
        public string source_directory_Path= Application.StartupPath;

        private string _target_directory_Path_plus;
        private string _target_directory_Path_plus_prev;

        private string target_directory;
        public string target_directory_Path_plus
        {   get { return _target_directory_Path_plus; }
            set
            {
                if (!Directory.Exists(value)) Directory.CreateDirectory(value);
                _target_directory_Path_plus = value;
            }
        }
        ~WriterAndFileMover()
        {
            MoveFilesAll(target_directory);
            Directory.Delete(target_directory_Path_plus, true);
            Directory.Delete(target_directory_Path_plus_prev, true);
        }

        public string target_directory_Path_plus_prev
        {
            get
            { return _target_directory_Path_plus_prev; }
            set
            {
                if (!Directory.Exists(value)) Directory.CreateDirectory(value);
                _target_directory_Path_plus_prev = value;
            }
        }

        public WriterAndFileMover(string name)
        {
            target_directory_Path_plus_prev = name + "/fir";
            target_directory_Path_plus = name + "/sec";
            target_directory = name;
        }

        public void MoveFilesAll(string target_directory)
        {
            foreach( var filepath in Directory.EnumerateFiles(target_directory_Path_plus_prev))
            {
                string filenames = Path.GetFileName(filepath);
                File.Delete(target_directory + "/" + filenames);
                File.Move(filepath, target_directory + "/" + filenames);
            }
        }

        public void Switch()
        {
            string tmp = target_directory_Path_plus;
            target_directory_Path_plus = target_directory_Path_plus_prev;
            target_directory_Path_plus_prev = tmp;

            foreach ( var file in Directory.GetFiles(target_directory_Path_plus))
            {
                File.Delete(file);
            }
        }

    }
}
