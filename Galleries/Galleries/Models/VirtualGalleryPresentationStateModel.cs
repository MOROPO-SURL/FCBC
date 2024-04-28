namespace MOROPO.FCBC.Galleries.Models
{
    public class VirtualGalleryPresentationStateModel
    {
        public Urho.Vector3 CameraPosition { get; set; }
        
        public Urho.Quaternion CameraRotation { get; set; }

        public float Yaw { get; set; }

        public float Pitch { get; set; }
    }
}
