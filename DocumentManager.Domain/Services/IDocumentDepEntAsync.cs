using DocumentManager.Domain;
using System;
using System.Threading.Tasks;
using DocumentManager.Domain.Model;

namespace DocumentManager.Domain.Services
{
    public interface IDocumentDepEntAsync
    {
        public Task Change(Document document);
        public Task<Document> GetByIdFiles(Guid idDoc);
        public Task<Document> GetByIdPictures(Guid idDoc);
        public Task<Document> GetByIdVideos(Guid idDoc);
    }
}
