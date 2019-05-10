using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NDS_Header_Reader
{
    class NDSReaderMain
    {
        static void Main(string[] args)
        {
            try
            {
                if (args.Length != 0)
                {
                    NDSParser ndsParser = new NDSParser();
                    ndsParser.readROMHeader(args[0]);
                    ndsParser.PrintAllInformation();
                }
                else
                {
                    Console.WriteLine("Usage: NDS Header Reader.exe <rompath>");
                }
            }catch(Exception)
            {
                Console.WriteLine("Usage: NDS Header Reader.exe <rompath>");
            }
            Console.Read();
        }
    }
}
