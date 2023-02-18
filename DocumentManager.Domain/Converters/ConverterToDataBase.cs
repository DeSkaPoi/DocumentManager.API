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
    public static class ConverterToDataBase
    {
        public static DocumentDataBase ConvertsToDataBase(this Document document)
        {
            return new DocumentDataBase(document.Id, document.Title, document.Content, document.Description, document.CreationTime, document.LastUpdate,
                document.Files.Select(f => f.ConvertsToDataBase()).ToList(), document.Pictures.Select(f => f.ConvertsToDataBase()).ToList(), document.Videos.Select(f => f.ConvertsToDataBase()).ToList());
        }

        public static IReadOnlyList<DocumentDataBase> ConvertsToDataBase(this IReadOnlyList<Document> document)
        {
            return document.Select(d => d.ConvertsToDataBase()).ToList();
        }

        public static PictureLinkDataBase ConvertsToDataBase(this PictureLink picture)
        {
            return new PictureLinkDataBase(picture.Id, picture.PictureId);
        }

        public static FileLinkDataBase ConvertsToDataBase(this FileLink file)
        {
            return new FileLinkDataBase(file.Id, file.FileId);
        }
        public static VideoLinkDataBase ConvertsToDataBase(this VideoLink video)
        {
            return new VideoLinkDataBase(video.Id, video.VideoId);
        }
    }
}
