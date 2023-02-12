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
    public class DocumentService : IDocumentServiceAsync
    {
        private IDocumentRepositoryAsync _repository;
        public DocumentService(IDocumentRepositoryAsync documentRepository) 
        {
            _repository = documentRepository;
        }
        public async Task Add(Document document)
        {
            var docDB = document.ConvertsToDataBase();
            await _repository.Add(docDB);
        }

        public async Task Change(Document document)
        {
            var docDB = document.ConvertsToDataBase();
            await _repository.Update(docDB);
        }

        public async Task Delete(Guid idDoc)
        {
            await _repository.Delete(idDoc);
        }

        public async Task<IReadOnlyList<Document>> GetAll()
        {
            var docDBCollection = await _repository.GetAll();
            return docDBCollection.Converts();
        }

        public async Task<Document> GetById(Guid idDoc)
        {
            var docDB = await _repository.GetById(idDoc);
            return docDB.Converts();
        }
    }
}
