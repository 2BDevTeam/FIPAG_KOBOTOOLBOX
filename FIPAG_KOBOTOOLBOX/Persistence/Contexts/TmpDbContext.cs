using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using FIPAG_KOBOTOOLBOX.Domains.Models;

namespace FIPAG_KOBOTOOLBOX.Persistence.Contexts
{
    public partial class TmpDbContext : DbContext
    {
        public TmpDbContext()
        {
        }

        public TmpDbContext(DbContextOptions<TmpDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Obaclientes> Obaclientes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=SRV05\\SQLDEV2019;Database=OnBD_FIPAG;User Id=isac.munguambe;password=Murd3rB4nd;MultipleActiveResultSets=true;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Obaclientes>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("OBAClientes");

                entity.Property(e => e.Error)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("error")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.KoboId).HasColumnName("koboId");

                entity.Property(e => e.KoboNome)
                    .HasMaxLength(255)
                    .HasColumnName("koboNome");

                entity.Property(e => e.PhcId).HasColumnName("phcId");

                entity.Property(e => e.PhcNome)
                    .HasMaxLength(255)
                    .HasColumnName("phcNome");

                entity.Property(e => e.Sync).HasColumnName("sync");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
