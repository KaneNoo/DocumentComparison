using Scan2XDocumentComparison.Shared;

namespace Scan2XDocumentComparison.Server.Services.DocumentService
{
    public interface IDocumentService
    {

        Task<ServiceResponse<ComparisonResult>> GetComparisonResults(string docNum);
        Task<ServiceResponse<DocSearchResult>> GetDocumentsWithDifferences(DocSearchData searchData, int page);
        Task<ServiceResponse<DetailedDocument>> GetDetailedDocument(string docNum);
        Task<ServiceResponse<bool>> SetDocumentVerified(string docNum);
        Task<ServiceResponse<byte[]>> GetPDFContent(string docNum);

    }
}
