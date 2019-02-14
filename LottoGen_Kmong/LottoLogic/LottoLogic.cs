using System;
using System.Collections.Generic;
using System.Linq;

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
            LogicWithNumberSet.sendResult += giveListToNotepadWriter;
        }

        ~Lottologic() { LogicWithNumberSet.sendResult -= giveListToNotepadWriter; }

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
