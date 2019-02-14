using System;
using System.Collections.Generic;

namespace LottoGen_Kmong.LottoLogic
{
    public class Lottologic 
    {
        public Action<IEnumerable<byte>> writerDoWrite;
        public ILogicWithNumberSet LogicWithNumberSet;

        public Lottologic(ILogicWithNumberSet logicWithNumberSet, Action<IEnumerable<byte>> writerDoWrite)
        {
            LogicWithNumberSet = logicWithNumberSet;
            this.writerDoWrite = writerDoWrite;
            LogicWithNumberSet.SendResult += giveListToNotepadWriter;
        }

        ~Lottologic() { LogicWithNumberSet.SendResult -= giveListToNotepadWriter; }

        public void doCalculate()
        {
            LogicWithNumberSet.Calculate();
        }

        public void giveListToNotepadWriter(IEnumerable<byte> number_list)
        {
            writerDoWrite(number_list);
        }
    }
}
