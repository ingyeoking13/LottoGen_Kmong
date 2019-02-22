using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using LottoCheck_Kmong.ExcelWrapper.ExceptionRules;
using OfficeOpenXml;

namespace LottoCheck_Kmong.ExcelWrapper
{
    public abstract class AbstractExcelReader<T>  : AbstractExcelAccessor where T : struct
    {
        private IReadExceptionRules readExceptionRules;
        public AbstractExcelReader(FileInfo excelFile) :base (excelFile)
        {
        }

        protected T ReadCellValue(ExcelWorksheet worksheet, string pos, [CallerMemberName] string name = null)
        {
            T ret;

            var value = worksheet.Cells[pos].Value;

            readExceptionRules = new ExceptionDecoration();
            readExceptionRules = new noNullreadException(readExceptionRules);
            readExceptionRules.checkWithValue(value, name);

            var type = typeof(T);
            var converter = TypeDescriptor.GetConverter(type);

            ret = (T)converter.ConvertFromString(value.ToString());
            readExceptionRules = new noNegativereadException(readExceptionRules);
            readExceptionRules.checkWithValue(ret, name);
            return ret;
        }
    }
}