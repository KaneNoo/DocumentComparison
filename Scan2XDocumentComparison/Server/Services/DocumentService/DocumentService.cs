namespace Scan2XDocumentComparison.Server.Services.DocumentService
{
    public class DocumentService : IDocumentService
    {
        private readonly DocumentsContext _context;

        public DocumentService(DocumentsContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<ComparisonResult>> GetComparisonResults(string docNum)
        {
            var response = new ServiceResponse<ComparisonResult>()
            {
                Data = await _context.ComparisonResults
                .SingleOrDefaultAsync(r =>
                docNum.Equals(docNum) &&
                (r.IsChecked != 1) &&
                (
                r.DocDate == 0 ||
                r.Vendor == 0 ||
                r.VendorInn == 0 ||
                r.VendorKpp == 0 ||
                r.Customer == 0 ||
                r.CustomerInn == 0 ||
                r.CustomerKpp == 0 ||
                r.WithEdo == 0 ||
                r.WithoutEdo == 0 ||
                r.SumAll == 0 ||
                r.SumTax == 0 ||
                r.SumApp == 0
                ))
            };

            return response;
        }

        public async Task<ServiceResponse<DetailedDocument>> GetDetailedDocument(string docNum)
        {
            var response = new ServiceResponse<DetailedDocument>();

            var docDB = await _context.DocumentsInDB
                .Where(d => d.DocNum.Equals(docNum))
                .FirstOrDefaultAsync();

            var scannedDoc = await _context.ScannedDocuments
                .Where(d => d.DocNum.Equals(docNum))
                .FirstOrDefaultAsync();

            var compResult = await _context.ComparisonResults
                .Where(d => d.DocNum.Equals(docNum))
                .FirstOrDefaultAsync();

            if (docDB == null || scannedDoc == null || compResult == null)
            {
                response.Success = false;
                response.Message = "Документ не существует";
            }
            else
            {
                response.Data = new DetailedDocument()
                {
                    ScannedDoc = scannedDoc,
                    DocInDB = docDB,
                    CompResult = compResult
                };
            }

            return response;
        }

        public async Task<ServiceResponse<DocSearchResult>> GetDocumentsWithDifferences(DocSearchData searchData, int page)
        {

            var response = new ServiceResponse<DocSearchResult>();

            var queue = _context.DocumentsInDB
                .Where(d => d.DocDate >= searchData.StartDate && d.DocDate <= searchData.EndDate)
                .Join(_context.ComparisonResults
                .Where(r =>
                     r.IsChecked != 1 &&
                     (
                     r.DocDate == 0 ||
                     r.Vendor == 0 ||
                     r.VendorInn == 0 ||
                     r.VendorKpp == 0 ||
                     r.Customer == 0 ||
                     r.CustomerInn == 0 ||
                     r.CustomerKpp == 0 ||
                     r.WithEdo == 0 ||
                     r.WithoutEdo == 0 ||
                     r.SumAll == 0 ||
                     r.SumTax == 0 ||
                     r.SumApp == 0)
                     ),
                d => d.DocNum,
                r => r.DocNum,
                (d, r) => new DetailedDocument
                {
                    DocInDB = new DocumentInDB
                    {
                        DocType = d.DocType,
                        DocDate = d.DocDate,
                        DocNum = d.DocNum,
                        Vendor = d.Vendor,
                        VendorInn = d.VendorInn,
                        VendorKpp = d.VendorKpp,
                        Customer = d.Customer,
                        CustomerInn = d.CustomerInn,
                        CustomerKpp = d.CustomerKpp,
                        WithEdo = d.WithEdo,
                        WithoutEdo = d.WithoutEdo,
                        SumTax = d.SumTax,
                        SumAll = d.SumAll,
                        SumApp = d.SumApp,
                        DaxRecid = d.DaxRecid
                    },

                    CompResult = new ComparisonResult
                    {
                        DocDate = r.DocDate,
                        DocNum = r.DocNum,
                        Vendor = r.Vendor,
                        VendorInn = r.VendorInn,
                        VendorKpp = r.VendorKpp,
                        Customer = r.Customer,
                        CustomerInn = r.CustomerInn,
                        CustomerKpp = r.CustomerKpp,
                        WithEdo = r.WithEdo,
                        WithoutEdo = r.WithoutEdo,
                        SumTax = r.SumTax,
                        SumAll = r.SumAll,
                        SumApp = r.SumApp,
                        DaxRecid = r.DaxRecid
                    }
                });

            int count = queue.Count();

            if (count > 0)
            {
                List<DetailedDocument> detailedDocuments = await queue
                 .Skip((page - 1) * searchData.DocsOnPageCount)
                 .Take(searchData.DocsOnPageCount)
                 .ToListAsync();

                double pageCount = Math.Ceiling((double)count / (double)searchData.DocsOnPageCount);

                response.Data = new DocSearchResult
                {
                    PageCount = (int)pageCount,
                    DocsOnPageCount = (int)searchData.DocsOnPageCount,
                    CurrentPage = page,
                    Documents = detailedDocuments
                };
            }
            else
            {
                response.Success = false;
                response.Message = "Документов с несовпадениями нет";
            }
            return response;
        }
        public async Task<ServiceResponse<bool>> SetDocumentVerified(string docNum)
        {
            var result = new ServiceResponse<bool>();
            var documentResultComparison = await _context.ComparisonResults.SingleOrDefaultAsync(r => r.DocNum.Equals(docNum));

            if (documentResultComparison != null)
            {
                try
                {
                    documentResultComparison.IsChecked = 1;
                    _context.ComparisonResults.Attach(documentResultComparison);
                    _context.Entry(documentResultComparison).State = EntityState.Modified;

                    await _context.SaveChangesAsync();

                    result.Data = true;
                }
                catch
                {
                    result.Success = false;
                }
            }
            else
            {
                result.Success = false;
            }

            return result;
        }

        public async Task<ServiceResponse<byte[]>> GetPDFContent(string docNum)
        {
            var result = new ServiceResponse<byte[]>();
            byte[] pdfContent;

            var docURL = _context.ComparisonResults.SingleOrDefault(d => d.DocNum == docNum).DocURL;

            if (docURL != null)
            {
                pdfContent = await File.ReadAllBytesAsync(docURL);

                result.Data = pdfContent;
            }
            else
            {
                result.Success = false;
                result.Message = "Не указан путь до PDF-файла";
            }

            return result;
        }
    }


}

