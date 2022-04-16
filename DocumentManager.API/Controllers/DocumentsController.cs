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
using DocumentManager.API.ErrorResponses;

namespace DocumentManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentRepository repository;

        public DocumentsController(IDocumentRepository repository)
        {
            this.repository = repository;
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
                var errorResponse = new ErrorResponse(ex.Message);
                return StatusCode(404, errorResponse);
            }
        }

        // GET: api/Documents/5
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<Document>> GetDocument(Guid id)
        {
            Document document = await repository.GetByIdAsync(id);
            if (document == null)
            {
                var errorResponse = new ErrorResponse($"Not found {id}");
                return StatusCode(404, errorResponse);
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
                var errorResponse = new ErrorResponse("Bad request: no match between id and object");
                return StatusCode(400, errorResponse);
            }
            try
            {
                await repository.UpdateAsync(document);
            }
            catch (Exception ex)
            {
                var errorResponse = new ErrorResponse(ex.Message);
                return StatusCode(500, errorResponse);
            }
            return StatusCode(204);
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
                var errorResponse = new ErrorResponse(ex.Message);
                return StatusCode(500, errorResponse);
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
                var errorResponse = new ErrorResponse(ex.Message);
                return StatusCode(404, errorResponse);
            }
            return StatusCode(204);
        }
    }
}
