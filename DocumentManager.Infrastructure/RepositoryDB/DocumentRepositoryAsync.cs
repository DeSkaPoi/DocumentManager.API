using DocumentManager.Infrastructure.ContextDB;
using DocumentManager.Infrastructure.ModelDB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace DocumentManager.Infrastructure.RepositoryDB
{
    public class DocumentRepositoryAsync : IDocumentRepositoryAsync, IDocumentDependentEntitiesAsync
    {
        private readonly DocManagerContext _context;
        private readonly object _locker = new object();
        public DocumentRepositoryAsync(DocManagerContext context)
        {
            _context = context;
        }

        public async Task Add(DocumentDataBase document)
        {
            var tmp = await _context.Documents.FindAsync(document.Id);
            if (tmp != null)
                throw new Exception("this object exists");
            await _context.AddAsync(document);
            await _context.SaveChangesAsync();
        }
        public async Task Update(DocumentDataBase document)
        {
            _context.Update(document);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid idDoc)
        {
            var tmp = await _context.Documents.FindAsync(idDoc);
            if (tmp == null)
                throw new Exception("it is impossible to delete what is not");
            _context.Remove(tmp);
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<DocumentDataBase>> GetAll()
        {
            return await _context.Documents.ToListAsync();
        }

        public async Task<DocumentDataBase> GetById(Guid idDoc)
        {
            var document = await _context.Documents.Where(docProp => docProp.Id == idDoc)
                .Include(d => d.Files)
                .Include(d => d.Pictures)
                .Include(d => d.Videos)
                .FirstOrDefaultAsync();
            return document;
        }

        public async Task<DocumentDataBase> GetByIdAsNoTracking(Guid idDoc)
        {
            var document = await _context.Documents.AsNoTracking().Where(docProp => docProp.Id == idDoc)
                .Include(d => d.Files)
                .Include(d => d.Pictures)
                .Include(d => d.Videos)
                .FirstOrDefaultAsync();
            return document;
        }

        public async Task<DocumentDataBase> GetByIdFiles(Guid idDoc)
        {
            var document = await _context.Documents.Where(docProp => docProp.Id == idDoc)
                .Include(d => d.Files)
                .FirstOrDefaultAsync();
            return document;
        }

        public async Task<DocumentDataBase> GetByIdPictures(Guid idDoc)
        {
            var document = await _context.Documents.Where(docProp => docProp.Id == idDoc)
                .Include(d => d.Pictures)
                .FirstOrDefaultAsync();
            return document;
        }

        public async Task<DocumentDataBase> GetByIdVideos(Guid idDoc)
        {
            var document = await _context.Documents.Where(docProp => docProp.Id == idDoc)
                .Include(d => d.Videos)
                .FirstOrDefaultAsync();
            return document;
        }
    }
}
