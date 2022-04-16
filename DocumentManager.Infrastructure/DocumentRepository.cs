using DocumentManager.Domain;
using DocumentManager.Infrastructure.InterfaceRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace DocumentManager.Infrastructure
{
    public class DocumentRepository : IDocumentRepository, IDocumentDependentEntities
    {
        private readonly DocManagerContext _context;
        public DocumentRepository(DocManagerContext context)
        {
            this._context = context;
        }

        public async Task AddAsync(Document document)
        {
            Document tmp = await _context.Documents.FindAsync(document.Id);
            if (tmp != null)
                throw new Exception("this object exists");
            await _context.AddAsync(document);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Document document)
        {
            _context.Update(document);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid idDoc)
        {
            Document tmp = await _context.Documents.FindAsync(idDoc);
            if (tmp == null)
                throw new Exception("it is impossible to delete what is not");
            _context.Remove(tmp);
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<Document>> GetAllAsync()
        {
            return await _context.Documents.ToListAsync();
        }

        public async Task<Document> GetByIdAsync(Guid idDoc)
        {
            var document = await _context.Documents.Where(docProp => docProp.Id == idDoc)
                .Include(d => d.Files)
                .Include(d => d.Pictures)
                .Include(d => d.Videos)
                .FirstOrDefaultAsync();
            return document;
        }

        public async Task<Document> GetByIdFilesAsync(Guid idDoc)
        {
            var document = await _context.Documents.Where(docProp => docProp.Id == idDoc)
                .Include(d => d.Files)
                .FirstOrDefaultAsync();
            return document;
        }

        public async Task<Document> GetByIdPicturesAsync(Guid idDoc)
        {
            var document = await _context.Documents.Where(docProp => docProp.Id == idDoc)
                .Include(d => d.Pictures)
                .FirstOrDefaultAsync();
            return document;
        }

        public async Task<Document> GetByIdVideosAsync(Guid idDoc)
        {
            var document = await _context.Documents.Where(docProp => docProp.Id == idDoc)
                .Include(d => d.Videos)
                .FirstOrDefaultAsync();
            return document;
        }
    }
}
