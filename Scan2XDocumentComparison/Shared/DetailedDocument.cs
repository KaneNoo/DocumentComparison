using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan2XDocumentComparison.Shared
{
    public class DetailedDocument
    {
        public DocumentInDB DocInDB { get; set; } = null!;
        public ScannedDocument ScannedDoc { get; set; } = null!;
        public ComparisonResult CompResult { get; set; } = null!;
    }
}
