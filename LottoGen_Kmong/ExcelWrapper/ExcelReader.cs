using LottoGen_Kmong.Helper;
using LottoGen_Kmong.LottoLogic;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

namespace LottoGen_Kmong.ExcelWrapper
{
    public class ExcelReader
    {
        private FileInfo excelFile;
        private gameArgs gameargs;
        private int groupNumber;

        private StartExcelReadPosition startExcelReadPosition = new StartExcelReadPosition();

        public gameArgs Gameargs { get => gameargs; set => gameargs = value; }
        public int GroupNumber {
            get => groupNumber;
            set
            {
                if (value <= 0) throw new ArgumentException("그룹수를 정확히 입력해주세요. "+ Environment.NewLine + "또는, 0 보다 큰 정수여야 합니다.");
                groupNumber = value;
            }
        }
        public ExcelReader(FileInfo excelFile)
        {
            this.excelFile = excelFile;
        }

        public void ReadGameArgsAndGroupNumberFromfilepath()
        {
            using (var package = new ExcelPackage(excelFile))
            {
                var worksheet= getWorkSheet(package);
                gameargs = new gameArgs(ReadCellValueInt(worksheet, "A4"), ReadCellValueInt(worksheet, "C4"));
                groupNumber = ReadCellValueInt(worksheet, "B10");
            }
        }

        public IEnumerable<gameGroup> ReturnNumberSetAndMinMax()
        {
            using (var package = new ExcelPackage(excelFile))
            {
                var worksheet= getWorkSheet(package);
                for (int i = 0; i < groupNumber; i++)
                {

                    int mn = ReadCellValueInt(worksheet, startExcelReadPosition.default_mnReader_Col_start + (startExcelReadPosition.default_mnReader_Row_start + i).ToString(), "mnNumber");
                    int mx = ReadCellValueInt(worksheet, startExcelReadPosition.default_mxReader_Col_start + (startExcelReadPosition.default_mxReader_Row_start + i).ToString(), "mxNumber");

                    int set_Count = ReadCellValueInt(worksheet, startExcelReadPosition.default_GroupReader_Col_start + (startExcelReadPosition.default_GroupReader_Row_start + i).ToString());
                    List<int> number_Set = new List<int>();
                    for (int j = 0; j < set_Count; j++)
                    {
                        number_Set.Add(
                            ReadCellValueInt(worksheet,
                            IntToStringConversionByBase.IntToStringConversion(j, 26, startExcelReadPosition.default_setReader_Col_start) + (startExcelReadPosition.default_setReader_Row_start ).ToString()
                                )
                            );
                    }

                    yield return new gameGroup(new Rule_minmaxArgs(mn, mx), number_Set);
                }
            }
        }

        private ExcelWorksheet getWorkSheet(ExcelPackage package)
        {
            if (package.Compatibility.IsWorksheets1Based)
            {
                return package.Workbook.Worksheets[2];
            }
            return package.Workbook.Worksheets[1];
        }

        private int ReadCellValueInt(ExcelWorksheet worksheet, string pos, [CallerMemberName]string name = null)
        {
            int ret;
            bool chk = true;

            var value = worksheet.Cells[pos].Value;
            if( value == null)
            {
                if (name == nameof(ReturnNumberSetAndMinMax)) throw new ArgumentException("게임 set은 공백일 수 없습니다.");
                else if (name == nameof(ReadGameArgsAndGroupNumberFromfilepath)) throw new ArgumentException("게임의 유형 또는 그룹 수는 공백 일 수 없습니다.");
                else if (name == "mnNumber" || name == "mxNumber") throw new ArgumentException("게임 추출 최소값 최대값은 공백 일 수 없습니다.");
            }

            chk = Int32.TryParse(value.ToString(), out ret);
            if (chk) return ret;

            else if ( chk == false || ret < 0)
            {
                if (name == nameof(ReturnNumberSetAndMinMax)) throw new ArgumentException("게임 set은 숫자 외의 문자이거나, 음수 일 수 없습니다.");
                else if (name == nameof(ReadGameArgsAndGroupNumberFromfilepath)) throw new ArgumentException("게임의 유형 또는 그룹 수는 숫자 외의 문자이거나, 음수 일 수 없습니다.");
                else if (name == "mnNumber" || name == "mxNumber") throw new ArgumentException("게임 추출 최소값 최대값은 숫자 외의 문자이거나, 음수 일 수 없습니다.");
            }
            return 0;
        }
    }
}
