using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Billing.Models
{
    public partial class billContext : DbContext
    {
        public billContext()
        {
        }

        public billContext(DbContextOptions<billContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Gst> Gst { get; set; }
        public virtual DbSet<Products> Products { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Data Source= KADEEJATHLUBAIB\\SQLEXPRESS; Initial Catalog= bill; Integrated security=True");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Cid)
                    .HasName("PK__category__D837D05FB6AA51E4");

                entity.ToTable("category");

                entity.Property(e => e.Cid)
                    .HasColumnName("cid")
                    .ValueGeneratedNever();

                entity.Property(e => e.Category1)
                    .HasColumnName("category")
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Gst>(entity =>
            {
                entity.ToTable("gst");

                entity.Property(e => e.GstId)
                    .HasColumnName("gstId")
                    .ValueGeneratedNever();

                entity.Property(e => e.Cid).HasColumnName("cid");

                entity.Property(e => e.Gstvalue).HasColumnName("gstvalue");

                entity.HasOne(d => d.C)
                    .WithMany(p => p.Gst)
                    .HasForeignKey(d => d.Cid)
                    .HasConstraintName("FK__gst__cid__3D5E1FD2");
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasKey(e => e.ProductCode)
                    .HasName("PK__products__C20683889508458B");

                entity.ToTable("products");

                entity.Property(e => e.ProductCode)
                    .HasColumnName("productCode")
                    .ValueGeneratedNever();

                entity.Property(e => e.Cid).HasColumnName("cid");

                entity.Property(e => e.DescProduct)
                    .HasColumnName("descProduct")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Qty).HasColumnName("qty");

                entity.Property(e => e.RatePerUnit).HasColumnName("ratePerUnit");

                entity.HasOne(d => d.C)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.Cid)
                    .HasConstraintName("FK__products__cid__3B75D760");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
