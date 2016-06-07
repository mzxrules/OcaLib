using mzxrules.Helper;
using System.IO;

namespace mzxrules.OcaLib
{
    public class ParticleEffectOverlayRecord : OverlayTableRecord
    {
        public int ParticleEffect { get; protected set; }
        public FileAddress RamAddress { get; private set; }
        public N64Ptr UnknownPtr { get; private set; }
        public uint Unknown1 { get; private set; }
        public ParticleEffectOverlayRecord()
        { }

        public ParticleEffectOverlayRecord(int index, BinaryReader br)
        {
            Initialize(index, br);
        }

        public ParticleEffectOverlayRecord(int index, byte[] data)
        {
            using (BinaryReader br = new BinaryReader(new MemoryStream(data)))
                Initialize(index, br);
        }

        private void Initialize(int index, BinaryReader br)
        {
            ParticleEffect = index;
            uint ramAddress;
            VRom = new FileAddress(br.ReadBigUInt32(), br.ReadBigUInt32());
            VRam = new FileAddress(br.ReadBigUInt32(), br.ReadBigUInt32());

            ramAddress = br.ReadBigUInt32();
            RamAddress = (ramAddress == 0) ? new FileAddress() : new FileAddress(ramAddress, ramAddress + VRam.Size);
            UnknownPtr = br.ReadBigUInt32();
            Unknown1 = br.ReadBigUInt32();
        }
    }
}
