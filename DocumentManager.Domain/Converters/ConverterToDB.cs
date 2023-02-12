using DocumentManager.Domain.Model;
using DocumentManager.Infrastructure.ModelDB;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManager.Domain.Converters
{
    public static class ConverterToDB
    {
        public static DocumentDataBase Converts(this Document document)
        {
            return new DocumentDataBase(document.Id, document.Content, document.Title, document.Description, document.CreationTime, document.LastUpdate,
                document.Files.Select(f => f.Converts()).ToList(), document.Pictures.Select(f => f.Converts()).ToList(), document.Videos.Select(f => f.Converts()).ToList());
        }

        public static IReadOnlyList<DocumentDataBase> Converts(this IReadOnlyList<Document> document)
        {
            return document.Select(d => d.Converts()).ToList();
        }

        public static PictureLinkDataBase Converts(this PictureLink picture)
        {
            return new PictureLinkDataBase(picture.Id, picture.PictureId);
        }

        public static FileLinkDataBase Converts(this FileLink file)
        {
            return new FileLinkDataBase(file.Id, file.FileId);
        }
        public static VideoLinkDataBase Converts(this VideoLink video)
        {
            return new VideoLinkDataBase(video.Id, video.VideoId);
        }

    }
}
