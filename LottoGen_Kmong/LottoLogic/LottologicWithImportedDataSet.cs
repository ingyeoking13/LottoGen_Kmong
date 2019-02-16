using System;
using System.Collections.Generic;
using System.IO;

namespace LottoGen_Kmong.LottoLogic
{
    public class LottologicWithImportedDataSet : abstractLottologic
    {
        public IEnumerable<string> Files { get; }
        /// <summary>
        /// 임포트 데이터로부터 쿼리를 날리는 생성자입니다.
        /// </summary>
        /// <param name="logicWithNumberSet">logic을 지정해주세요.</param>
        /// <param name="writerDoWrite">writer의 메서드를 전달받습니다.</param>
        /// <param name="files">임포트 하는 파일들의 리스트를 주세요.</param>
        public LottologicWithImportedDataSet(ILogicWithNumberSet logicWithNumberSet, Action<IEnumerable<byte>> writerDoWrite, IEnumerable<string> files) : 
            base(logicWithNumberSet, writerDoWrite)
        {
            Files = files;
        }

        public override void doCalculate()
        {
            foreach (var file in Files)
            {
                Console.WriteLine(Path.GetFileName(file) + " 을 열었습니다.");
                using (var reader = new StreamReader(file))
                {

                    LogicWithNumberSet.CalcuateWithExistNumberset(reader);
                }
            }
        }
    }
}
