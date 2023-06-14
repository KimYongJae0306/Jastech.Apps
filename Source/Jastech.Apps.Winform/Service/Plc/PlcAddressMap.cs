using Jastech.Apps.Winform.Service.Plc.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jastech.Apps.Winform.Service.Plc
{
    public class PlcAddressMap
    {
        public string Name { get; private set; }

        public WordType WordType { get; private set; }

        public int AddressNum { get; set; }

        public int WordSize { get; set; }

        public string Value { get; set; }

        public PlcAddressMap(PlcResultMap name, WordType wordType, int addressNum, int wordSize)
        {
            Name = name.ToString();
            WordType = wordType;
            AddressNum = addressNum;
            WordSize = wordSize;
        }

        public PlcAddressMap(PlcCommonMap name, WordType wordType, int addressNum, int wordSize)
        {
            Name = name.ToString();
            WordType = wordType;
            AddressNum = addressNum;
            WordSize = wordSize;
        }
    }

    public enum WordType
    {
        HEX,
        DEC,
    }
}
