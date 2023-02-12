using System;

namespace DocumentManager.API.ModelResponse
{
    public class VideoLinkResponse
    {
        public Guid Id { get; }
        public Guid VideoId { get; }
        public VideoLinkResponse(Guid id, Guid videoId)
        {
            Id = id;
            VideoId = videoId;
        }
    }
}
