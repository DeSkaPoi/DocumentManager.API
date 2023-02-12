using System;

namespace DocumentManager.Domain.Model
{
    public class VideoLink
    {
        public Guid Id { get; }
        public Guid VideoId { get; }
        public VideoLink(Guid id, Guid videoId)
        {
            Id = id;
            VideoId = videoId;
        }
    }
}
