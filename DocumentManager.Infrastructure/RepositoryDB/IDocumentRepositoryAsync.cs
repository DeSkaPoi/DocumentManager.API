using DocumentManager.Infrastructure.ModelDB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DocumentManager.Infrastructure.RepositoryDB
{
    public interface IDocumentRepositoryAsync
    {
        public Task Add(DocumentDataBase document);
        public Task Update(DocumentDataBase document);
        public Task Delete(Guid idDoc);
        public Task<IReadOnlyList<DocumentDataBase>> GetAll();
        public Task<DocumentDataBase> GetById(Guid idDoc);
        public void ChangeTrackerClear();
    }
}
