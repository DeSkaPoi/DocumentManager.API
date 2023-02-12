using DocumentManager.Domain;
using DocumentManager.Infrastructure.ModelDB;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DocumentManager.Infrastructure.RepositoryDB
{
    public interface IDocumentRepository
    {
        public Task AddAsync(DocumentDataBase document);
        public Task UpdateAsync(DocumentDataBase document);
        public Task DeleteAsync(Guid idDoc);
        public Task<IReadOnlyList<DocumentDataBase>> GetAllAsync();
        public Task<DocumentDataBase> GetByIdAsync(Guid idDoc);
    }
}
