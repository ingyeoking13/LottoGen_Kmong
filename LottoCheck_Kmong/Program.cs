using LottoCheck_Kmong.ExcelWrapper;
using LottoCheck_Kmong.LottoCheckSolution;
using OfficeOpenXml;
using System;
using System.IO;

namespace LottoCheck_Kmong
{
    public class Program
    {
        static string FileName = "LottoCheckXLS.xlsx"; 
        static string outputFileName = "outputXLS.xlsx"; 
        static void Main(string[] args)
        {
            ExcelbyteReader excelbyteReader = new ExcelbyteReader(new FileInfo(FileName));
            var game_TwoSet = excelbyteReader.ReturnNumberSet();

            File.Delete(outputFileName);

            using (var package = new ExcelPackage(new FileInfo(outputFileName)))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sales list - " + DateTime.Now.ToShortDateString());
                package.Save();
            }

            ExcelintWriter excelintWriter = new ExcelintWriter(new FileInfo(outputFileName));
            LottoCheckerLogic checkerLogic = new LottoCheckerLogic( game_TwoSet.Item1, game_TwoSet.Item2 , excelintWriter.WriteCollectionCounting);
            excelintWriter.write_Set(game_TwoSet.Item2);
            checkerLogic.doCalculate();

            excelbyteReader.Dispose();
            excelintWriter.Dispose();
        }
    }
}
