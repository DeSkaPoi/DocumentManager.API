using DocumentManager.Infrastructure.ModelDB;
using System;
using System.Collections.Generic;

namespace DocumentManager.Domain.Model
{
    public class Document
    {
        public Guid Id { get; }
        public string Title { get; }
        public string Content { get; }
        public string Description { get; }
        public DateTime CreationTime { get; }
        public DateTime LastUpdate { get; }

        public List<FileLink> Files { get; }
        public List<PictureLink> Pictures { get; }
        public List<VideoLink> Videos { get; }

        public Document()
        {
            Files = new List<FileLink>();
            Pictures = new List<PictureLink>();
            Videos = new List<VideoLink>();
        }
        public Document(Guid id, string title, string content, string description, DateTime creationTime, DateTime lastUpdate,
            List<FileLink> fileLink, List<PictureLink> pictureLink, List<VideoLink> videoLink)
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
