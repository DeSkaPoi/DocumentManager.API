using DocumentManager.Domain.Model;
using DocumentManager.Infrastructure.ModelDB;
using System.Collections.Generic;
using DocumentManager.API.ModelResponse;
using System.Linq;

namespace DocumentManager.API.Converts
{
    public static class ConverterToResponse
    {
        public static DocumentResponse ConvertToResponse(this Document document)
        {
            return new DocumentResponse(document.Id, document.Title, document.Content, document.Description, document.CreationTime, document.LastUpdate,
                document.Files.Select(f => f.ConvertToResponse()).ToList(), document.Pictures.Select(f => f.ConvertToResponse()).ToList(), document.Videos.Select(f => f.ConvertToResponse()).ToList());
        }
        public static IReadOnlyList<DocumentResponse> ConvertToResponse(this IReadOnlyList<Document> document)
        {
            return document.Select(d => d.ConvertToResponse()).ToList();
        }

        public static PictureLinkResponse ConvertToResponse(this PictureLink picture)
        {
            return new PictureLinkResponse(picture.Id, picture.PictureId);
        }

        public static FileLinkResponse ConvertToResponse(this FileLink file)
        {
            return new FileLinkResponse(file.Id, file.FileId);
        }
        public static VideoLinkResponse ConvertToResponse(this VideoLink video)
        {
            return new VideoLinkResponse(video.Id, video.VideoId);
        }
    }
}
