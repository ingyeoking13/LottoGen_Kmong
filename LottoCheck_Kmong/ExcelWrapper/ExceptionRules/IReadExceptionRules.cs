using LottoCheck_Kmong.Services;
using System;

namespace LottoCheck_Kmong.ExcelWrapper.ExceptionRules
{
    public interface IReadExceptionRules
    {
        bool checkWithValue(object value, string name);
    }

    public class ExceptionDecoration : IReadExceptionRules
    {
        public bool checkWithValue(object value, string name)
        {
            return false;
        }
    }
    public class noNullreadException : IReadExceptionRules
    {
        IReadExceptionRules readExceptionRule;

        public noNullreadException(IReadExceptionRules readExceptionRules)
        {
            this.readExceptionRule = readExceptionRules;
        }

        public bool checkWithValue(object value, string name)
        {
            if (value == null || readExceptionRule.checkWithValue(value, name))
            {
                string message = $"UNKOWN ERROR FROM {name}";
                if (name == "ReturnNumberSetAndMinMax") message = "게임 set은 공백일 수 없습니다.";
                else if (name == "ReadGameArgsAndGroupNumberFromfilepath") message = "게임의 유형 또는 그룹 수는 공백 일 수 없습니다.";
                else if (name == "mnNumber" || name == "mxNumber") message = "게임 추출 최소값 최대값은 공백 일 수 없습니다.";
                ExceptionDialogService.getInstance().showMessageAndAllert(message);
                return true;
            }
            return false;
        }
    }

    public class noNegativereadException : IReadExceptionRules 
    {
        IReadExceptionRules readExceptionRule;

        public noNegativereadException(IReadExceptionRules readExceptionRules) { this.readExceptionRule = readExceptionRules; }

        public bool checkWithValue(object value, string name)
        {
            if ((int)value < 0 || readExceptionRule.checkWithValue(value, name))
            {
                string message = $"UNKOWN ERROR FROM {name}";
                if (name == "ReturnNumberSetAndMinMax") message = "게임 set은 숫자 외의 문자이거나, 음수 일 수 없습니다.";
                else if (name == "ReadGameArgsAndGroupNumberFromfilepath") message = "게임의 유형 또는 그룹 수는 숫자 외의 문자이거나, 음수 일 수 없습니다.";
                else if (name == "mnNumber" || name == "mxNumber") message = "게임 추출 최소값 최대값은 숫자 외의 문자이거나, 음수 일 수 없습니다.";
                ExceptionDialogService.getInstance().showMessageAndAllert(message);
                return true;
            }
            return false;
        }
    }
}
