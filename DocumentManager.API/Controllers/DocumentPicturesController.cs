using DocumentManager.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using DocumentManager.Infrastructure.ModelDB;
using DocumentManager.Domain.Model;
using DocumentManager.Domain.Services;
using DocumentManager.API.ModelResponse;
using DocumentManager.Domain.Converters;
using DocumentManager.API.Converts;

namespace DocumentManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentPicturesController : ControllerBase
    {
        private readonly IDocumentDepEntAsync service;

        public DocumentPicturesController(IDocumentDepEntAsync service)
        {
            this.service = service;
        }

        [HttpGet("{idDoc}")]
        public async Task<ActionResult<DocumentResponse>> GetPicturesDocument(Guid idDoc)
        {
            try
            {
                var docFiles = await service.GetByIdPictures(idDoc);
                var docResponse = docFiles.ConvertToResponse();
                return docResponse;
            }
            catch (Exception ex)
            {
                return StatusCode(404, ex.Message);
            }
        }

        [HttpPost("{idDoc:Guid}/{idPicture:Guid}")]
        public async Task<ActionResult<DocumentResponse>> PostPictureDocument(Guid idDoc, Guid idPicture)
        {
            try
            {
                await service.Add(idDoc, idPicture, "PictureLink");
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                var errorResponse = new ErrorResponse(ex.Message);
                return StatusCode(500, errorResponse);
            }
        }

        [HttpDelete("{idDoc}/{idPictureLink}")]
        public async Task<IActionResult> DeletePictureDocument(Guid idDoc, Guid idPictureLink)
        {
            try
            {
                await service.Remove(idDoc, idPictureLink);
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
