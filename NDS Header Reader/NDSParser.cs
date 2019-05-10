using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace NDS_Header_Reader
{
    class NDSParser
    {
        private NDSHeader ndsHeader;
        public NDSParser()
        {
            
        }

        public void readROMHeader(string path)
        {
            Console.WriteLine("[+] Opening ROM: " + path + "...");

            ndsHeader = new NDSHeader();
            BinaryReader bin = new BinaryReader(File.OpenRead(path));

            //Read structure
            Console.WriteLine("[+] Reading ROM Header...");
            //BASIC INFORMATION
            ndsHeader.GameTitle = bin.ReadBytes(12);
            ndsHeader.GameCode = bin.ReadBytes(4);
            ndsHeader.MakerCode = bin.ReadUInt16();
            ndsHeader.UnitCode = bin.ReadByte();
            ndsHeader.EncryptionSeed = bin.ReadByte();
            ndsHeader.DeviceCapability = bin.ReadByte();
            ndsHeader.Reserved = bin.ReadBytes(7);
            ndsHeader.GameRevision = bin.ReadByte();
            ndsHeader.NDSRegion = bin.ReadByte();
            ndsHeader.ROMVersion = bin.ReadByte();
            ndsHeader.InternalFlags = bin.ReadByte();

            Console.WriteLine("[+] Reading ROM Assembly Info...");

            //ASSEMBLY INFORMATION
            ndsHeader.ARM9RomOffset = bin.ReadUInt32();
            ndsHeader.ARM9EntryPoint = bin.ReadUInt32();
            ndsHeader.ARM9LoadAddress = bin.ReadUInt32();
            ndsHeader.ARM9Size = bin.ReadUInt32();

            ndsHeader.ARM7RomOffset = bin.ReadUInt32();
            ndsHeader.ARM7EntryPoint = bin.ReadUInt32();
            ndsHeader.ARM7LoadAddress = bin.ReadUInt32();
            ndsHeader.ARM7Size = bin.ReadUInt32();

            Console.WriteLine("[+] Reading File System Info...");

            //FILE SYSTEM INFORMATION
            ndsHeader.FNTOffset = bin.ReadUInt32();
            ndsHeader.FNTLength = bin.ReadUInt32();

            ndsHeader.FATOffset = bin.ReadUInt32();
            ndsHeader.FATLength = bin.ReadUInt32();

            ndsHeader.ARM9OverlayOffset = bin.ReadUInt32();
            ndsHeader.ARM9OverlayLength = bin.ReadUInt32();
            ndsHeader.ARM7OverlayOffset = bin.ReadUInt32();
            ndsHeader.ARM7OverlayLength = bin.ReadUInt32();

            Console.WriteLine("[+] Reading Extended Info....");
            //EXTENDED INFORMATION
            ndsHeader.NormalCardControlRegisterSettings = bin.ReadUInt32();
            ndsHeader.SecureCardControlRegisterSettings = bin.ReadUInt32();

            ndsHeader.IconBannerOffset = bin.ReadUInt32();
            ndsHeader.SecureArea = bin.ReadUInt16();
            ndsHeader.SecureTransferTimeOut = bin.ReadUInt16();
            ndsHeader.ARM9Autoload = bin.ReadUInt32();
            ndsHeader.ARM7Autoload = bin.ReadUInt32();
            ndsHeader.SecureDisable = bin.ReadUInt64();

            ndsHeader.NTRRegionRomSize = bin.ReadUInt32();
            ndsHeader.HeaderSize = bin.ReadUInt32();
            ndsHeader.ReservedDSi = bin.ReadBytes(56);
            ndsHeader.NintendoLogo = bin.ReadBytes(156);
            ndsHeader.NintendoLogoCRC = bin.ReadUInt16();
            ndsHeader.HeaderCRC = bin.ReadUInt16();
            ndsHeader.DebbugerReserved = bin.ReadBytes(32);

            Console.WriteLine("[+] ROM Header readed successfully!");
            Console.WriteLine("");
            bin.Close();

        }

        public void PrintBasicInformation()
        {
            Console.WriteLine("----------------- BASIC INFORMATION ----------------");
            Console.WriteLine("");
            Console.WriteLine("TITLE: " + Encoding.UTF8.GetString(ndsHeader.GameTitle));
            Console.WriteLine("GAMECODE: " + Encoding.UTF8.GetString(ndsHeader.GameCode));
            Console.WriteLine("MAKERCODE: " + $"0x{ndsHeader.MakerCode:X}");
            Console.WriteLine("UNITCODE: " + Utility.GetUnitCode(ndsHeader.UnitCode) + $"(0x{ndsHeader.UnitCode:X})");
            Console.WriteLine("ENCRYPTION SEED SELECT: " + Utility.Binary(ndsHeader.EncryptionSeed));
            Console.WriteLine("DEVICE CAPACITY: " + Utility.CalculateDeviceCapacity(ndsHeader.DeviceCapability) + $" (0x{ndsHeader.DeviceCapability:X})");
            Console.WriteLine("GAME REVISION: " + $"0x{ndsHeader.GameRevision:X}");
            Console.WriteLine("NDS REGION: " + Utility.GetNDSRegion(ndsHeader.NDSRegion) + $"(0x{ndsHeader.NDSRegion:X})");
            Console.WriteLine("ROM VERSION: " + $"0x{ndsHeader.ROMVersion:X}");
            Console.WriteLine("INTERNAL FLAGS: " + Utility.Binary(ndsHeader.InternalFlags));
            Console.WriteLine("");
        }

        public void PrintAssemblyInformation()
        {
            Console.WriteLine("------------------ ASSEMBLY INFO -------------------");
            Console.WriteLine("");
            Console.WriteLine("ARM9 ROM OFFSET: " + $"0x{ndsHeader.ARM9RomOffset:X}" + "              " + "ARM7 ROM OFFSET: " + $"0x{ndsHeader.ARM7RomOffset:X}");
            Console.WriteLine("ARM9 ENTRYPOINT: " + $"0x{ndsHeader.ARM9EntryPoint:X}" + "           " + "ARM7 ENTRYPOINT: " + $"0x{ndsHeader.ARM7EntryPoint:X}");
            Console.WriteLine("ARM9 RAM ENTRY:  " + $"0x{ndsHeader.ARM9LoadAddress:X}" + "           " + "ARM7 RAM ENTRY:  " +  $"0x{ndsHeader.ARM7LoadAddress:X}");
            Console.WriteLine("ARM9 SIZE:       " + $"0x{ndsHeader.ARM9Size:X}" + "             " + "ARM7 SIZE:       " + $"0x{ndsHeader.ARM7Size:X}");
            Console.WriteLine("");
        }

        public void PrintFileSystemInformation()
        {
            Console.WriteLine("---------------- FILE SYSTEM INFO ------------------");
            Console.WriteLine("");
            Console.WriteLine("FNT OFFSET: " + $"0x{ndsHeader.FNTOffset}");
            Console.WriteLine("FNT LENGTH: " + $"0x{ndsHeader.FNTLength}");
            Console.WriteLine("FAT OFFSET: " + $"0x{ndsHeader.FATOffset}");
            Console.WriteLine("FAT LENGTH: " + $"0x{ndsHeader.FATLength}");
            Console.WriteLine("");
            Console.WriteLine("ARM9 OVERLAY OFFSET: " + $"0x{ndsHeader.ARM9OverlayOffset:X}" + "          " + "ARM7 OVERLAY OFFSET: " + $"0x{ndsHeader.ARM7OverlayOffset:X}");
            Console.WriteLine("ARM9 OVERLAY LENGTH: " + $"0x{ndsHeader.ARM9OverlayLength:X}" + "           " + "ARM7 OVERLAY LENGTH: " + $"0x{ndsHeader.ARM7OverlayLength:X}");
            Console.WriteLine("");
        }
        public void PrintExtendedInformation()
        {
            Console.WriteLine("-------------- EXTRA INFO ------------------------");
            Console.WriteLine("");
            Console.WriteLine("NCC REGISTER SETTINGS: " + $"{ndsHeader.NormalCardControlRegisterSettings:X}h");
            Console.WriteLine("SCC REGISTER SETTINGS: " + $"{ndsHeader.SecureCardControlRegisterSettings:X}h");
            Console.WriteLine("SECURE AREA 2K CRC: " + $"   {ndsHeader.SecureArea:X}");
            Console.WriteLine("ARM9 AUTOLOAD: " + $"        0x{ndsHeader.ARM9Autoload:X}");
            Console.WriteLine("ARM7 AUTOLOAD: " + $"        0x{ndsHeader.ARM7Autoload:X}");
            Console.WriteLine("SECURE DISABLE: " + $"       0x{ndsHeader.SecureDisable:X}");
            Console.WriteLine("NTR REGION ROM SIZE:   " + (ndsHeader.NTRRegionRomSize / 1000000.0).ToString("n2") + " MB");
            Console.WriteLine("HEADER SIZE:           " + (ndsHeader.HeaderSize / 1000.0).ToString("n2") + " KB");
        }

        public void PrintAllInformation()
        {
            PrintBasicInformation();
            PrintAssemblyInformation();
            PrintFileSystemInformation();
            PrintExtendedInformation();
        }
    }
}
