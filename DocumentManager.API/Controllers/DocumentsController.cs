using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DocumentManager.Domain;
using DocumentManager.Infrastructure;
using DocumentManager.Infrastructure.InterfaceRepository;

namespace DocumentManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentRepository repository;

        public DocumentsController(DocManagerContext context)
        {
            repository = new DocumentRepository(context);
        }

        // GET: api/Documents
        [HttpGet]
        public async Task<IReadOnlyList<Document>> GetDocuments() 
        {
            return await repository.GetAllAsync();
        }

        // GET: api/Documents/5
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<Document>> GetDocument(Guid id)
        {
            var document = await repository.GetByIdAsync(id);

            if (document == null)
            {
                return NotFound();
            }

            return document;
        }

        // PUT: https://localhost:5001/Documents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> PutDocument(Guid? id, Document document)
        {
            if (id != document.Id)
            {
                return BadRequest();
            }
            await repository.UpdateAsync(document);
            return NoContent();
        }

        // POST: api/Documents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Document>> PostDocument(Document document)
        {
            await repository.AddAsync(document);
            return CreatedAtAction("GetDocument", new { id = document.Id }, document);
        }

        // DELETE: api/Documents/5
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteDocument(Guid id)
        {
            try 
            {
                await repository.DeleteAsync(id); 
            }
            catch (Exception)
            {
                return NotFound(id);
            }
            return NoContent();
        }

        // DELETE: api/Documents/
        [HttpDelete]
        public async Task<IActionResult> DeleteRangeDocument(List<Guid> ids) 
        {
            List<Guid> NoneDeleteGuid = new List<Guid>();
            int i = 0;
            try
            {
                for (; i < ids.Count; i++)
                {
                    await repository.DeleteAsync(ids[i]);
                }
            }
            catch (Exception)
            {
                NoneDeleteGuid.Add(ids[i]);
            }
            
            if (NoneDeleteGuid.Count != 0)
            {
                return NotFound(NoneDeleteGuid);
            }
            return NoContent();
        }
    }
}
