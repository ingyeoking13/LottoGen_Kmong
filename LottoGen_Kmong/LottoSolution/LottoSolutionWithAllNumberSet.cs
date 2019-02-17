using System;
using System.IO;
using LottoGen_Kmong.ExcelWrapper;
using LottoGen_Kmong.LottoLogic;
using LottoGen_Kmong.Helper;
using LottoGen_Kmong.NotePadWriterANDReader;
using Microsoft.WindowsAPICodePack.Dialogs;
using LottoGen_Kmong.Services;
using LottoGen_Kmong.LottoLogic.CoreLogic;

namespace LottoGen_Kmong.LottoSolution
{
    public class LottoSolutionWithAllNumberSet : AbstractLottoSolution
    {
        public override void doJob()
        {
            ExcelReader reader = new ExcelReader(new FileInfo("LottoGenXLS.xlsx"));
            reader.ReadGameArgsAndGroupNumberFromfilepath();
            Logger.getInstance().writerFile(reader.Gameargs.Range + " to " + reader.Gameargs.Extract);

            CommonOpenFileDialog target_dialog = new CommonOpenFileDialog();
            try
            {
                target_dialog = getTargetDirectory();
            }
            catch (Exception ex)
            {
                ExceptionDialogService.getInstance().showMessageAndAllert(ex.Message);
            }

            WriterAndFileMover mover = new WriterAndFileMover(target_dialog.FileName);

            int cnt = 0;
            var list = reader.ReturnNumberSetAndMinMax();
            foreach (var i in reader.ReturnNumberSetAndMinMax())
            {

                notePadWriter writer = new notePadWriter(mover.target_directory_Path_plus + $"/output{cnt}A.txt");
                abstractLottologic lottologic;
                if (cnt == 0)
                {
                    CreateAllPossibleNumber creater = new CreateAllPossibleNumber(reader.Gameargs, writer.WriteFile);
                    Logger.getInstance().writerFile(cnt++ + " 번째 작업중... 모든 경우의 수를 생성중입니다.  ");
                    creater.createAllPossibleNumber();
                }
                else
                {
                    lottologic = new LottologicWithImportedDataSet(
                        new LogicWithExistNumberSet(i.rule_MinmaxArgs, i.numberSet),
                        writer.WriteFile,
                        new FileListHave(Directory.GetFiles(mover.target_directory_Path_plus_prev, searchPattern: "*.txt")),
                        writer.deleteFile
                        );
                    Logger.getInstance().writerFile(cnt++ + " 번째 그룹 작업중입니다.");
                    lottologic.doCalculate();
                }
                mover.Switch();
                writer.CloseWriter();
            }
        }
    }
}
