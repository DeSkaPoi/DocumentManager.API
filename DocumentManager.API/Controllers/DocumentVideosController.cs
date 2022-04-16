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
    public class DocumentVideosController : ControllerBase
    {
        private readonly IDocumentDependentEntities repository;

        public DocumentVideosController(IDocumentDependentEntities repository)
        {
            this.repository = repository;
        }

        [HttpGet("{idDoc}")]
        public async Task<ActionResult<Document>> GetVideosDocument(Guid idDoc)
        {
            try
            {
                return await repository.GetByIdVideosAsync(idDoc);
            }
            catch (Exception ex)
            {
                return StatusCode(404 ,ex.Message);
            }
        }

        [HttpPost("{idDoc:Guid}/{idVideo:Guid}")]
        public async Task<ActionResult<Document>> PostVideoDocument(Guid idDoc, Guid idVideo)
        {
            var document = await repository.GetByIdVideosAsync(idDoc);
            if (document == null)
            {
                return StatusCode(400);
            }
            VideoLink videoLink = new VideoLink(idVideo);
            document.Videos.Add(videoLink);
            await repository.UpdateAsync(document);
            return StatusCode(201);
        }

        [HttpDelete("{idDoc}/{idVideoLink}")]
        public async Task<IActionResult> DeleteVideoDocument(Guid idDoc, Guid idVideoLink)
        {
            var document = await repository.GetByIdVideosAsync(idDoc);
            var delVideo = document.Videos.Where(f => f.Id == idVideoLink).FirstOrDefault();
            document.Videos.Remove(delVideo);
            await repository.UpdateAsync(document);
            return StatusCode(204);
        }
    }
}
