using System;
using System.Collections.Generic;

namespace Scan2XDocumentComparison.Shared
{
    public partial class DocumentInDB
    {
        public string? DocType { get; set; }
        public DateTime? DocDate { get; set; }
        public string DocNum { get; set; } = null!;
        public string? Vendor { get; set; }
        public long? VendorInn { get; set; }
        public long? VendorKpp { get; set; }
        public string? Customer { get; set; }
        public long? CustomerInn { get; set; }
        public long? CustomerKpp { get; set; }
        public int? WithEdo { get; set; }
        public string? WithoutEdo { get; set; }
        public long? SumAll { get; set; }
        public long? SumTax { get; set; }
        public long? SumApp { get; set; }
        public long DaxRecid { get; set; }
    }
}
