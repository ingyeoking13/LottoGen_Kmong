using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Windows.Forms;

namespace LottoGen_Kmong.LottoSolution
{
    public abstract class AbstractLottoSolution
    {
        protected CommonOpenFileDialog getTargetDirectory()
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

        protected CommonOpenFileDialog getSourceDirectory()
        {
            CommonOpenFileDialog source_dialog = new CommonOpenFileDialog()
            {
                IsFolderPicker = true,
                Title = "임포트 데이터가 있는 폴더를 선택해주세요."
            };
            MessageBox.Show("임포트[data set].txt가 저장되어 있는 폴더를 지정해 해주세요."
                + Environment.NewLine + "결과물 폴더와 달라야합니다. !");

            source_dialog.ShowDialog();
            return source_dialog;
        }
        public abstract void doJob();
    }
}
