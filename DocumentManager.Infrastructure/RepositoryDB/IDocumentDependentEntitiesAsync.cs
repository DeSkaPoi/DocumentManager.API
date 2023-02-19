using DocumentManager.Infrastructure.ModelDB;
using System;
using System.Threading.Tasks;

namespace DocumentManager.Infrastructure.RepositoryDB
{
    public interface IDocumentDependentEntitiesAsync
    {
        public Task Update(DocumentDataBase document);
        public Task<DocumentDataBase> GetById(Guid idDoc);
        public Task<DocumentDataBase> GetByIdAsNoTracking(Guid idDoc);
        public Task<DocumentDataBase> GetByIdFiles(Guid idDoc);
        public Task<DocumentDataBase> GetByIdPictures(Guid idDoc);
        public Task<DocumentDataBase> GetByIdVideos(Guid idDoc);
    }
}
