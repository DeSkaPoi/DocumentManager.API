using System;

namespace DocumentManager.Infrastructure.ModelDB
{
    public class PictureLinkDataBase
    {
        public Guid Id { get; set; }
        public Guid PictureId { get; set; }
        public DocumentDataBase Document { get; set; }

        public PictureLinkDataBase() { }
        public PictureLinkDataBase(Guid id, Guid pictureId)
        {
            Id = id;
            PictureId = pictureId;
        }
    }
}
