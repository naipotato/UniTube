namespace UniTube.Core.Resources
{
    public class Thumbnails
    {
        public Thumbnail Default { get; set; }
        public Thumbnail Medium { get; set; }
        public Thumbnail High { get; set; }
        public Thumbnail Standard { get; set; }
        public Thumbnail MaxRes { get; set; }

        public class Thumbnail
        {
            public string Url { get; set; }
            public uint Width { get; set; }
            public uint Height { get; set; }
        }
    }
}
