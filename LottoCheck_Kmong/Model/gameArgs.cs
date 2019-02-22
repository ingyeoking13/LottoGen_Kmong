using LottoCheck_Kmong.Services;
using System;

namespace LottoCheck_Kmong.LottoLogic
{
    public class gameArgs
    {
        public int range;// 45
        public int setSize;// 6
        public int gameNumber;

        public gameArgs() { }

        public gameArgs(int range, int setSize, int gameNumber)
        {
            this.setSize = setSize;//.GetValueOrDefault(); // null return 0
            this.range = range;//.GetValueOrDefault();
            this.gameNumber = gameNumber;
        }

    }
}
