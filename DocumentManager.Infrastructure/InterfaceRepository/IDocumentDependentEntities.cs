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
    public interface IDocumentDependentEntities
    {
        public Task UpdateAsync(Document document);
        public Task<Document> GetByIdFilesAsync(Guid idDoc);
        public Task<Document> GetByIdPicturesAsync(Guid idDoc);
        public Task<Document> GetByIdVideosAsync(Guid idDoc);
    }
}
