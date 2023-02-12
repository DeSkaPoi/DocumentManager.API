using DocumentManager.API.ModelResponse;
using DocumentManager.Domain.Model;
using DocumentManager.Domain.Services;
using DocumentManager.Infrastructure.ModelDB;
using DocumentManager.Infrastructure.RepositoryDB;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DocumentManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocimentServiceAsync _repository;

        public DocumentsController(IDocimentServiceAsync repository)
        {
            _repository = repository;
        }

        // GET: api/Documents
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<DocumentResponse>>> GetDocuments()
        {
            try
            {
                var documents = await _repository.GetAll();
                var action = new ActionResult<IReadOnlyList<DocumentResponse>>(documents);
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
        public async Task<ActionResult<DocumentResponse>> GetDocument(Guid id)
        {
            var document = await _repository.GetById(id);
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
        public async Task<IActionResult> PutDocument(Guid? id, DocumentResponse document)
        {
            if (id != document.Id)
            {
                var errorResponse = new ErrorResponse("Bad request: no match between id and object");
                return StatusCode(400, errorResponse);
            }
            try
            {
                await _repository.Change(document);
                return StatusCode(204);
            }
            catch (Exception ex)
            {
                var errorResponse = new ErrorResponse(ex.Message);
                return StatusCode(500, errorResponse);
            }
        }

        // POST: api/Documents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DocumentResponse>> PostDocument(DocumentResponse document)
        {
            try
            {
                await _repository.Add(document);
                return StatusCode(201, document);
            }
            catch (Exception ex)
            {
                var errorResponse = new ErrorResponse(ex.Message);
                return StatusCode(500, errorResponse);
            }
        }

        // DELETE: api/Documents/5
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteDocument(Guid id)
        {
            try
            {
                await _repository.Delete(id);
                return StatusCode(204);
            }
            catch (Exception ex)
            {
                var errorResponse = new ErrorResponse(ex.Message);
                return StatusCode(404, errorResponse);
            }
        }
    }
}
