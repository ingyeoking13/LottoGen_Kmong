using System.Collections.Generic;

namespace LottoCheck_Kmong.LottoCheckSolution
{
    public class LottoTwoListIsomorphicCount : ILottoLogic
    {
        /// <summary>
        /// 두 리스트에 공통 인자를 셉니다. 
        /// </summary>
        /// <param name="target_array"></param>
        /// <param name="source_array"></param>
        /// <returns></returns>
        public static int CalcuateWithExistNumberset(ICollection<byte> target_array, ICollection<byte> source_array)
        {
            int hit = 0;

            foreach (var i in target_array)
            {
                foreach (int j in source_array)
                {
                    if (i == j) hit++;
                }
            };

            return hit;
        }
    }
}
