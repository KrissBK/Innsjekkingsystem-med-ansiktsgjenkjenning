namespace FaceRecogApp.Models
{
    public class Picture
    {
        public int PictureId { get; set; }

        public Employee Employee { get; set; }

        public byte[] PictureBinary { get; set; }

    }
}
