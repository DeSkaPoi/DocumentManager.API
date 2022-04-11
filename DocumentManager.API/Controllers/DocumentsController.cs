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
        public async Task<ActionResult<IReadOnlyList<Document>>> GetDocuments() 
        {
            try
            {
                var documents = await repository.GetAllAsync();
                var action = new ActionResult<IReadOnlyList<Document>>(documents);
                return action; 
            }
            catch (Exception ex)
            {
                return StatusCode(404, ex.Message);
            }
        }

        // GET: api/Documents/5
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<Document>> GetDocument(Guid id)
        {
            var document = await repository.GetByIdAsync(id);
            if (document == null)
            {
                return StatusCode(404, $"Not found {id}");
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
                return StatusCode(400);
            }
            try
            {
                await repository.UpdateAsync(document);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return StatusCode(204, document);
        }

        // POST: api/Documents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Document>> PostDocument(Document document)
        {
            try
            {
                await repository.AddAsync(document);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return StatusCode(201, document);
        }

        // DELETE: api/Documents/5
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteDocument(Guid id)
        {
            try 
            {
                await repository.DeleteAsync(id); 
            }
            catch (Exception ex)
            {
                return StatusCode(404, ex.Message);
            }
            return StatusCode(204);
        }
    }
}
