using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Profile
{
    public class clsParameters
    {
        public string Name;
        public string Value;
        public string Direction;
        public clsParameters(string strName, string strValue, string strDirection)
        {
            Name = strName;
            Value = strValue;
            Direction = strDirection;
        }
    }
}