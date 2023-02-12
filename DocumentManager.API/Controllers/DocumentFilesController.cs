using DocumentManager.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using DocumentManager.API.ErrorResponses;
using DocumentManager.Infrastructure.ModelDB;
using DocumentManager.Infrastructure.RepositoryDB;
using DocumentManager.Domain.Model;
using DocumentManager.Domain.Services;
using DocumentManager.API.ModelResponse;

namespace DocumentManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentFilesController : ControllerBase
    {
        private readonly IDocumentDepEntAsync _repository;

        public DocumentFilesController(IDocumentDepEntAsync repository)
        {
            this._repository = repository;
        }

        [HttpGet("{idDoc}")]
        public async Task<ActionResult<Document>> GetFilesDocument(Guid idDoc)
        {
            try
            {
                return await _repository.GetByIdFiles(idDoc);
            }
            catch (Exception ex)
            {
                return StatusCode(404, ex.Message);
            }
        }

        [HttpPost("{idDoc:Guid}/{idFile:Guid}")]
        public async Task<ActionResult<Document>> PostFileDocument(Guid idDoc, Guid idFile)
        {
            var document = await _repository.GetByIdFiles(idDoc);
            if (document == null)
            {
                return StatusCode(400);
            }

            try
            {
                var fileLink = new FileLink(new Guid(), idFile);
                document.Files.Add(fileLink);
                await _repository.Change(document);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                var errorResponse = new ErrorResponse(ex.Message);
                return StatusCode(404, errorResponse);
            }
        }

        [HttpDelete("{idDoc}/{idFileLink}")]
        public async Task<IActionResult> DeleteFileDocument(Guid idDoc, Guid idFileLink)
        {
            var document = await _repository.GetByIdFiles(idDoc);
            if (document == null)
            {
                return StatusCode(400);
            }

            try
            {
                var delFile = document.Files.FirstOrDefault(f => f.FileId == idFileLink);
                document.Files.Remove(delFile);
                await _repository.Change(document);
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
