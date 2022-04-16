using DocumentManager.Domain;
using DocumentManager.Infrastructure.InterfaceRepository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentPicturesController : ControllerBase
    {
        private readonly IDocumentDependentEntities _repository;

        public DocumentPicturesController(IDocumentDependentEntities repository)
        {
            this._repository = repository;
        }

        [HttpGet("{idDoc}")]
        public async Task<ActionResult<Document>> GetPicturesDocument(Guid idDoc)
        {
            try
            {
                return await _repository.GetByIdPicturesAsync(idDoc);
            }
            catch (Exception ex)
            {
                return StatusCode(404, ex.Message);
            }
        }

        [HttpPost("{idDoc:Guid}/{idPicture:Guid}")]
        public async Task<ActionResult<Document>> PostPictureDocument(Guid idDoc, Guid idPicture)
        {
            var document = await _repository.GetByIdPicturesAsync(idDoc);
            if (document == null)
            {
                return StatusCode(400);
            }
            PictureLink pictureLink = new PictureLink(idPicture);
            document.Pictures.Add(pictureLink);
            await _repository.UpdateAsync(document);
            return StatusCode(201);
        }

        [HttpDelete("{idDoc}/{idPictureLink}")]
        public async Task<IActionResult> DeletePictureDocument(Guid idDoc, Guid idPictureLink)
        {
            var document = await _repository.GetByIdPicturesAsync(idDoc);
            var delPicture = document.Pictures.FirstOrDefault(f => f.Id == idPictureLink);
            document.Pictures.Remove(delPicture);
            await _repository.UpdateAsync(document);
            return StatusCode(204);
        }
    }
}
