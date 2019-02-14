using System;
using System.Collections.Generic;
using System.Linq;

namespace LottoGen_Kmong.LottoLogic
{
    public class Lottologic
    {
        public gameArgs gameRule; //  A to B, 45 to 6
        public Rule_minmaxArgs minmax_rule; // 최소 최대
        public IEnumerable<int> wanna_Set; // [ .... ] 
        public Action<IEnumerable<byte>> writerDoWrite;

        public Lottologic(gameArgs gameRule, Rule_minmaxArgs minmax_rule, IEnumerable<int> wanna_Set, Action<IEnumerable<byte>> writerDoWrite)
        {
            this.gameRule = gameRule;
            this.minmax_rule = minmax_rule;
            this.wanna_Set = wanna_Set;
            this.writerDoWrite = writerDoWrite;
        }

        public void calculate()
        {
            Random random = new Random(DateTime.Now.Millisecond);
            int extract = random.Next(minmax_rule.min, minmax_rule.max+1);

            // wanna_Set 에서 extract 갯수만큼 빼줌 
            bool[] chk = new bool[wanna_Set.Count()];

            SelectExtractNumberFromSet(0, extract, chk, 0);
        }

        private void SelectExtractNumberFromSet(int depth, int depth_max, bool[] chk, int j)
        {
            if ( depth == depth_max)
            {
                GoConcatSelectedNumberAndRangedNumber(chk);
                return;
            }

            for (int i=j; i<wanna_Set.Count(); i++)
            {
                if (chk[i]) continue;

                chk[i] = true;
                SelectExtractNumberFromSet(depth + 1, depth_max, chk, j+1);
                chk[i] = false;
            }
        }

        private void GoConcatSelectedNumberAndRangedNumber(bool[] chk)
        {
            List<byte> result = new List<byte>();
            int k = gameRule.Extract;
            bool[] total = new bool[gameRule.Range + 1];
            Console.Write("== 선택 : ");

            for (int i=0; i<chk.Length; i++)
            {
                if (chk[i])
                {
                    result.Add((byte)wanna_Set.ElementAt(i));
                    total[wanna_Set.ElementAt(i)] = true;
                    k--;
                    Console.Write($"{wanna_Set.ElementAt(i)} ");
                }
                else total[wanna_Set.ElementAt(i)] = true;
            }
            Console.WriteLine("가 선택되었습니다.");

            // RangedNumber permutaition
            goPermutationRangedNumber(total, result, k, 1);

        }

        private void goPermutationRangedNumber(bool[] total, List<byte> result, int k, int j)
        {
            if (k == 0 )
            {
                writerDoWrite(result);
                return;
            }

            int error_cnt = 0;
            for (int i=j; i<total.Length; i++)
            {
                if (total[i])
                {
                    error_cnt++;

                    if (error_cnt >= gameRule.Range) throw new ArgumentException("excel 파일 논리에 문제가 있습니다. 사용자의 실수입니다. ");
                    continue;
                }

                total[i] = true;
                result.Add((byte)i);

                goPermutationRangedNumber(total, result, k-1, j+1);

                result.Remove((byte)i);
                total[i] = false;
            }
        }

    }


}
