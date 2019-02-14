using System;
using System.Collections.Generic;

namespace LottoGen_Kmong.LottoLogic
{
    public abstract class abstractLottologic 
    {
        public Action<IEnumerable<byte>> writerDoWrite;
        public ILogicWithNumberSet LogicWithNumberSet;

        public abstractLottologic(ILogicWithNumberSet logicWithNumberSet, Action<IEnumerable<byte>> writerDoWrite)
        {
            LogicWithNumberSet = logicWithNumberSet;
            this.writerDoWrite = writerDoWrite;
            LogicWithNumberSet.SendResult += giveListToNotepadWriter;
        }

        ~abstractLottologic() { LogicWithNumberSet.SendResult -= giveListToNotepadWriter; }

        public void giveListToNotepadWriter(IEnumerable<byte> number_list)
        {
            writerDoWrite(number_list);
        }
        public abstract void doCalculate();
    }
}
