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
    public class DocumentPicturesController : ControllerBase
    {
        private readonly IDocumentDependentEntities repository;

        public DocumentPicturesController(DocManagerContext context)
        {
            repository = new DocumentRepository(context);
        }

        [HttpGet("{idDoc}")]
        public async Task<Document> GetPicturesDocument(Guid idDoc)
        {
            return await repository.GetByIdPicturesAsync(idDoc);
        }

        [HttpPost("{idDoc:Guid}/{idPicture:Guid}")]
        public async Task<ActionResult<Document>> PostPictureDocument(Guid idDoc, Guid idPicture)
        {
            var document = await repository.GetByIdPicturesAsync(idDoc);
            if (document == null)
            {
                return BadRequest("dsssd");
            }
            PictureLink pictureLink = new PictureLink(idPicture);
            document.Pictures.Add(pictureLink);
            await repository.UpdateAsync(document);
            return CreatedAtAction("PostPictureDocument", new { id = pictureLink.Id }, pictureLink);
        }

        [HttpDelete("{idDoc}/{idPictureLink}")]
        public async Task<IActionResult> DeletePictureDocument(Guid idDoc, Guid idPictureLink)
        {
            var document = await repository.GetByIdPicturesAsync(idDoc);
            var delPicture = document.Pictures.Where(f => f.Id == idPictureLink).FirstOrDefault();
            document.Pictures.Remove(delPicture);
            await repository.UpdateAsync(document);
            return NoContent();
        }
    }
}
