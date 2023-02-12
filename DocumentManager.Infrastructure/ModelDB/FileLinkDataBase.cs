using System;

namespace DocumentManager.Infrastructure.ModelDB
{
    public class FileLinkDataBase
    {
        public Guid Id { get; set; }
        public Guid FileId { get; set; }
        public DocumentDataBase Document { get; set; }

        public FileLinkDataBase(Guid fileId)
        {
            FileId = fileId;
        }
    }
}
