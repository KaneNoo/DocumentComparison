using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace Scan2XDocumentComparison.Client.Services.DocumentService
{
    public class DocumentService : IDocumentService
    {
        private readonly HttpClient _client;

        public event Action DocumentsChanged;
        public string Message { get; set; } = "Загружаем документы...";
        public int CurrentPage { get; set; } = 1;
        public int DocsOnPageCount { get; set; } = 10;
        public int PageCount { get; set; } = 0;
        public List<DetailedDocument> Documents { get; set; } = new List<DetailedDocument>();

        public DocSearchData LastSearchData { get; set; } = new DocSearchData()
        {
            DocsOnPageCount = 10,
            StartDate = DateTime.Now.Date,
            EndDate = DateTime.Now.Date.AddDays(1).AddTicks(-1)
        };

        public DocumentService(HttpClient client)
        {
            _client = client;
        }


        public async Task<ServiceResponse<DetailedDocument>> GetDetailedDocument(string docNum)
        {
            var result = await _client.GetFromJsonAsync<ServiceResponse<DetailedDocument>>($"api/document/detailed/{docNum}");

            return result;
        }


        public async Task GetDocumentsWithDifferences(DocSearchData searchData, int page)
        {
            LastSearchData = searchData;

            //var result = await _client.GetFromJsonAsync<ServiceResponse<List<DetailedDocument>>>("api/document/");
            var response = await _client.PostAsJsonAsync($"api/document/search/{page}", searchData);

            var result = await response.Content.ReadFromJsonAsync<ServiceResponse<DocSearchResult>>();

            if (result != null && result.Data != null)
            {
                if (result.Data.Documents != null)
                {
                    Documents = result.Data.Documents;
                }
                

                CurrentPage = result.Data.CurrentPage;
                LastSearchData.PageCount = result.Data.PageCount;
                PageCount = result.Data.PageCount;
            }
            else
            {
                Documents = new List<DetailedDocument>();
            }

            if (Documents.Count == 0)
            {
                Message = "Нет документов с различиями";
            }

            DocumentsChanged.Invoke();
        }

        public async Task<ServiceResponse<bool>> SetDocumentVerified(string docNum)
        {
            var result = new ServiceResponse<bool>();

            ////Переделать на Patch
            //var content = new JsonPatchDocument<ComparisonResult>();
            //content.Replace(r => r.IsChecked, 1);
            //var serializedDoc = JsonConvert.SerializeObject(content);
            //var requestContent = new StringContent(serializedDoc, Encoding.UTF8, "application/json-patch+json");


            var response = await _client.PostAsJsonAsync($"api/document/setverified", docNum);

            if (response != null && response.IsSuccessStatusCode)
            {
                result.Data = true;
            }
            else
            {
                result.Success = false;
            }

            return result;
        }

        public async Task<ServiceResponse<byte[]>> GetPdfFile(string docNum)
        {

            var result = new ServiceResponse<byte[]>();

            
            var response = await _client.GetFromJsonAsync<ServiceResponse<byte[]>>($"api/document/getpdf/{docNum}");

            if (response != null && response.Success)
            {
                if (response.Data != null)
                {
                    result.Data = response.Data;
                }
            }
            


            return result;
        }
    }
}
