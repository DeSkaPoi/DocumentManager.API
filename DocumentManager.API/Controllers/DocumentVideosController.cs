using DocumentManager.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using DocumentManager.Infrastructure.ModelDB;
using DocumentManager.Infrastructure.RepositoryDB;
using DocumentManager.Domain.Model;
using DocumentManager.Domain.Services;
using DocumentManager.API.ModelResponse;
using DocumentManager.Domain.Converters;
using DocumentManager.API.Converts;

namespace DocumentManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentVideosController : ControllerBase
    {
        private readonly IDocumentDepEntAsync _repository;

        public DocumentVideosController(IDocumentDepEntAsync repository)
        {
            this._repository = repository;
        }

        [HttpGet("{idDoc}")]
        public async Task<ActionResult<DocumentResponse>> GetVideosDocument(Guid idDoc)
        {
            try
            {
                var docFiles = await _repository.GetByIdVideos(idDoc);
                var docResponse = docFiles.ConvertToResponse();
                return docResponse;
            }
            catch (Exception ex)
            {
                return StatusCode(404, ex.Message);
            }
        }

        [HttpPost("{idDoc:Guid}/{idVideo:Guid}")]
        public async Task<ActionResult<DocumentResponse>> PostVideoDocument(Guid idDoc, Guid idVideo)
        {
            try
            {
                await _repository.Add(idDoc, idVideo, "VideoLink");
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                var errorResponse = new ErrorResponse(ex.Message);
                return StatusCode(500, errorResponse);
            }
        }

        [HttpDelete("{idDoc}/{idVideoLink}")]
        public async Task<IActionResult> DeleteVideoDocument(Guid idDoc, Guid idVideoLink)
        {
            try
            {
                await _repository.Remove(idDoc, idVideoLink);
                return StatusCode(204);
            }
            catch (Exception ex)
            {
                var errorResponse = new ErrorResponse(ex.Message);
                return StatusCode(500, errorResponse);
            }
        }
    }
}
