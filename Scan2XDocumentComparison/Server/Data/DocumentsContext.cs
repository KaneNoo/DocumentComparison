using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Scan2XDocumentComparison.Server.Data
{
    public partial class DocumentsContext : DbContext
    {
        public DocumentsContext()
        {
        }

        public DocumentsContext(DbContextOptions<DocumentsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DocumentInDB> DocumentsInDB { get; set; } = null!;
        public virtual DbSet<ScannedDocument> ScannedDocuments { get; set; } = null!;
        public virtual DbSet<ComparisonResult> ComparisonResults { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DocumentInDB>(entity =>
            {
                entity.HasKey(e => new { e.DaxRecid, e.DocNum });

                entity.ToTable("S2X_DOC_LIST");

                entity.HasIndex(e => new { e.DaxRecid, e.DocNum }, "IX_S2X_DOC_LIST")
                    .IsUnique();

                entity.Property(e => e.Customer)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("CUSTOMER");

                entity.Property(e => e.CustomerInn).HasColumnName("CUSTOMER_INN");

                entity.Property(e => e.CustomerKpp).HasColumnName("CUSTOMER_KPP");

                entity.Property(e => e.DaxRecid).HasColumnName("DAX_RECID");

                entity.Property(e => e.DocDate)
                    .HasColumnType("datetime")
                    .HasColumnName("DOC_DATE");

                entity.Property(e => e.DocNum)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("DOC_NUM");

                entity.Property(e => e.DocType)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("DOC_TYPE");

                entity.Property(e => e.SumAll).HasColumnName("SUM_ALL");

                entity.Property(e => e.SumApp).HasColumnName("SUM_APP");

                entity.Property(e => e.SumTax).HasColumnName("SUM_TAX");

                entity.Property(e => e.Vendor)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("VENDOR");

                entity.Property(e => e.VendorInn).HasColumnName("VENDOR_INN");

                entity.Property(e => e.VendorKpp).HasColumnName("VENDOR_KPP");

                entity.Property(e => e.WithEdo).HasColumnName("WITH_EDO");

                entity.Property(e => e.WithoutEdo)
                    .HasMaxLength(10)
                    .HasColumnName("WITHOUT_EDO")
                    .IsFixedLength();
            });

            modelBuilder.Entity<ScannedDocument>(entity =>
            {
                entity.HasKey(e => new { e.DaxRecid, e.DocNum });

                entity.ToTable("S2X_DOC_LIST_S2X");

                entity.HasIndex(e => new { e.DaxRecid, e.DocNum }, "IX_S2X_DOC_LIST_S2X")
                    .IsUnique();

                entity.Property(e => e.Customer)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("CUSTOMER");

                entity.Property(e => e.CustomerInn).HasColumnName("CUSTOMER_INN");

                entity.Property(e => e.CustomerKpp).HasColumnName("CUSTOMER_KPP");

                entity.Property(e => e.DaxRecid).HasColumnName("DAX_RECID");

                entity.Property(e => e.DocDate)
                    .HasColumnType("datetime")
                    .HasColumnName("DOC_DATE");

                entity.Property(e => e.DocNum)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("DOC_NUM");

                entity.Property(e => e.DocType)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("DOC_TYPE");

                entity.Property(e => e.SumAll).HasColumnName("SUM_ALL");

                entity.Property(e => e.SumApp).HasColumnName("SUM_APP");

                entity.Property(e => e.SumTax).HasColumnName("SUM_TAX");

                entity.Property(e => e.Vendor)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("VENDOR");

                entity.Property(e => e.VendorInn).HasColumnName("VENDOR_INN");

                entity.Property(e => e.VendorKpp).HasColumnName("VENDOR_KPP");

                entity.Property(e => e.WithEdo).HasColumnName("WITH_EDO");

                entity.Property(e => e.WithoutEdo)
                    .HasMaxLength(10)
                    .HasColumnName("WITHOUT_EDO")
                    .IsFixedLength();
            });

            modelBuilder.Entity<ComparisonResult>(entity =>
            {
                entity.HasKey(e => new { e.DaxRecid, e.DocNum });

                entity.ToTable("S2X_DOC_WEB");

                entity.Property(e => e.Customer).HasColumnName("CUSTOMER");

                entity.Property(e => e.CustomerInn).HasColumnName("CUSTOMER_INN");

                entity.Property(e => e.CustomerKpp).HasColumnName("CUSTOMER_KPP");

                entity.Property(e => e.DaxRecid).HasColumnName("DAX_RECID");

                entity.Property(e => e.DocDate).HasColumnName("DOC_DATE");

                entity.Property(e => e.DocNum)
                    .HasMaxLength(100)
                    .HasColumnName("DOC_NUM");

                entity.Property(e => e.SumAll).HasColumnName("SUM_ALL");

                entity.Property(e => e.SumApp).HasColumnName("SUM_APP");

                entity.Property(e => e.SumTax).HasColumnName("SUM_TAX");

                entity.Property(e => e.Vendor).HasColumnName("VENDOR");

                entity.Property(e => e.VendorInn).HasColumnName("VENDOR_INN");

                entity.Property(e => e.VendorKpp).HasColumnName("VENDOR_KPP");

                entity.Property(e => e.WithEdo).HasColumnName("WITH_EDO");

                entity.Property(e => e.WithoutEdo).HasColumnName("WITHOUT_EDO");

                entity.Property(e => e.DocURL).HasColumnName("DOC_URL");
                entity.Property(e => e.IsChecked).HasColumnName("DOC_CHCK");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
