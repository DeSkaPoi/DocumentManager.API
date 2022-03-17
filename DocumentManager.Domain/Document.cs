using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManager.Domain
{
    public class Document
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Content { get; private set; } 
        public string Description { get; private set; }
        public DateTime CreationTime { get; private set; }
        public DateTime LastUpdate { get; private set; }

        public List<FileLink> Files { get; private set; } 
        public List<PictureLink> Pictures { get; private set; } 
        public List<VideoLink> Videos { get; private set; } 

        public Document()
        {
            Files = new List<FileLink>();
            Pictures = new List<PictureLink>();
            Videos = new List<VideoLink>();
        }

    }
}
