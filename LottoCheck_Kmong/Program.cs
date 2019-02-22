using LottoCheck_Kmong.ExcelWrapper;
using LottoCheck_Kmong.LottoCheckSolution;
using System.IO;

namespace LottoCheck_Kmong
{
    public class Program
    {
        static string FileName = "LottoCheckXLS.xlsx"; 
        static void Main(string[] args)
        {
            ExcelbyteReader excelbyteReader = new ExcelbyteReader(new FileInfo(FileName));
            var game_TwoSet = excelbyteReader.ReturnNumberSet();

            ExcelintWriter excelintWriter = new ExcelintWriter(new FileInfo(FileName));
            LottoCheckerLogic checkerLogic = new LottoCheckerLogic( game_TwoSet.Item1, game_TwoSet.Item2 , excelintWriter.WriteCollectionCounting);
            checkerLogic.doCalculate();
        }
    }
}
