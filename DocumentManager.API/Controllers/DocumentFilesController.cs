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
    public class DocumentFilesController : ControllerBase
    {
        private readonly IDocumentDependentEntities repository;

        public DocumentFilesController(IDocumentDependentEntities repository)
        {
            this.repository = repository;
        }

        [HttpGet("{idDoc}")]
        public async Task<ActionResult<Document>> GetFilesDocument(Guid idDoc)
        {
            try
            {
                return await repository.GetByIdFilesAsync(idDoc);
            }
            catch (Exception ex)
            {
                return StatusCode(404, ex.Message);
            }
        }

        [HttpPost("{idDoc:Guid}/{idFile:Guid}")]
        public async Task<ActionResult<Document>> PostFileDocument(Guid idDoc, Guid idFile)
        {
            var document = await repository.GetByIdFilesAsync(idDoc);
            if (document == null)
            {
                return StatusCode(400);
            }
            FileLink fileLink = new FileLink(idFile);
            document.Files.Add(fileLink);
            await repository.UpdateAsync(document);
            return StatusCode(201);
        }

        [HttpDelete("{idDoc}/{idFileLink}")]
        public async Task<IActionResult> DeleteFileDocument(Guid idDoc, Guid idFileLink)
        {
            var document = await repository.GetByIdFilesAsync(idDoc);
            var delFile = document.Files.Where(f => f.Id == idFileLink).FirstOrDefault();
            document.Files.Remove(delFile);
            await repository.UpdateAsync(document);
            return StatusCode(204);
        }
    }
}
