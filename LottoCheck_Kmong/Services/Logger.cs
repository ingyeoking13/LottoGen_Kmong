using System;
using System.IO;

namespace LottoCheck_Kmong.Services
{
    public class Logger: IDisposable
    {
        [ThreadStatic]
        private static Logger _logger;
        private string filepath = "LOGGER.txt";
        [ThreadStatic]
        private static StreamWriter _stream;

        public static Logger logger { get => _logger; }
        public static StreamWriter Stream { get => _stream; }

        private Logger()
        {
            _stream = new StreamWriter(filepath, true);
            Stream.WriteLine($"{DateTime.Now} 작업 시작");
        }


        public static Logger getInstance()
        {
            if (logger == null) _logger = new Logger();
            return logger;
        }

        public void writerFile(string str)
        {
            Console.WriteLine(str);
            Stream.WriteLine($"{DateTime.Now} {str}");
        }
        public void disposeStreamWriter()
        {
            Stream.WriteLine($"{DateTime.Now} 작업 끝============");
            Stream.WriteLine();
            Stream.Dispose();
        }

        public void Dispose()
        {
            disposeStreamWriter();
        }
    }
}


