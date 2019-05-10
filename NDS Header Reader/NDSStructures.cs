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
}
