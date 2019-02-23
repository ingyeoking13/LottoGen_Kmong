using LottoCheck_Kmong.Helper;
using LottoCheck_Kmong.LottoLogic;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;

namespace LottoCheck_Kmong.ExcelWrapper
{
    public class ExcelintWriter : AbstractExcelWriter<int>
    {

        private StartExcelReadPosition startExcelReadPosition = new StartExcelReadPosition();
        static int row = 0;
        static int offset = 0;

        public ExcelintWriter(FileInfo excelFile) : base(excelFile) { }

        public void WriteCollectionCounting(ICollection<int> list)
        {
            var worksheets = getWorkSheet(package);
            var worksheet = worksheets[1];

            int cnt = 0 + offset;
            foreach (var i in list)
            {
                string pos = IntToStringConversionByBase.IntToStringConversion(cnt, off: startExcelReadPosition.default_setReader_Col_start) + (startExcelReadPosition.default_setReader_Row_start + row).ToString();
                WriteCellValue(worksheet, pos, i);
                cnt++;
            }
            row++;
        }

        public void write_Set(gameGroup B)
        {
            var worksheets = getWorkSheet(package);
            var worksheet = worksheets[1];

            var list_list = B.numberSet;
            foreach (var i in list_list)
            {
                offset = i.Count + 2;
                int cnt = 0;
                foreach (var j in i)
                {
                    string pos = IntToStringConversionByBase.IntToStringConversion(cnt, off: startExcelReadPosition.default_setReader_Col_start) + (startExcelReadPosition.default_setReader_Row_start + row).ToString();
                    WriteCellValue(worksheet, pos, j);
                    cnt++;
                }
                row++;
            }
            row = 0;
        }

    }
}
