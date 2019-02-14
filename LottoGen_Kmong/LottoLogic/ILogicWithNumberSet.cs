using System;
using System.Collections.Generic;

namespace LottoGen_Kmong.LottoLogic
{
    public interface ILogicWithNumberSet
    {
        Action<IEnumerable<byte>> SendResult { get; set; }
        void Calculate();
    }
}