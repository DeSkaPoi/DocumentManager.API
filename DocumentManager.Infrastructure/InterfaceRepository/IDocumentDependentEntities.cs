using DocumentManager.Domain;
using System;
using System.Threading.Tasks;

namespace DocumentManager.Infrastructure.InterfaceRepository
{
    public interface IDocumentDependentEntities
    {
        public Task UpdateAsync(Document document);
        public Task<Document> GetByIdFilesAsync(Guid idDoc);
        public Task<Document> GetByIdPicturesAsync(Guid idDoc);
        public Task<Document> GetByIdVideosAsync(Guid idDoc);
    }
}
