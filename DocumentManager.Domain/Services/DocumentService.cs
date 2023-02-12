using DocumentManager.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManager.Domain.Services
{
    public class DocumentService : IDocimentServiceAsync
    {
        public DocumentService() 
        { 
        }
        public Task Add(Document document)
        {
            throw new NotImplementedException();
        }

        public Task Change(Document document)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid idDoc)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Document>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Document> GetById(Guid idDoc)
        {
            throw new NotImplementedException();
        }
    }
}
