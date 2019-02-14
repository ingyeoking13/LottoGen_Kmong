using System;
using System.Collections.Generic;
using System.IO;

namespace LottoGen_Kmong.LottoLogic
{
    public interface ILogicWithNumberSet
    {
        Action<IEnumerable<byte>> SendResult { get; set; }
        IEnumerable<int> Wanna_Set { get; set; }
        Rule_minmaxArgs Minmax_rule { get; set; } // 최소 최대
        void Calculate();
        void CalcuateWithExistNumberset(StreamReader streamReader);

    }
}