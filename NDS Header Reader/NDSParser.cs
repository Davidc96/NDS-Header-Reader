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
        private NDSiHeader ndsiHeader;

        private bool includedDSiHeader;

        public NDSParser()
        {
            includedDSiHeader = false;
        }

        public void readROMHeader(string path)
        {
            Console.WriteLine("[+] Opening ROM: " + path + "...");

            ndsHeader = new NDSHeader();
            ndsiHeader = new NDSiHeader();

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


            //READ DSI HEADER

            if(ndsHeader.UnitCode == 2 || ndsHeader.UnitCode == 3)
            {
                includedDSiHeader = true;

                ndsiHeader.GlobalMBK1MBK5Settings = bin.ReadBytes(20);
                ndsiHeader.LocalMBK6MBK8SettingsARM9 = bin.ReadBytes(12);
                ndsiHeader.LocalMBK6MBK8SettingsARM7 = bin.ReadBytes(12);
                ndsiHeader.GlobalMBK9Setting = bin.ReadUInt32();

                ndsiHeader.RegionFlags = bin.ReadUInt32();
                ndsiHeader.AccessControl = bin.ReadUInt32();
                ndsiHeader.ARM7SCFGEXTMask = bin.ReadUInt32();
                ndsiHeader.ReservedFlags = bin.ReadUInt32();

                ndsiHeader.ARM9iOffset = bin.ReadUInt32();
                ndsiHeader.Reserved = bin.ReadUInt32();
                ndsiHeader.ARM9iLoadAddress = bin.ReadUInt32();
                ndsiHeader.ARM9iSize = bin.ReadUInt32();
                ndsiHeader.ARM7iOffset = bin.ReadUInt32();
                ndsiHeader.UnknownPointer = bin.ReadUInt32();
                ndsiHeader.ARM7iLoadAddress = bin.ReadUInt32();
                ndsiHeader.ARM7iSize = bin.ReadUInt32();

                ndsiHeader.DigestNTRRegionOffset = bin.ReadUInt32();
                ndsiHeader.DigestNTRRegionLength = bin.ReadUInt32();
                ndsiHeader.DigestTWLRegionOffset = bin.ReadUInt32();
                ndsiHeader.DigestTWLRegionLength = bin.ReadUInt32();
                ndsiHeader.DigestSectorHashtableOffset = bin.ReadUInt32();
                ndsiHeader.DigestSectorHashtableLength = bin.ReadUInt32();
                ndsiHeader.DigestBlockHashtableOffset = bin.ReadUInt32();
                ndsiHeader.DigestBlockHashtableLength = bin.ReadUInt32();
                ndsiHeader.DigestSectorSize = bin.ReadUInt32();
                ndsiHeader.DigestBlockSectorCount = bin.ReadUInt32();

                ndsiHeader.IconBannerSize = bin.ReadUInt32();
                ndsiHeader.Unknwown = bin.ReadUInt32();
                ndsiHeader.NTRTWLRegionROMSize = bin.ReadUInt32();
                ndsiHeader.Unknown2 = bin.ReadBytes(20);

                ndsiHeader.ModCryptArea1Offset = bin.ReadUInt32();
                ndsiHeader.ModCryptArea1Size = bin.ReadUInt32();
                ndsiHeader.ModCryptArea2Offset = bin.ReadUInt32();
                ndsiHeader.ModCryptArea2Size = bin.ReadUInt32();

                ndsiHeader.TitleID = bin.ReadUInt64();
                ndsiHeader.DSiWarePublicSavSize = bin.ReadUInt32();
                ndsiHeader.DSiWarePrivateSavSize = bin.ReadUInt32();
                ndsiHeader.ReservedZero = bin.ReadBytes(176);
                ndsiHeader.Unknown3 = bin.ReadBytes(16);

                ndsiHeader.ARM9iSHA1HMACHash = bin.ReadBytes(20);
                ndsiHeader.ARM7iSHA1HMACHash = bin.ReadBytes(20);
                ndsiHeader.DigestMasterSHA1HMACHash = bin.ReadBytes(20);
                ndsiHeader.BannerSHA1HMACHFlash = bin.ReadBytes(20);
                ndsiHeader.ARM9iDecryptedSHA1HMACHash = bin.ReadBytes(20);
                ndsiHeader.ARM7iDecryptedSHA1HMACHash = bin.ReadBytes(20);

            }

            Console.WriteLine("[+] ROM Header readed successfully!");
            Console.WriteLine("");
            bin.Close();

        }

        public void PrintNDSBasicInformation()
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

        public void PrintNDSAssemblyInformation()
        {
            Console.WriteLine("------------------ ASSEMBLY INFO -------------------");
            Console.WriteLine("");
            Console.WriteLine("ARM9 ROM OFFSET: " + $"0x{ndsHeader.ARM9RomOffset:X}" + "              " + "ARM7 ROM OFFSET: " + $"0x{ndsHeader.ARM7RomOffset:X}");
            Console.WriteLine("ARM9 ENTRYPOINT: " + $"0x{ndsHeader.ARM9EntryPoint:X}" + "           " + "ARM7 ENTRYPOINT: " + $"0x{ndsHeader.ARM7EntryPoint:X}");
            Console.WriteLine("ARM9 RAM ENTRY:  " + $"0x{ndsHeader.ARM9LoadAddress:X}" + "           " + "ARM7 RAM ENTRY:  " +  $"0x{ndsHeader.ARM7LoadAddress:X}");
            Console.WriteLine("ARM9 SIZE:       " + $"0x{ndsHeader.ARM9Size:X}" + "             " + "ARM7 SIZE:       " + $"0x{ndsHeader.ARM7Size:X}");
            Console.WriteLine("");
        }

        public void PrintNDSFileSystemInformation()
        {
            Console.WriteLine("---------------- FILE SYSTEM INFO ------------------");
            Console.WriteLine("");
            Console.WriteLine("FNT OFFSET: " + $"0x{ndsHeader.FNTOffset}");
            Console.WriteLine("FNT LENGTH: " + $"0x{ndsHeader.FNTLength}");
            Console.WriteLine("FAT OFFSET: " + $"0x{ndsHeader.FATOffset}");
            Console.WriteLine("FAT LENGTH: " + $"0x{ndsHeader.FATLength}");
            Console.WriteLine("");
            Console.WriteLine("ARM9 OVERLAY TABLE OFFSET: " + $"0x{ndsHeader.ARM9OverlayOffset:X}" + "          " + "ARM7 OVERLAY OFFSET: " + $"0x{ndsHeader.ARM7OverlayOffset:X}");
            Console.WriteLine("ARM9 OVERLAY TABLE LENGTH: " + $"0x{ndsHeader.ARM9OverlayLength:X}" + "           " + "ARM7 OVERLAY LENGTH: " + $"0x{ndsHeader.ARM7OverlayLength:X}");
            Console.WriteLine("");
        }
        public void PrintNDSExtendedInformation()
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
            Console.WriteLine("");
        }

        public void PrintDSiWareSettingsInformation()
        {
            Console.WriteLine("-------------- DSI SETTINGS INFORMATION ------------------");
            Console.WriteLine("");
            Console.WriteLine("GLOBAL MBK1 - MBK5 SETTINGS:     " + Utility.GetHexFromBytes(ndsiHeader.GlobalMBK1MBK5Settings));
            Console.WriteLine("LOCAL MBK6 - MBK8 SETTINGS ARM9: " + Utility.GetHexFromBytes(ndsiHeader.LocalMBK6MBK8SettingsARM9));
            Console.WriteLine("LOCAL MBK6 - MBK8 SETTINGS ARM7: " + Utility.GetHexFromBytes(ndsiHeader.LocalMBK6MBK8SettingsARM7));
            Console.WriteLine("GLOBAL MBK9 SETTINGS:            " + $"0x{ndsiHeader.GlobalMBK9Setting:X}");
            Console.WriteLine("");
        }

        public void PrintDSiWareExtendedAssemblyInformation()
        {
            Console.WriteLine("------------ DSI EXTENDED ASSEMBLY INFORMATION -----------");
            Console.WriteLine("");
            Console.WriteLine("ARM9i ROM OFFSET:   " + $"0x{ndsiHeader.ARM9iOffset:X}" + "              " + "ARM7i ROM OFFSET:   " + $"0x{ndsiHeader.ARM7iOffset:X}");
            Console.WriteLine("ARM9i LOAD ADDRESS: " + $"0x{ndsiHeader.ARM9iLoadAddress:X}" + "              " + "ARM7i LOAD ADDRESS: " + $"0x{ndsiHeader.ARM7iLoadAddress:X}");
            Console.WriteLine("ARM9i SIZE:         " + $"0x{ndsiHeader.ARM9iSize:X}" + " (" + ndsiHeader.ARM9iSize + " bytes)" + "  " + "ARM7i SIZE:         " + $"0x{ndsiHeader.ARM7iSize:X}" + " (" + ndsiHeader.ARM7iSize + " bytes)");
            Console.WriteLine("");
        }

        public void PrintDSiWareDigestInformation()
        {
            Console.WriteLine("------------ DSI DIGEST INFORMATION -------------");
            Console.WriteLine("");
            Console.WriteLine("NTR REGION OFFSET:       " + $"0x{ndsiHeader.DigestNTRRegionOffset:X}");
            Console.WriteLine("NTR REGION LENGTH:       " + $"0x{ndsiHeader.DigestNTRRegionLength:X}" + " (" + (ndsiHeader.DigestNTRRegionLength / 1000000.0).ToString("n2") + " MB)");
            Console.WriteLine("TWL REGION OFFSET:       " + $"0x{ndsiHeader.DigestTWLRegionOffset:X}");
            Console.WriteLine("TWL REGION LENGTH:       " + $"0x{ndsiHeader.DigestTWLRegionLength:X}" + " (" + (ndsiHeader.DigestTWLRegionLength / 1024.0).ToString("n2") + " KB)");
            Console.WriteLine("");
            Console.WriteLine("SECTOR HASHTABLE OFFSET: " + $"0x{ndsiHeader.DigestSectorHashtableOffset:X}");
            Console.WriteLine("SECTOR HASHTABLE LENGTH: " + $"0x{ndsiHeader.DigestSectorHashtableLength:X}" + " (" + (ndsiHeader.DigestSectorHashtableLength / 1024.0).ToString("n2") + " KB)");
            Console.WriteLine("BLOCK HASHTABLE OFFSET:  " + $"0x{ndsiHeader.DigestBlockHashtableOffset:X}");
            Console.WriteLine("BLOCK HASHTABLE LENGTH:  " + $"0x{ndsiHeader.DigestBlockHashtableLength:X}" + " ("+(ndsiHeader.DigestBlockHashtableLength / 1024.0).ToString("n2") +" KB)");
            Console.WriteLine("SECTOR SIZE: " + $"0x{ndsiHeader.DigestSectorSize:X}" + " (" + ndsiHeader.DigestSectorSize + ")");
            Console.WriteLine("BLOCK COUNT: " + ndsiHeader.DigestBlockSectorCount);
            Console.WriteLine("");
        }

        public void PrintDsiWareModCryptInformation()
        {
            Console.WriteLine("------------ DSI MODCRYPT INFORMATION -----------");
            Console.WriteLine("");
            Console.WriteLine("MODCRYPT AREA1 OFFSET: " + $"0x{ndsiHeader.ModCryptArea1Offset:X}");
            Console.WriteLine("MODCRYPT AREA1 SIZE:   " + $"0x{ndsiHeader.ModCryptArea1Size:X}" + " (" + ndsiHeader.ModCryptArea1Size + ")");
            Console.WriteLine("MODCRYPT AREA2 OFFSET: " + $"0x{ndsiHeader.ModCryptArea2Offset:X}");
            Console.WriteLine("MODCRYPT AREA2 SIZE:   " + $"0x{ndsiHeader.ModCryptArea2Size:X}" + " (" + ndsiHeader.ModCryptArea2Size + ")");
            Console.WriteLine("");
        }

        public void PrintAllInformation()
        {
            PrintNDSBasicInformation();
            PrintNDSAssemblyInformation();
            PrintNDSFileSystemInformation();
            PrintNDSExtendedInformation();

            if(includedDSiHeader)
            {
                Console.WriteLine("---------------- DSI HEADERS ----------------");
                PrintDSiWareSettingsInformation();
                PrintDSiWareExtendedAssemblyInformation();
                PrintDSiWareDigestInformation();
                PrintDsiWareModCryptInformation();
            }
        }
    }
}
