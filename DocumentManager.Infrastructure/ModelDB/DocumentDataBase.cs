using System;
using System.Collections.Generic;

namespace DocumentManager.Infrastructure.ModelDB
{
    public class DocumentDataBase
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime LastUpdate { get; set; }

        public List<FileLinkDataBase> Files { get; set; }
        public List<PictureLinkDataBase> Pictures { get; set; }
        public List<VideoLinkDataBase> Videos { get; set; }

        public DocumentDataBase()
        {
            Files = new List<FileLinkDataBase>();
            Pictures = new List<PictureLinkDataBase>();
            Videos = new List<VideoLinkDataBase>();
        }

        public DocumentDataBase(Guid id, string title, string content, string description, DateTime creationTime, DateTime lastUpdate,
            List<FileLinkDataBase> fileLinkDataBases, List<PictureLinkDataBase> pictureLinkDataBases, List<VideoLinkDataBase> videoLinkDataBases)
        {
            Id = id;
            Title = title;
            Content = content;
            Description = description;
            CreationTime = creationTime;
            LastUpdate = lastUpdate;
            Files = fileLinkDataBases;
            Pictures = pictureLinkDataBases;
            Videos = videoLinkDataBases;
        }

    }
}
