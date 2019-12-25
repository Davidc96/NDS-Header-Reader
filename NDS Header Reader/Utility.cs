using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NDS_Header_Reader
{
    class Utility
    {
        public static string Binary(byte value)
        {
            return "B(" + Convert.ToString(value, 2) + ")";
        }

        public static string GetNDSRegion(byte region)
        {
            switch(region)
            {
                case 0:
                    return "NORMAL";
                case 128: //80h
                    return "CHINA";
                case 64: //40h
                    return "KOREA";
                default:
                    return "OTHER";
            }
        }

        public static string GetUnitCode(byte unitCode)
        {
            switch(unitCode)
            {
                case 0:
                    return "NDS";
                case 2:
                    return "NDS + DSi";
                case 3:
                    return "DSi";
                default:
                    return "UNKNOWN";
            }
        }

        public static string CalculateDeviceCapacity(byte deviceCapacity)
        {
            return ((128000 << deviceCapacity) / 1000000.0).ToString("n2") + " MB";
        }
        
        public static string GetHexFromBytes(byte[] bytes)
        {
            string completeHex = "";

            foreach(byte b in bytes)
            {
                completeHex += $"{b:X}";
            }

            return completeHex;
        }
    }
}
