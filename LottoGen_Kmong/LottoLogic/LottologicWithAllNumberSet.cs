using System;
using System.Collections.Generic;

namespace LottoGen_Kmong.LottoLogic
{
    public class LottologicWithAllNumberSet : abstractLottologic
    {
        public LottologicWithAllNumberSet(ILogicWithNumberSet logicWithNumberSet, Action<IEnumerable<byte>> writerDoWrite) : 
            base(logicWithNumberSet, writerDoWrite) { }

        public override void doCalculate()
        {
            LogicWithNumberSet.Calculate();
        }
    }
}
