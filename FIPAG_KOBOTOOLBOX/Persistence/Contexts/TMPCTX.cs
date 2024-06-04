using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using FIPAG_KOBOTOOLBOX.Domains.Models;

namespace FIPAG_KOBOTOOLBOX.Persistence.Contexts
{
    public partial class TMPCTX : DbContext
    {
        public TMPCTX()
        {
        }

        public TMPCTX(DbContextOptions<TMPCTX> options)
            : base(options)
        {
        }

        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=nacala;Database=E14E105BD_CFM;User Id=saphc;password=Sp3eD!!1989$$00203;Trusted_Connection=False;MultipleActiveResultSets=true;TrustServerCertificate=True;Encrypt=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
            modelBuilder.Entity<UPor001>(entity =>
            {
             
            });
            */
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
