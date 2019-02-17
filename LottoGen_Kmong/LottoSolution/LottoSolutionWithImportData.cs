using System;
using System.IO;
using LottoGen_Kmong.ExcelWrapper;
using LottoGen_Kmong.LottoLogic;
using LottoGen_Kmong.Helper;
using LottoGen_Kmong.NotePadWriterANDReader;
using Microsoft.WindowsAPICodePack.Dialogs;
using LottoGen_Kmong.Services;

namespace LottoGen_Kmong.LottoSolution 
{
    class LottoSolutionWithImportData : AbstractLottoSolution
    {
        public override void doJob()
        {
            ExcelReader reader = new ExcelReader(new FileInfo("LottoGenXLS.xlsx"));
            reader.ReadGameArgsAndGroupNumberFromfilepath();
            Logger.getInstance().writerFile(reader.Gameargs.Range + " to " + reader.Gameargs.Extract);

            CommonOpenFileDialog source_dialog = new CommonOpenFileDialog();
            CommonOpenFileDialog target_dialog = new CommonOpenFileDialog();
            try
            {
                source_dialog = getSourceDirectory();
                target_dialog = getTargetDirectory();
                if (source_dialog.FileName == target_dialog.FileName) throw new Exception("임포트 데이터 폴더 위치와 출력 폴더 위치는 같을 수 없습니다.");
            }
            catch (Exception ex)
            {
                ExceptionDialogService.getInstance().showMessageAndAllert(ex.Message);
            }

            WriterAndFileMover mover = new WriterAndFileMover(target_dialog.FileName);

            int cnt = 1;
            foreach (var i in reader.ReturnNumberSetAndMinMax())
            {

                IFilesListHave fileList;
                if (cnt == 1) fileList = new FileListHave(Directory.GetFiles(source_dialog.FileName, "*.txt"));
                else fileList = new FileListHaveDeletable(Directory.GetFiles(mover.target_directory_Path_plus_prev, "*.txt"));

                notePadWriter writer = new notePadWriter(mover.target_directory_Path_plus + $"/output{cnt}A.txt");
                abstractLottologic lottoLogic = new LottologicWithImportedDataSet(
                    new LogicWithExistNumberSet(i.rule_MinmaxArgs, i.numberSet),
                    writer.WriteFile,
                    fileList,
                    writerDelete: writer.deleteFile
                    );

                Logger.getInstance().writerFile(cnt++ + " 번재 그룹 작업중입니다.");
                lottoLogic.doCalculate();

                mover.Switch();
                writer.CloseWriter();
            }

        }
    }
}
