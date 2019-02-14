using System;
using System.Linq;

namespace LottoGen_Kmong.Helper
{
    public static class IntToStringConversionByBase
    {
        public static string IntToStringConversion(int input, int basenum, char off)
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
