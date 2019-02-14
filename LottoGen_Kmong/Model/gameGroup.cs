using System.Collections.Generic;

namespace LottoGen_Kmong.LottoLogic
{
    public class gameGroup
    {
        public Rule_minmaxArgs rule_MinmaxArgs;
        public List<int> numberSet;

        public gameGroup(Rule_minmaxArgs rule_MinmaxArgs, List<int> numberSet)
        {
            this.rule_MinmaxArgs = rule_MinmaxArgs;
            this.numberSet = numberSet;
        }
    }
}
