using System;
using System.Collections.Generic;
using System.IO;

namespace LottoGen_Kmong.LottoLogic
{
    public class LottologicWithImportedDataSet : abstractLottologic
    {
        public IEnumerable<string> Files { get; }
        public LottologicWithImportedDataSet(ILogicWithNumberSet logicWithNumberSet, Action<IEnumerable<byte>> writerDoWrite, IEnumerable<string> files) : 
            base(logicWithNumberSet, writerDoWrite)
        {
            Files = files;
        }

        public override void doCalculate()
        {
            foreach (var file in Files)
            {
                using (var reader = new StreamReader(file))
                {
                    LogicWithNumberSet.CalcuateWithExistNumberset(reader);
                }
            }
        }
    }
}
