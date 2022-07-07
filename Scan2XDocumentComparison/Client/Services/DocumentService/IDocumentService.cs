namespace Scan2XDocumentComparison.Client.Services.DocumentService
{
    public interface IDocumentService
    {
        event Action DocumentsChanged;
        string Message { get;set; }
        int CurrentPage { get; set; }
        int PageCount { get; set; }
        int DocsOnPageCount { get; set; }
        public DocSearchData LastSearchData { get; set; }
        List<DetailedDocument> Documents { get; set; }
        Task GetDocumentsWithDifferences(DocSearchData searchData, int page);
        Task<ServiceResponse<DetailedDocument>> GetDetailedDocument(string docNum);
        Task<ServiceResponse<bool>> SetDocumentVerified(string docNum);
        Task<ServiceResponse<byte[]>> GetPdfFile(string docNum);
    }
}
