using LottoCheck_Kmong.Helper;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;

namespace LottoCheck_Kmong.ExcelWrapper
{
    public class ExcelintWriter : AbstractExcelWriter<int>
    {

        private StartExcelReadPosition startExcelReadPosition = new StartExcelReadPosition();
        static int row = 0;

        public ExcelintWriter(FileInfo excelFile) : base(excelFile) { }

        public void WriteCollectionCounting(ICollection<int> list)
        {
            using (var package = new ExcelPackage(excelFile))
            {
                var worksheets = getWorkSheet(package);
                var worksheet = worksheets[(int)Sheet.RESULTPAGE];

                int cnt = 0;
                foreach( var i in list)
                {
                    string pos= IntToStringConversionByBase.IntToStringConversion(cnt, off: startExcelReadPosition.default_setReader_Col_start) + (startExcelReadPosition.default_setReader_Row_start+row).ToString();
                    WriteCellValue(worksheet,pos,i);
                    cnt++;
                }
                package.Save();
            }
            row++;
        }
    }
}
