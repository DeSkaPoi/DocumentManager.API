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

        public DocumentFilesController(DocManagerContext context)
        {
            repository = new DocumentRepository(context);
        }

        [HttpGet("{idDoc}")]
        public async Task<Document> GetFilesDocument(Guid idDoc)
        {
            return await repository.GetByIdFilesAsync(idDoc);
        }

        [HttpPost("{idDoc:Guid}/{idFile:Guid}")]
        public async Task<ActionResult<Document>> PostFileDocument(Guid idDoc, Guid idFile)
        {
            var document = await repository.GetByIdFilesAsync(idDoc);
            if (document == null)
            {
                return BadRequest();
            }
            FileLink fileLink = new FileLink(idFile);
            document.Files.Add(fileLink);
            await repository.UpdateAsync(document);
            return CreatedAtAction("PostFileDocument", new { id = fileLink.Id }, fileLink);
        }

        [HttpDelete("{idDoc}/{idFileLink}")]
        public async Task<IActionResult> DeleteFileDocument(Guid idDoc, Guid idFileLink)
        {
            var document = await repository.GetByIdFilesAsync(idDoc);
            var delFile = document.Files.Where(f => f.Id == idFileLink).FirstOrDefault();
            document.Files.Remove(delFile);
            await repository.UpdateAsync(document);
            return NoContent();
        }
    }
}
