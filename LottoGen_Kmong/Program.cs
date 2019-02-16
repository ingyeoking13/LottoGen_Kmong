using System;
using System.IO;
using LottoGen_Kmong.ExcelWrapper;
using LottoGen_Kmong.LottoLogic;
using System.Numerics;
using System.Windows.Forms;
using LottoGen_Kmong.Helper;
using LottoGen_Kmong.NotePadWriterANDReader;
using Microsoft.WindowsAPICodePack.Dialogs;

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
            if (input == "1") StartWithAllNumberSet();
            else if (input == "2") StartWithImportData();
            else throw new Exception("-_-;; 올바르지 않은 문구입니다. ");
        }

        static void StartWithAllNumberSet()
        {
            ExcelReader reader = new ExcelReader(new FileInfo("LottoGenXLS.xlsx"));
            reader.ReadGameArgsAndGroupNumberFromfilepath();
            Console.WriteLine(reader.Gameargs.Range + " to " + reader.Gameargs.Extract);

            CommonOpenFileDialog target_dialog = getTargetDirectory();
            
            WriterAndFileMover mover = new WriterAndFileMover(target_dialog.FileName);

            int cnt = 1;
            var list = reader.ReturnNumberSetAndMinMax();
            foreach (var i in  reader.ReturnNumberSetAndMinMax())
            {

                notePadWriter writer = new notePadWriter(mover.target_directory_Path_plus+$"/output{cnt}A.txt");
                abstractLottologic lottologic;
                if (cnt == 1)
                {
                    lottologic = new LottologicWithAllNumberSet(
                        new LogicWithAllPossibleNumberSet(
                            i.rule_MinmaxArgs, 
                            reader.Gameargs, 
                            i.numberSet),
                        writer.WriteFile
                    );
                }
                else
                {
                    lottologic = new LottologicWithImportedDataSet(
                        new LogicWithExistNumberSet(i.rule_MinmaxArgs, i.numberSet),
                        writer.WriteFile,
                        Directory.GetFiles(mover.target_directory_Path_plus_prev, searchPattern: "*.txt")
                        );
                }

                mover.Switch();

                Console.WriteLine(cnt++ + " 번재 그룹 작업중입니다.");
                lottologic.doCalculate();
                writer.CloseWriter();
            }
        }

        static void StartWithImportData()
        {
            ExcelReader reader = new ExcelReader(new FileInfo("LottoGenXLS.xlsx"));
            reader.ReadGameArgsAndGroupNumberFromfilepath();
            Console.WriteLine(reader.Gameargs.Range + " to " + reader.Gameargs.Extract);

            CommonOpenFileDialog source_dialog = getSourceDirectory();
            CommonOpenFileDialog target_dialog = getTargetDirectory();
            
            WriterAndFileMover mover = new WriterAndFileMover(target_dialog.FileName);

            int cnt = 1;
            foreach (var i in reader.ReturnNumberSetAndMinMax())
            {

                notePadWriter writer = new notePadWriter( mover.target_directory_Path_plus + $"/output{cnt}A.txt");
                abstractLottologic lottoLogic = new LottologicWithImportedDataSet(
                    new LogicWithExistNumberSet(i.rule_MinmaxArgs, i.numberSet),
                    writer.WriteFile,
                    (cnt==1)?
                    Directory.GetFiles(source_dialog.FileName, "*.txt")
                    :
                    Directory.GetFiles(mover.target_directory_Path_plus_prev, "*.txt")
                    );

                Console.WriteLine(cnt++ + " 번재 그룹 작업중입니다.");
                lottoLogic.doCalculate();

                mover.Switch();
                writer.CloseWriter();
            }
        }

        static CommonOpenFileDialog getTargetDirectory()
        {
            CommonOpenFileDialog target_dialog = new CommonOpenFileDialog()
            {
                IsFolderPicker = true,
                Title = "저장할 폴더를 선택해주세요."
            };

            MessageBox.Show("결과물[data set].txt를 출력할 폴더를 지정해 해주세요.");
             target_dialog.ShowDialog();
            return target_dialog;
        }

        static CommonOpenFileDialog getSourceDirectory()
        {
            CommonOpenFileDialog source_dialog = new CommonOpenFileDialog()
            {
                IsFolderPicker = true,
                Title = "임포트 데이터가 있는 폴더를 선택해주세요."
            };
            MessageBox.Show("임포트[data set].txt가 저장되어 있는 폴더를 지정해 해주세요." 
                + Environment.NewLine + "결과물 폴더와 달라야합니다. !" );

            source_dialog.ShowDialog();
            return source_dialog;
        }
    }
}
