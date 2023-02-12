using System;

namespace DocumentManager.API.ModelResponse
{
    public class FileLinkResponse
    {
        public Guid Id { get; set; }
        public Guid FileId { get; set; }

        public FileLinkResponse(Guid id, Guid fileId)
        {
            Id = id;
            FileId = fileId;
        }
    }
}
