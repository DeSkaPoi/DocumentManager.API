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
    public class DocumentVideosController : ControllerBase
    {
        private readonly IDocumentDependentEntities _repository;

        public DocumentVideosController(IDocumentDependentEntities repository)
        {
            this._repository = repository;
        }

        [HttpGet("{idDoc}")]
        public async Task<ActionResult<Document>> GetVideosDocument(Guid idDoc)
        {
            try
            {
                return await _repository.GetByIdVideosAsync(idDoc);
            }
            catch (Exception ex)
            {
                return StatusCode(404, ex.Message);
            }
        }

        [HttpPost("{idDoc:Guid}/{idVideo:Guid}")]
        public async Task<ActionResult<Document>> PostVideoDocument(Guid idDoc, Guid idVideo)
        {
            var document = await _repository.GetByIdVideosAsync(idDoc);
            if (document == null)
            {
                return StatusCode(400);
            }

            try
            {
                var videoLink = new VideoLink(idVideo);
                document.Videos.Add(videoLink);
                await _repository.UpdateAsync(document);
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
            var document = await _repository.GetByIdVideosAsync(idDoc);
            if (document == null)
            {
                return StatusCode(400);
            }

            try
            {
                var delVideo = document.Videos.FirstOrDefault(f => f.Id == idVideoLink);
                document.Videos.Remove(delVideo);
                await _repository.UpdateAsync(document);
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
