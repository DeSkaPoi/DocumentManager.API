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

        public DocumentVideosController(DocManagerContext context)
        {
            repository = new DocumentRepository(context);
        }

        [HttpGet("{idDoc}")]
        public async Task<Document> GetVideosDocument(Guid idDoc)
        {
            return await repository.GetByIdVideosAsync(idDoc);
        }

        [HttpPost("{idDoc:Guid}/{idVideo:Guid}")]
        public async Task<ActionResult<Document>> PostVideoDocument(Guid idDoc, Guid idVideo)
        {
            var document = await repository.GetByIdVideosAsync(idDoc);
            if (document == null)
            {
                return BadRequest();
            }
            VideoLink videoLink = new VideoLink(idVideo);
            document.Videos.Add(videoLink);
            await repository.UpdateAsync(document);
            return CreatedAtAction("PostPictureDocument", new { id = videoLink.Id }, videoLink);
        }

        [HttpDelete("{idDoc}/{idVideoLink}")]
        public async Task<IActionResult> DeleteVideoDocument(Guid idDoc, Guid idVideoLink)
        {
            var document = await repository.GetByIdVideosAsync(idDoc);
            var delVideo = document.Videos.Where(f => f.Id == idVideoLink).FirstOrDefault();
            document.Videos.Remove(delVideo);
            await repository.UpdateAsync(document);
            return NoContent();
        }
    }
}
