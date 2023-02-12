using System;

namespace DocumentManager.Domain.Model
{
    public class PictureLink
    {
        public Guid Id { get; }
        public Guid PictureId { get; }
        public PictureLink(Guid id, Guid pictureId)
        {
            Id = id;
            PictureId = pictureId;
        }
    }
}
