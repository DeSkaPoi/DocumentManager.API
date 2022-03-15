using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DocumentManager.Domain;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManager.Infrastructure.InterfaceRepository
{
    public interface IDocumentRepository
    {
        public Task AddAsync(Document document);
        public Task UpdateAsync(Document document);
        public Task DeleteAsync(Guid idDoc);
        public Task<IReadOnlyList<Document>> GetAllAsync();
        public Task<Document> GetByIdAsync(Guid idDoc);
    }
}
