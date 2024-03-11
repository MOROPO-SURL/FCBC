namespace MOROPO.FCBC.Galleries.Models.View
{
    public class ImageViewModel
    {
        public System.Guid Id { get; set; }

        public string Extension { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public byte[] Data { get; set; }
    }
}