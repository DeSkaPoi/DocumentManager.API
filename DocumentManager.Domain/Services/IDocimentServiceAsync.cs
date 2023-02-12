using DocumentManager.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManager.Domain.Services
{
    public interface IDocimentServiceAsync
    {
        public Task Add(Document document);
        public Task Change(Document document);
        public Task Delete(Guid idDoc);
        public Task<IReadOnlyList<Document>> GetAll();
        public Task<Document> GetById(Guid idDoc);
    }
}
