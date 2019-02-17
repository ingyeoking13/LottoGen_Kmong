using System;
using System.Collections.Generic;

namespace LottoGen_Kmong.LottoLogic.CoreLogic
{
    public class CreateAllPossibleNumber
    {
        public gameArgs Gameargs { get; }
        public Action<IEnumerable<byte>> WriterWrite { get; }
        private bool[] chk;

        public CreateAllPossibleNumber(gameArgs gameargs, Action<IEnumerable<byte>>  writerWrite)
        {
            Gameargs = gameargs;
            WriterWrite = writerWrite;
            chk= new bool[Gameargs.Range + 1];
        }

        public void createAllPossibleNumber()
        {

            int now = 1;
            go(now, Gameargs.Range, 0, Gameargs.Extract);
        }
        public void go(int now, int mx, int cnt, int mxcnt)
        {
            if ( cnt == mxcnt)
            {
                List<byte> ret= new List<byte>();
                for (byte i=1; i<=(byte)mx; i++)
                {
                    if (chk[i]) ret.Add(i);
                }

                WriterWrite(ret);
                return;
            }
            if (now > mx)
            {
                return;
            }

            for (int i=now; i<=mx; i++)
            {
                chk[i] = true;
                go(i + 1, mx, cnt + 1, mxcnt);
                chk[i] = false;
            }

        }

    }
}
