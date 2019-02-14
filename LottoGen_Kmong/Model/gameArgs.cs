using System;

namespace LottoGen_Kmong.LottoLogic
{
    public class gameArgs
    {
        private int range;// 45
        private int extract;// 6
        public gameArgs(int range, int extract)
        {
            Extract = extract;//.GetValueOrDefault(); // null return 0
            Range = range;//.GetValueOrDefault();
        }

        public int Extract
        {
            get => extract;
            set
            {
                if (value == 0) throw new System.Exception("게임의 유형을 입력해주세요." + Environment.NewLine+ "  또는 게임의 유형이 0이 될 수 없습니다.");
                extract = value;
            }
        }

        public int Range {
            get => range;
            set
            {
                if (value == 0) throw new System.Exception("게임의 유형을 입력해주세요." + Environment.NewLine+ "  또는 게임의 유형이 0이 될 수 없습니다.");
                range = value;
            }
        }
    }


}
