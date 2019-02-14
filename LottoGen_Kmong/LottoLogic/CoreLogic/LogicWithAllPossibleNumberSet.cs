using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LottoGen_Kmong.LottoLogic
{
    public class LogicWithAllPossibleNumberSet : ILogicWithNumberSet
    {
        public gameArgs gameRule; //  A to B, 45 to 6
        private Action<IEnumerable<byte>> sendResult;

        public Action<IEnumerable<byte>> SendResult { get => sendResult; set => sendResult = value; }
        public IEnumerable<int> Wanna_Set { get; set; }
        public Rule_minmaxArgs Minmax_rule { get; set; }

        public LogicWithAllPossibleNumberSet(Rule_minmaxArgs minmax_rule, gameArgs gameRule, IEnumerable<int> wanna_Set)
        {
            Minmax_rule = minmax_rule;
            Wanna_Set = wanna_Set;
            this.gameRule = gameRule;
        }

        public void Calculate()
        {
            Random random = new Random(DateTime.Now.Millisecond);
            int extract = random.Next(Minmax_rule.min, Minmax_rule.max + 1);

            // wanna_Set 에서 extract 갯수만큼 빼줌 
            bool[] chk = new bool[Wanna_Set.Count()];

            SelectExtractNumberFromSet(0, extract, chk, 0);
        }

        private void SelectExtractNumberFromSet(int depth, int depth_max, bool[] chk, int j)
        {
            if (depth == depth_max)
            {
                GoConcatSelectedNumberAndRangedNumber(chk);
                return;
            }

            for (int i = j; i < Wanna_Set.Count(); i++)
            {
                if (chk[i]) continue;

                chk[i] = true;
                SelectExtractNumberFromSet(depth + 1, depth_max, chk, j + 1);
                chk[i] = false;
            }
        }

        private void GoConcatSelectedNumberAndRangedNumber(bool[] chk)
        {
            List<byte> result = new List<byte>();
            int k = gameRule.Extract;
            bool[] total = new bool[gameRule.Range + 1];
            Console.Write("== 선택 : ");

            for (int i = 0; i < chk.Length; i++)
            {
                if (chk[i])
                {
                    result.Add((byte)Wanna_Set.ElementAt(i));
                    total[Wanna_Set.ElementAt(i)] = true;
                    k--;
                    Console.Write($"{Wanna_Set.ElementAt(i)} ");
                }
                else total[Wanna_Set.ElementAt(i)] = true;
            }
            Console.WriteLine("가 선택되었습니다.");

            // RangedNumber permutaition
            goPermutationRangedNumber(total, result, k, 1);
        }

        private void goPermutationRangedNumber(bool[] total, List<byte> result, int k, int j)
        {
            if (k == 0)
            {
                sendResult(result);
                return;
            }

            int error_cnt = 0;
            for (int i = j; i < total.Length; i++)
            {
                if (total[i])
                {
                    error_cnt++;

                    if (error_cnt >= gameRule.Range) throw new ArgumentException("excel 파일 논리에 문제가 있습니다. 사용자의 실수입니다. ");
                    continue;
                }

                total[i] = true;
                result.Add((byte)i);

                goPermutationRangedNumber(total, result, k - 1, j + 1);

                result.Remove((byte)i);
                total[i] = false;
            }
        }

        public void CalcuateWithExistNumberset(StreamReader streamReader) { throw new NotImplementedException(); }
    }
}
