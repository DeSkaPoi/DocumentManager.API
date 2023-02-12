using System;

namespace DocumentManager.API.ModelResponse
{
    public class FileLink
    {
        public Guid Id { get; set; }
        public Guid FileId { get; set; }

        public FileLink(Guid fileId)
        {
            FileId = fileId;
        }
    }
}
