using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Scan2XDocumentComparison.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;


        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpPost("search/{page}")]
        public async Task<ActionResult<ServiceResponse<DocSearchData>>> GetDocumentsWithDifferences(DocSearchData searchData, int page = 1)
        {
            var response = await _documentService.GetDocumentsWithDifferences(searchData, page);

            return Ok(response);
        }

        [HttpGet("detailed/{docNum}")]
        public async Task<ActionResult<ServiceResponse<DetailedDocument>>> GetDetailedDocument(string docNum)
        {
            var response = await _documentService.GetDetailedDocument(docNum);
            return Ok(response);
        }

        [HttpGet("getpdf/{docNum}")]
        public async Task<ActionResult<ServiceResponse<byte[]>>> GetPdfFile(string docNum)
        {
            var response = await _documentService.GetPDFContent(docNum);

            return Ok(response);
        }

        [HttpPost("setverified")]
        public async Task<ActionResult<ServiceResponse<bool>>> SetDocumentVerified([FromBody] string docNum)
        {
            //var comparisonObject = new ComparisonResult();
            //content.ApplyTo(comparisonObject);

            var response = await _documentService.SetDocumentVerified(docNum);

            return Ok(response);
        }
    }

}
