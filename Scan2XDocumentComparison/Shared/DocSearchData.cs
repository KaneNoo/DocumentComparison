using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan2XDocumentComparison.Shared
{
    public class DocSearchData
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PageCount { get; set; }
        public int DocsOnPageCount { get; set; }
    }
}
