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
    public class DocumentDepEntService : IDocumentDepEntAsync
    {
        private readonly IDocumentDependentEntitiesAsync _docDepEnt;
        public DocumentDepEntService(IDocumentDependentEntitiesAsync docDepEnt)
        {
            _docDepEnt = docDepEnt;
        }

        public async Task Change(Document document)
        {
            await _docDepEnt.Update(document.ConvertsToDataBase());
        }

        public async Task<Document> GetByIdFiles(Guid idDoc)
        {
           var docDB = await _docDepEnt.GetByIdFiles(idDoc);
           return docDB.Converts();
        }

        public async Task<Document> GetByIdPictures(Guid idDoc)
        {
            var docDB = await _docDepEnt.GetByIdPictures(idDoc);
            return docDB.Converts();
        }

        public async Task<Document> GetByIdVideos(Guid idDoc)
        {
            var docDB = await _docDepEnt.GetByIdVideos(idDoc);
            return docDB.Converts();
        }
    }
}
