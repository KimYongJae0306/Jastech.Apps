using Jastech.Apps.Winform.Service.Plc.Maps;

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
            if (wordType == WordType.DoubleWord)
                WordSize = 2;
            else
                WordSize = wordSize;
        }

        public PlcAddressMap(PlcCommonMap name, WordType wordType, int addressNum, int wordSize)
        {
            Name = name.ToString();
            WordType = wordType;
            AddressNum = addressNum;

            if (wordType == WordType.DoubleWord)
                WordSize = 2;
            else
                WordSize = wordSize;
        }
    }

    public enum WordType
    {
        DoubleWord,
        HEX,
        DEC,
    }
}
