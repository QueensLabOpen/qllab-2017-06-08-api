using System;

namespace QLLAB.Models
{
    public class Content
    {
        public Guid BlobId { get; set; }
        public string Tags { get; set; }
        public string Filename { get; set; }
        public string Data { get; set; }
        public byte[] ByteData { get; set; }
    }
}
