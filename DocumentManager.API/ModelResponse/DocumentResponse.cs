using DocumentManager.Domain.Model;
using System;
using System.Collections.Generic;

namespace DocumentManager.API.ModelResponse
{
    public class DocumentResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime LastUpdate { get; set; }

        public List<FileLinkResponse> Files { get; set; }
        public List<PictureLinkResponse> Pictures { get; set; }
        public List<VideoLinkResponse> Videos { get; set; }

        public DocumentResponse()
        {
            Files = new List<FileLinkResponse>();
            Pictures = new List<PictureLinkResponse>();
            Videos = new List<VideoLinkResponse>();
        }

        public DocumentResponse(Guid id, string title, string content, string description, DateTime creationTime, DateTime lastUpdate,
           List<FileLinkResponse> fileLink, List<PictureLinkResponse> pictureLink, List<VideoLinkResponse> videoLink)
        {
            Id = id;
            Title = title;
            Content = content;
            Description = description;
            CreationTime = creationTime;
            LastUpdate = lastUpdate;
            Files = fileLink;
            Pictures = pictureLink;
            Videos = videoLink;
        }
    }
}
