using DocumentManager.API.ModelResponse;
using DocumentManager.Domain.Model;
using System.Collections.Generic;
using System.Linq;

namespace DocumentManager.API.Converts
{
    public static class ConverterToModel
    {
        public static Document ConvertsToModel(this DocumentResponse document)
        {
            return new Document(document.Id, document.Title, document.Content, document.Description, document.CreationTime, document.LastUpdate,
                document.Files.Select(f => f.ConvertsToModel()).ToList(), document.Pictures.Select(f => f.ConvertsToModel()).ToList(), document.Videos.Select(f => f.ConvertsToModel()).ToList());
        }
        public static IReadOnlyList<Document> ConvertsToModel(this IReadOnlyList<DocumentResponse> document)
        {
            return document.Select(d => d.ConvertsToModel()).ToList();
        }

        public static PictureLink ConvertsToModel(this PictureLinkResponse picture)
        {
            return new PictureLink(picture.Id, picture.PictureId);
        }

        public static FileLink ConvertsToModel(this FileLinkResponse file)
        {
            return new FileLink(file.Id, file.FileId);
        }
        public static VideoLink ConvertsToModel(this VideoLinkResponse video)
        {
            return new VideoLink(video.Id, video.VideoId);
        }
    }
}
