using LottoCheck_Kmong.Services;
using OfficeOpenXml;
using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace LottoCheck_Kmong.ExcelWrapper
{
    public class AbstractExcelWriter<T> : AbstractExcelAccessor where T : struct
    {
        public AbstractExcelWriter(FileInfo excelFile) : base(excelFile)
        {
        }

        protected bool WriteCellValue(ExcelWorksheet worksheet, string pos, T value, [CallerMemberName] string name = null)
        {
            try
            {
                worksheet.Cells[pos].Value = value.ToString();
            }
            catch (Exception e)
            {
                ExceptionDialogService.getInstance().showMessageAndAllert($"{name} Calls {nameof(WriteCellValue)}. 그러나 예외가 발생했습니다." + Environment.NewLine + e.Message);
            }
            return true;
        }
    }
}
