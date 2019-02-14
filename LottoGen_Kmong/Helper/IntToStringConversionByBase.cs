using System;
using System.Linq;

namespace LottoGen_Kmong.Helper
{
    public static class IntToStringConversionByBase
    {
        /// <summary>
        /// 몇 번째 숫자를 표현할지 문자열을 반환합니다.
        /// </summary>
        /// <param name="input">몇 번째 숫자를 표현할 지 선택합니다.</param>
        /// <param name="basenum">base를 선택합니다. default 26입니다.(알파벳기준)</param>
        /// <param name="off">offset을 선택합니다. 해당 인자는 엑셀의 경우 선택될 수 있습니다.</param>
        /// <returns></returns>
        public static string IntToStringConversion(int input, int basenum = 26, char off = 'A')
        {
            string ret ="";
            char offSet = off;
            input += (offSet - 'A');
            input++;

            while(input>0)
            {
                int tmp= (input-1)%basenum;
                ret += (char)('A' + tmp);
                input = (input - tmp) / basenum;
            }
            return new String(ret.Reverse().ToArray());
        }
    }
}
