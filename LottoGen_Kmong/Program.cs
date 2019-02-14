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
            ExcelReader reader = new ExcelReader(new FileInfo("LottoGenXLS.xlsx"));
            reader.ReadGameArgsAndGroupNumberFromfilepath();
            Console.WriteLine(reader.Gameargs.Range + " to " + reader.Gameargs.Extract);

            BigInteger bg = new BigInteger(1);

            int cnt = 1;
            foreach (var i in reader.ReturnNumberSetAndMinMax())
            {
                notePadWriter writer = new notePadWriter($"output{cnt}.txt");
                Lottologic lottoLogic = new Lottologic(
                    reader.Gameargs,
                    i.rule_MinmaxArgs,
                    i.numberSet,
                    writer.WriteFile
                );
                Console.WriteLine(bg + " 번째 그룹 작업중입니다."); 
                lottoLogic.calculate();

            }
        }
    }
}
