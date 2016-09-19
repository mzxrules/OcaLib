using mzxrules.Helper;
using System.IO;

namespace mzxrules.OcaLib
{
    public class EntranceTableRecord
    {
        public byte SceneIndex;
        public byte SpawnIndex;
        public short Variables;

        public EntranceTableRecord (BinaryReader br)
        {
            SceneIndex = br.ReadByte();
            SpawnIndex = br.ReadByte();
            Variables = br.ReadBigInt16();
        }
    }
}
