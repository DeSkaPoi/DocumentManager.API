using System;

namespace DocumentManager.API.ModelResponse
{
    public class PictureLinkResponse
    {
        public Guid Id { get; set; }
        public Guid PictureId { get; set; }
        public PictureLinkResponse(Guid id, Guid pictureId)
        {
            Id = id;
            PictureId = pictureId;
        }
    }
}
