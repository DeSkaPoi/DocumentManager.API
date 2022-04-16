using System;

namespace DocumentManager.Domain
{
    public class VideoLink
    {
        public Guid Id { get; set; }
        //public string ServiceUrl { get; set; }
        public Guid VideoId { get; set; }
        public Document Document { get; set; }
        public VideoLink(Guid videoId)
        {
            VideoId = videoId;
        }
    }
}
