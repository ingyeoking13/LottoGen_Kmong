using LottoCheck_Kmong.Helper;
using LottoCheck_Kmong.LottoLogic;
using LottoCheck_Kmong.Services;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;


namespace LottoCheck_Kmong.ExcelWrapper
{

    enum Sheet
    {
        AGROUPPAGE=2,
        BGROUPPAGE=3,
        RESULTPAGE=4
    }
    public class ExcelbyteReader : AbstractExcelReader<int>
    {

        private StartExcelReadPosition startExcelReadPosition = new StartExcelReadPosition();

        public ExcelbyteReader(FileInfo excelFile) : base(excelFile) 
        {
        }

        private gameArgs ReadGameArgsAndGroupNumberFromWorkSheet(int idx)
        {
            gameArgs gameargs =new gameArgs(0, 0, 0);

            try
            {
                var worksheets = getWorkSheet(package);
                var worksheet = worksheets[idx];
                gameargs = new gameArgs(ReadCellValue(worksheet, "D3"), ReadCellValue(worksheet, "B4"), ReadCellValue(worksheet, "B5"));
            }
            catch( Exception e)
            {
                ExceptionDialogService.getInstance().showMessageAndAllert($"error occured in {nameof(ReadGameArgsAndGroupNumberFromWorkSheet)}" +Environment.NewLine + e.Message);
            }
            return gameargs;
        }

        public Tuple<gameGroup, gameGroup> ReturnNumberSet()
        {
            gameGroup ret = new gameGroup();
            gameGroup ret2 = new gameGroup();

            gameArgs firstGameArgs = ReadGameArgsAndGroupNumberFromWorkSheet((int)Sheet.AGROUPPAGE);
            gameArgs secGameArgs = ReadGameArgsAndGroupNumberFromWorkSheet((int)Sheet.BGROUPPAGE);

            var worksheets= getWorkSheet(package);
            for (int k = (int)Sheet.AGROUPPAGE; k <= (int)Sheet.BGROUPPAGE; k++)
            {
                var worksheet = worksheets[k];
                var gameargs = firstGameArgs;
                if (k == 3) gameargs = secGameArgs;

                for (int i = 0; i < gameargs.gameNumber; i++)
                {

                    List<byte> number_Set = new List<byte>();
                    for (int j = 0; j < gameargs.setSize; j++)
                    {
                        string pos = IntToStringConversionByBase.IntToStringConversion
                            (j, 26, startExcelReadPosition.default_setReader_Col_start) + (startExcelReadPosition.default_setReader_Row_start + i).ToString();
                        number_Set.Add( byte.Parse(ReadCellValue(worksheet, pos ).ToString()));
                    }
                    if (k == 2) ret.numberSet.Add(number_Set);
                    else ret2.numberSet.Add(number_Set);
                }
            }
            ret.gameargs = firstGameArgs;
            ret.gameargs = secGameArgs;
            return new Tuple<gameGroup, gameGroup>(ret, ret2);
        }
    }
}
