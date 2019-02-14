using System;
using System.IO;
using System.Collections.Generic;
using LottoGen_Kmong.ExcelWrapper;
using LottoGen_Kmong.LottoLogic;
using System.Numerics;

namespace LottoGen_Kmong
{
    class Program
    {
        static void Main(string[] args)
        {
            StartWithAllNumberSet();
        }

        static void StartWithAllNumberSet()
        {
            ExcelReader reader = new ExcelReader(new FileInfo("LottoGenXLS.xlsx"));
            reader.ReadGameArgsAndGroupNumberFromfilepath();
            Console.WriteLine(reader.Gameargs.Range + " to " + reader.Gameargs.Extract);

            BigInteger bg = new BigInteger(1);

            int cnt = 1;
            foreach (var i in reader.ReturnNumberSetAndMinMax())
            {

                if (cnt == 1)
                {
                    notePadWriter writer = new notePadWriter($"output{cnt}A.txt");
                    abstractLottologic lottoLogic = new LottologicWithAllNumberSet(
                        new LogicWithAllPossibleNumberSet(
                            i.rule_MinmaxArgs, 
                            reader.Gameargs, 
                            i.numberSet),
                        writer.WriteFile
                    );
                    Console.WriteLine(bg + " 번째 그룹 작업중입니다.");
                    lottoLogic.doCalculate();
                }
                else
                {
                    notePadWriter writer = new notePadWriter($"output{cnt}A.txt");
                    abstractLottologic lottoLogic = new LottologicWithImportedDataSet(
                        new LogicWithExistNumberSet(),
                        writer.WriteFile);
                    Console.WriteLine(bg + " 번재 그룹 작업중입니다.");
                    lottoLogic.doCalculate();
                }
                cnt++;
            }
        }

        static void StartWithImportData()
        {
            ExcelReader reader = new ExcelReader(new FileInfo("LottoGenXLS.xlsx"));
            reader.ReadGameArgsAndGroupNumberFromfilepath();
            Console.WriteLine(reader.Gameargs.Range + " to " + reader.Gameargs.Extract);
            int cnt = 1;

            foreach (var i in reader.ReturnNumberSetAndMinMax())
            {

                notePadWriter writer = new notePadWriter($"output{cnt}A.txt");
                abstractLottologic lottoLogic = new LottologicWithImportedDataSet(
                    new LogicWithExistNumberSet(),
                    writer.WriteFile);
                Console.WriteLine(cnt++ + " 번재 그룹 작업중입니다.");
                lottoLogic.doCalculate();

            }
        }
    }
}
