using System;

namespace DocumentManager.Infrastructure.ModelDB
{
    public class VideoLinkDataBase
    {
        public Guid Id { get; set; }
        public Guid VideoId { get; set; }
        public DocumentDataBase Document { get; set; }
        public VideoLinkDataBase() { }
        public VideoLinkDataBase(Guid id, Guid videoId)
        {
            Id = id;
            VideoId = videoId;
        }
    }
}
