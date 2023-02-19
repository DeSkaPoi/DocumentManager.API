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
using System.Collections.Generic;
using DocumentManager.API.Converts;

namespace DocumentManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentFilesController : ControllerBase
    {
        private readonly IDocumentDepEntAsync _repository;

        public DocumentFilesController(IDocumentDepEntAsync repository)
        {
            this._repository = repository;
        }

        [HttpGet("{idDoc}")]
        public async Task<ActionResult<DocumentResponse>> GetFilesDocument(Guid idDoc)
        {
            try
            {
                var docFiles = await _repository.GetByIdFiles(idDoc);
                var docResponse = docFiles.ConvertToResponse();
                return docResponse;
            }
            catch (Exception ex)
            {
                return StatusCode(404, ex.Message);
            }
        }

        [HttpPost("{idDoc:Guid}/{idFile:Guid}")]
        public async Task<ActionResult<DocumentResponse>> PostFileDocument(Guid idDoc, Guid idFile)
        {
            try
            {
                await _repository.Add(idDoc, idFile, "FileLink");
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
            try
            {
                await _repository.Remove(idDoc, idFileLink);
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
