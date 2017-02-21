using mzxrules.Helper;
namespace mzxrules.OcaLib.SceneRoom.Commands
{
    public interface ISegmentAddressAsset
    {
        SegmentAddress SegmentAddress { get; set; }
        void Initialize(System.IO.BinaryReader br);
    }
}
