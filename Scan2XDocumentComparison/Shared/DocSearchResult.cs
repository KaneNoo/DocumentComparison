using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan2XDocumentComparison.Shared
{
    public class DocSearchResult
    {
        public List<DetailedDocument> Documents { get; set; } = new List<DetailedDocument>();
        public int PageCount { get; set; }
        public int CurrentPage { get; set; }
        public int DocsOnPageCount { get; set; }
    }
}
