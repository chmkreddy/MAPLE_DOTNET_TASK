using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MAPLE_INSURANCE_API.Models
{
    public partial class MAPLE_TESTContext : DbContext
    {
        public MAPLE_TESTContext()
        {
        }

        public MAPLE_TESTContext(DbContextOptions<MAPLE_TESTContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Contracts> Contracts { get; set; }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contracts>(entity =>
            {
                entity.HasKey(e => e.ContractId)
                    .HasName("PK__CONTRACT__C90D3469F7945092");

                entity.ToTable("CONTRACTS");

                entity.Property(e => e.CoveragePlan)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerAddress)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerCountry)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.CustomerDob)
                    .HasColumnName("CustomerDOB")
                    .HasColumnType("datetime");

                entity.Property(e => e.CustomerGender)
                    .HasMaxLength(1)
                    .IsFixedLength();

                entity.Property(e => e.CustomerName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SaleDate).HasColumnType("datetime");
            });

           OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
