using System;
using System.Collections.Generic;

namespace LottoGen_Kmong.LottoLogic
{
    public abstract class abstractLottologic 
    {
        public Action<IEnumerable<byte>> writerDoWrite;
        public ILogicWithNumberSet LogicWithNumberSet;
        public ICollection<string> fileSet = new List<string>();

        /// <summary>
        /// 데이터를 처리하는 logic의 생성자입니다.
        /// </summary>
        /// <param name="logicWithNumberSet">Logic을 지정해주세요.</param>
        /// <param name="writerDoWrite">writer 클래스의 write 메서드를 지정해주세요.</param>
        public abstractLottologic(ILogicWithNumberSet logicWithNumberSet, Action<IEnumerable<byte>> writerDoWrite)
        {
            LogicWithNumberSet = logicWithNumberSet;
            this.writerDoWrite = writerDoWrite;
            LogicWithNumberSet.SendResult += giveListToNotepadWriter;
        }

        ~abstractLottologic() { LogicWithNumberSet.SendResult -= giveListToNotepadWriter; }

        public void giveListToNotepadWriter(IEnumerable<byte> number_list) { writerDoWrite(number_list); }
        public void updateFileSet(string name) {; }
        public abstract void doCalculate();
    }
}
