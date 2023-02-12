using System;

namespace DocumentManager.Infrastructure.ModelDB
{
    public class FileLinkDataBase
    {
        public Guid Id { get; set; }
        public Guid FileId { get; set; }
        public DocumentDataBase Document { get; set; }

        public FileLinkDataBase() { }
        public FileLinkDataBase(Guid id, Guid fileId)
        {
            Id = id;
            FileId = fileId;
        }
    }
}
