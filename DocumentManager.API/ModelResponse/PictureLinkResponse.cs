﻿using System;

namespace DocumentManager.Domain.Model
{
    public class PictureLink
    {
        public Guid Id { get; set; }
        public Guid PictureId { get; set; }
        public Document Document { get; set; }
        public PictureLink(Guid pictureId)
        {
            PictureId = pictureId;
        }
    }
}
