using LottoGen_Kmong.NotePadWriterANDReader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LottoGen_Kmong.LottoLogic
{
    public class LogicWithExistNumberSet : ILogicWithNumberSet
    {
        private Action<IEnumerable<byte>> sendResult;
        private IEnumerable<int> wanna_set;
        private Rule_minmaxArgs minmax_rule;

        public Action<IEnumerable<byte>> SendResult { get => sendResult; set => sendResult = value; }
        public IEnumerable<int> Wanna_Set { get => wanna_set; set =>wanna_set =value; }
        public Rule_minmaxArgs Minmax_rule { get => minmax_rule; set => minmax_rule = value; }

        public LogicWithExistNumberSet(
            Rule_minmaxArgs minmax_rule, IEnumerable<int> wanna_Set)
        {
            Minmax_rule = minmax_rule;
            Wanna_Set = wanna_Set;
        }

        public void Calculate() { throw new NotImplementedException(); }
        public void CalcuateWithExistNumberset(IEnumerable<byte> intArray)
        {
            int hit = 0;

            foreach( var i in intArray)
            {
                int length = Wanna_Set.Count();
                for (int j = 0; j < length; j++)
                {
                    if (i == (byte)Wanna_Set.ElementAt(j)) hit++;
                }
            };

            if (hit >= Minmax_rule.min && hit <= minmax_rule.max )
            {
                sendResult(intArray);
            }

        }
    }
}
