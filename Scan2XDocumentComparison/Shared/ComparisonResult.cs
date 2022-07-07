using System;
using System.Collections.Generic;

namespace Scan2XDocumentComparison.Shared
{
    public partial class ComparisonResult
    {
        public string DocNum { get; set; } = null!;
        public int? DocDate { get; set; }
        public int? Vendor { get; set; }
        public int? VendorInn { get; set; }
        public int? VendorKpp { get; set; }
        public int? Customer { get; set; }
        public int? CustomerInn { get; set; }
        public int? CustomerKpp { get; set; }
        public int? WithEdo { get; set; }
        public int? WithoutEdo { get; set; }
        public int? SumAll { get; set; }
        public int? SumTax { get; set; }
        public int? SumApp { get; set; }
        public long DaxRecid { get; set; }
        public string? DocURL { get; set; }
        public int? IsChecked { get; set; }

    }
}
