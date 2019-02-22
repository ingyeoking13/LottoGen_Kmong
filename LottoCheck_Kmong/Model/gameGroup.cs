using System.Collections.Generic;

namespace LottoCheck_Kmong.LottoLogic
{
    public class gameGroup
    {
        public gameArgs gameargs;
        public List<List<byte>> numberSet= new List<List<byte>>();

        public gameGroup()
        {
        }

        public gameGroup( List<List<byte>> numberSet, gameArgs gameargs)
        {
            this.numberSet = numberSet;
            this.gameargs = gameargs;
        }
    }
}
