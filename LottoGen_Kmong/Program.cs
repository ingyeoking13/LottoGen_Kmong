using System;
using LottoGen_Kmong.Services;
using LottoGen_Kmong.LottoSolution;

namespace LottoGen_Kmong
{
    class Program
    {

        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("[1] 임포트 데이터가 없습니다. 840만개 숫자와 바로 엑셀 쿼리로부터 시작해주세요. " + 
                Environment.NewLine + 
                "[2] 임포트 데이터가 있습니다. 데이터셋과 엑셀 쿼리로부터 시작해주세요. ");

            string input;
            input = Console.ReadLine();
            AbstractLottoSolution lottoSolution = new LottoSolutionWithAllNumberSet();

            if (input == "1") lottoSolution = new LottoSolutionWithAllNumberSet();
            else if (input == "2") lottoSolution = new LottoSolutionWithImportData();
            else
            {
                ExceptionDialogService.getInstance().showMessageAndAllert("입력은 1 또는 2만해주세요. ");
            }

            lottoSolution.doJob();
            Logger.getInstance().Dispose();
        }
    }
}
