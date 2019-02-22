using LottoCheck_Kmong.LottoLogic;
using System;
using System.Collections.Generic;

namespace LottoCheck_Kmong.LottoCheckSolution
{
    public class LottoCheckerLogic
    {
        gameGroup aGroup; gameGroup bGroup;
        Action<ICollection<int>> Write_sender;

        public LottoCheckerLogic(gameGroup a, gameGroup b, Action<ICollection<int>> Write_sender)
        {
            aGroup = a;
            bGroup = b;
            this.Write_sender = Write_sender;
        }

        public void doCalculate()
        {

            foreach (var j in bGroup.numberSet)
            {
                int[] count = new int[aGroup.gameargs.setSize+1];
                foreach (var i in aGroup.numberSet)
                {
                   count[ LottoTwoListIsomorphicCount.CalcuateWithExistNumberset(i, j)]++;
                }

                Write_sender(count);
            }
        }
    }
}
