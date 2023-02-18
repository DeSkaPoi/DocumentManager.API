using DocumentManager.Domain.Model;
using DocumentManager.Infrastructure.ModelDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManager.Domain.Converters
{
    public static class ConverterToModel
    {
        public static Document Converts(this DocumentDataBase document)
        {
            return new Document(document.Id, document.Title, document.Content, document.Description, document.CreationTime, document.LastUpdate,
                document.Files.Select(f => f.Converts()).ToList(), document.Pictures.Select(f => f.Converts()).ToList(), document.Videos.Select(f => f.Converts()).ToList());
        }
        public static IReadOnlyList<Document> Converts(this IReadOnlyList<DocumentDataBase> document)
        {
            return document.Select(d => d.Converts()).ToList();
        }

        public static PictureLink Converts(this PictureLinkDataBase picture)
        {
            return new PictureLink(picture.Id, picture.PictureId);
        }

        public static FileLink Converts(this FileLinkDataBase file)
        {
            return new FileLink(file.Id, file.FileId);
        }
        public static VideoLink Converts(this VideoLinkDataBase video)
        {
            return new VideoLink(video.Id, video.VideoId);
        }
    }
}
