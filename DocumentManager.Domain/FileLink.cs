using System;

namespace DocumentManager.Domain
{
    public class FileLink
    {
        public Guid Id { get; set; }
        public Guid FileId { get; set; }
        public Document Document { get; set; }

        public FileLink(Guid fileId)
        {
            FileId = fileId;
        }
    }
}
