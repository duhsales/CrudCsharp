using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Util
{
    public class Result
    {
        private bool IsResult { get; set; }
        private string Message { get; set; }

        public void setResult(bool isResult)
        {
            this.IsResult = isResult;
        }

        public void setMessage(string str)
        {
            Message = str;
        }

        public bool isResult()
        {
            return IsResult;
        }

        public string getMessage()
        {
            return Message;
        }

        public Result()
        {
            IsResult = true;
            Message = string.Empty;
        }

        public Result(bool bResult)
        {
            IsResult = bResult;
            Message = string.Empty;
        }
    }
}
