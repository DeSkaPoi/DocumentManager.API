using DocumentManager.Domain;
using DocumentManager.Infrastructure.ModelDB;
using System;
using System.Threading.Tasks;

namespace DocumentManager.Infrastructure.RepositoryDB
{
    public interface IDocumentDependentEntities
    {
        public Task UpdateAsync(DocumentDataBase document);
        public Task<DocumentDataBase> GetByIdFilesAsync(Guid idDoc);
        public Task<DocumentDataBase> GetByIdPicturesAsync(Guid idDoc);
        public Task<DocumentDataBase> GetByIdVideosAsync(Guid idDoc);
    }
}
