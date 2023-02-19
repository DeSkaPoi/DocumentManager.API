using DocumentManager.Domain.Converters;
using DocumentManager.Domain.Model;
using DocumentManager.Infrastructure.RepositoryDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManager.Domain.Services
{
    public class DocumentDepEntService : IDocumentDepEntAsync
    {
        private readonly IDocumentDependentEntitiesAsync _docDepEnt;
        public DocumentDepEntService(IDocumentDependentEntitiesAsync docDepEnt)
        {
            _docDepEnt = docDepEnt;
        }

        public async Task Add(Guid idDoc, Guid idDepEnt, string typeDepEnt)
        {
            var documentDb = await _docDepEnt.GetByIdAsNoTracking(idDoc);
            var document = documentDb.Converts();
            if (document == null)
            {
                throw new ArgumentNullException(idDoc.ToString());  
            }

            if (typeDepEnt == "FileLink")
            {
                var filesLink = new FileLink(new Guid(), idDepEnt);
                document.Files.Add(filesLink);
            }
            else if (typeDepEnt == "PictureLink")
            {
                var pictireLink = new PictureLink(new Guid(), idDepEnt);
                document.Pictures.Add(pictireLink);
            }
            else if(typeDepEnt == "VideoLink")
            {
                var videoLink = new VideoLink(new Guid(), idDepEnt);
                document.Videos.Add(videoLink);
            }
            else
            {
                throw new ArgumentNullException("no data");
            }
            await _docDepEnt.Update(document.ConvertsToDataBase());
        }

        public async Task Remove(Guid idDoc, Guid idDepEnt)
        {
            var document = await _docDepEnt.GetById(idDoc);
            //var document = documentDb.Converts();
            if (document == null)
            {
                throw new ArgumentNullException(idDoc.ToString());
            }

            var file = document.Files.FirstOrDefault(f => f.FileId == idDepEnt);
            if (file != default)
            {
                document.Files.Remove(file);
            }

            var picture = document.Pictures.FirstOrDefault(f => f.PictureId== idDepEnt);
            if (picture != default)
            {
                document.Pictures.Remove(picture);
            }

            var video = document.Videos.FirstOrDefault(f => f.VideoId == idDepEnt);
            if (video != default)
            {
                document.Videos.Remove(video);
            }
            await _docDepEnt.Update(document);
        }

        public async Task<Document> GetByIdFiles(Guid idDoc)
        {
           var docDB = await _docDepEnt.GetByIdFiles(idDoc);
           return docDB.Converts();
        }

        public async Task<Document> GetByIdPictures(Guid idDoc)
        {
            var docDB = await _docDepEnt.GetByIdPictures(idDoc);
            return docDB.Converts();
        }

        public async Task<Document> GetByIdVideos(Guid idDoc)
        {
            var docDB = await _docDepEnt.GetByIdVideos(idDoc);
            return docDB.Converts();
        }
    }
}
