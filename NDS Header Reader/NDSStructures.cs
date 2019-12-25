using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NDS_Header_Reader
{
    public struct NDSHeader
    {
        //BASIC INFO
        public byte[] GameTitle;
        public byte[] GameCode;
        public ushort MakerCode;
        public byte UnitCode;
        public byte EncryptionSeed;
        public byte DeviceCapability;
        public byte[] Reserved;
        public byte GameRevision;
        public byte NDSRegion;
        public byte ROMVersion;
        public byte InternalFlags;

        //ASSEMBLY INFO
        public uint ARM9RomOffset;
        public uint ARM9EntryPoint;
        public uint ARM9LoadAddress;
        public uint ARM9Size;

        public uint ARM7RomOffset;
        public uint ARM7EntryPoint;
        public uint ARM7LoadAddress;
        public uint ARM7Size;

        //FILE SYSTEM INFO
        public uint FNTOffset;
        public uint FNTLength;

        public uint FATOffset;
        public uint FATLength;

        public uint ARM9OverlayOffset;
        public uint ARM9OverlayLength;
        public uint ARM7OverlayOffset;
        public uint ARM7OverlayLength;

        //MORE STUFF
        public uint NormalCardControlRegisterSettings;
        public uint SecureCardControlRegisterSettings;
        public uint IconBannerOffset;
        public ushort SecureArea;
        public ushort SecureTransferTimeOut;
        public uint ARM9Autoload;
        public uint ARM7Autoload;
        public ulong SecureDisable;
        public uint NTRRegionRomSize;
        public uint HeaderSize;
        public byte[] ReservedDSi;
        public byte[] NintendoLogo;
        public ushort NintendoLogoCRC;
        public ushort HeaderCRC;
        public byte[] DebbugerReserved;
    }

    public struct NDSiHeader
    {
        public byte[] GlobalMBK1MBK5Settings;
        public byte[] LocalMBK6MBK8SettingsARM9;
        public byte[] LocalMBK6MBK8SettingsARM7;
        public uint GlobalMBK9Setting;

        public uint RegionFlags;
        public uint AccessControl;
        public uint ARM7SCFGEXTMask;
        public uint ReservedFlags;

        public uint ARM9iOffset;
        public uint Reserved;
        public uint ARM9iLoadAddress;
        public uint ARM9iSize;
        public uint ARM7iOffset;
        public uint UnknownPointer;
        public uint ARM7iLoadAddress;
        public uint ARM7iSize;

        public uint DigestNTRRegionOffset;
        public uint DigestNTRRegionLength;
        public uint DigestTWLRegionOffset;
        public uint DigestTWLRegionLength;
        public uint DigestSectorHashtableOffset;
        public uint DigestSectorHashtableLength;
        public uint DigestBlockHashtableOffset;
        public uint DigestBlockHashtableLength;
        public uint DigestSectorSize;
        public uint DigestBlockSectorCount;

        public uint IconBannerSize;
        public uint Unknwown;
        public uint NTRTWLRegionROMSize;
        public byte[] Unknown2;

        public uint ModCryptArea1Offset;
        public uint ModCryptArea1Size;
        public uint ModCryptArea2Offset;
        public uint ModCryptArea2Size;

        public ulong TitleID;
        public uint DSiWarePublicSavSize;
        public uint DSiWarePrivateSavSize;
        public byte[] ReservedZero;
        public byte[] Unknown3;

        public byte[] ARM9iSHA1HMACHash;
        public byte[] ARM7iSHA1HMACHash;
        public byte[] DigestMasterSHA1HMACHash;
        public byte[] BannerSHA1HMACHFlash;
        public byte[] ARM9iDecryptedSHA1HMACHash;
        public byte[] ARM7iDecryptedSHA1HMACHash;
    }
}
