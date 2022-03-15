using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DocumentManager.Domain;
using DocumentManager.Infrastructure.InterfaceRepository;

namespace DocumentManager.Infrastructure
{
    public class DocumentRepository : IDocumentRepository, IDocumentDependentEntities
    {
        private readonly DocManagerContext context; 
        public DocumentRepository(DocManagerContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(Document document)
        {
            Document tmp = await context.Documents.FindAsync(document.Id);
            if (tmp != null)
                return;
            await context.AddAsync(document);
            await context.SaveChangesAsync(); 
        }
        public async Task UpdateAsync(Document document)
        {
            context.Update(document);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid idDoc)
        {
            Document tmp = await context.Documents.FindAsync(idDoc);
            if (tmp == null)
                throw new Exception();
            context.Remove(tmp);
            await context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<Document>> GetAllAsync() 
        {
            return await context.Documents.ToListAsync();
        }

        public async Task<Document> GetByIdAsync(Guid idDoc) 
        {
            var document = await context.Documents.Where(docProp => docProp.Id == idDoc)
                .Include(d => d.Files)
                .Include(d => d.Pictures)
                .Include(d => d.Videos)
                .FirstOrDefaultAsync();
            return document;
        }

        public async Task<Document> GetByIdFilesAsync(Guid idDoc)
        {
            var document = await context.Documents.Where(docProp => docProp.Id == idDoc)
                .Include(d => d.Files)
                .FirstOrDefaultAsync();
            return document;
        }

        public async Task<Document> GetByIdPicturesAsync(Guid idDoc)
        {
            var document = await context.Documents.Where(docProp => docProp.Id == idDoc)
                .Include(d => d.Pictures)
                .FirstOrDefaultAsync();
            return document;
        }

        public async Task<Document> GetByIdVideosAsync(Guid idDoc)
        {
            var document = await context.Documents.Where(docProp => docProp.Id == idDoc)
                .Include(d => d.Videos)
                .FirstOrDefaultAsync();
            return document;
        }
    }
}
