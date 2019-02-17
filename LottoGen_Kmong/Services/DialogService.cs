using System;
using System.Windows.Forms;

namespace LottoGen_Kmong.Services
{
    public class ExceptionDialogService
    {
        [ThreadStatic]
        private static ExceptionDialogService instance;
        
        private ExceptionDialogService()
        {
        }

        public static ExceptionDialogService getInstance()
        {
            if (instance == null) instance = new ExceptionDialogService();
            return instance;
        }

        public void showMessageAndAllert(string message)
        {
            MessageBox.Show(message);
            Logger.getInstance().writerFile(message);
            Logger.getInstance().disposeStreamWriter();
            throw new ArgumentException(message);
        }
    }
}
