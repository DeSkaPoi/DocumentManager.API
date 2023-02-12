using System;

namespace DocumentManager.Domain.Model
{
    public class FileLink
    {
        public Guid Id { get; }
        public Guid FileId { get; }

        public FileLink(Guid id, Guid fileId)
        {
            Id = id;
            FileId = fileId;
        }
    }
}
