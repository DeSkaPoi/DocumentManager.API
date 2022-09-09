using DocumentManager.Domain;
using DocumentManager.Infrastructure.InterfaceRepository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using DocumentManager.API.ErrorResponses;

namespace DocumentManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentFilesController : ControllerBase
    {
        private readonly IDocumentDependentEntities _repository;

        public DocumentFilesController(IDocumentDependentEntities repository)
        {
            this._repository = repository;
        }

        [HttpGet("{idDoc}")]
        public async Task<ActionResult<Document>> GetFilesDocument(Guid idDoc)
        {
            try
            {
                return await _repository.GetByIdFilesAsync(idDoc);
            }
            catch (Exception ex)
            {
                return StatusCode(404, ex.Message);
            }
        }

        [HttpPost("{idDoc:Guid}/{idFile:Guid}")]
        public async Task<ActionResult<Document>> PostFileDocument(Guid idDoc, Guid idFile)
        {
            var document = await _repository.GetByIdFilesAsync(idDoc);
            if (document == null)
            {
                return StatusCode(400);
            }

            try
            {
                var fileLink = new FileLink(idFile);
                document.Files.Add(fileLink);
                await _repository.UpdateAsync(document);
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
            var document = await _repository.GetByIdFilesAsync(idDoc);
            if (document == null)
            {
                return StatusCode(400);
            }

            try
            {
                var delFile = document.Files.FirstOrDefault(f => f.FileId == idFileLink);
                document.Files.Remove(delFile);
                await _repository.UpdateAsync(document);
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
