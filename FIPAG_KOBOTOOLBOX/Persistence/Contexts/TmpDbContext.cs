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

        public virtual DbSet<Cl2> Cl2 { get; set; } = null!;

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

            modelBuilder.Entity<Cl2>(entity =>
            {
                entity.HasKey(e => e.Cl2stamp)
                    .HasName("pk_cl2")
                    .IsClustered(false);

                entity.ToTable("cl2");

                entity.Property(e => e.Cl2stamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("cl2stamp")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.Adcsepaativa).HasColumnName("adcsepaativa");

                entity.Property(e => e.Cadmintipo1)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cadmintipo1")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Cadmintipo1stamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("cadmintipo1stamp")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.Cadmintipo2)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cadmintipo2")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Cadmintipo2stamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("cadmintipo2stamp")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.Cadmintipo3)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cadmintipo3")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Cadmintipo3stamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("cadmintipo3stamp")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.Cadmintipo4)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cadmintipo4")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Cadmintipo4stamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("cadmintipo4stamp")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.Cativaperc)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("cativaperc");

                entity.Property(e => e.Clivacaixa).HasColumnName("clivacaixa");

                entity.Property(e => e.Cobrecsede).HasColumnName("cobrecsede");

                entity.Property(e => e.Codendereco)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("codendereco")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Codpais)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("codpais")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Dbpm).HasColumnName("dbpm");

                entity.Property(e => e.Descpais)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("descpais")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Dgrelhas).HasColumnName("dgrelhas");

                entity.Property(e => e.Dlogin)
                    .HasColumnType("datetime")
                    .HasColumnName("dlogin")
                    .HasDefaultValueSql("(CONVERT([datetime],'19000101',0))");

                entity.Property(e => e.Doceletronicos).HasColumnName("doceletronicos");

                entity.Property(e => e.Egarapa)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("egarapa")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Egargrupo)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("egargrupo")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Egaropdes)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("egaropdes")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Egaropera)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("egaropera")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Egarpgl)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("egarpgl")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Egarprod)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("egarprod")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Estabmod)
                    .HasColumnType("numeric(3, 0)")
                    .HasColumnName("estabmod");

                entity.Property(e => e.Forgotdate)
                    .HasColumnType("datetime")
                    .HasColumnName("forgotdate")
                    .HasDefaultValueSql("(CONVERT([datetime],'19000101',0))");

                entity.Property(e => e.Forgotid)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("forgotid")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Hlogin)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("hlogin")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Isb2b).HasColumnName("isb2b");

                entity.Property(e => e.Latitude)
                    .HasColumnType("numeric(10, 6)")
                    .HasColumnName("latitude");

                entity.Property(e => e.Longitude)
                    .HasColumnType("numeric(10, 6)")
                    .HasColumnName("longitude");

                entity.Property(e => e.Marcada).HasColumnName("marcada");

                entity.Property(e => e.Monitignios).HasColumnName("monitignios");

                entity.Property(e => e.Nifvalidado).HasColumnName("nifvalidado");

                entity.Property(e => e.Nomemod)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("nomemod")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Nomod)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("nomod");

                entity.Property(e => e.Ousrdata)
                    .HasColumnType("datetime")
                    .HasColumnName("ousrdata")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Ousrhora)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("ousrhora")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Ousrinis)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("ousrinis")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Passpais)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("passpais")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Passpaisdesc)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("passpaisdesc")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Pwportal)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("pwportal")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Refmbndias)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("refmbndias");

                entity.Property(e => e.Refmbtpdata)
                    .HasColumnType("numeric(1, 0)")
                    .HasColumnName("refmbtpdata");

                entity.Property(e => e.Refmbusavalidade).HasColumnName("refmbusavalidade");

                entity.Property(e => e.Retarrat)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("retarrat")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Tdocidcod)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("tdocidcod")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Tdocidenif).HasColumnName("tdocidenif");

                entity.Property(e => e.Termsconditions).HasColumnName("termsconditions");

                entity.Property(e => e.Trackfuelpos)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("trackfuelpos")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UAlugact).HasColumnName("u_alugact");

                entity.Property(e => e.UAndar)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("u_andar")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UAnterior)
                    .HasColumnType("numeric(16, 0)")
                    .HasColumnName("u_anterior");

                entity.Property(e => e.UCalramal)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("u_calramal")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UClientid)
                    .HasColumnType("text")
                    .HasColumnName("u_clientid")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UClprepg).HasColumnName("u_clprepg");

                entity.Property(e => e.UConsmin)
                    .HasColumnType("numeric(4, 0)")
                    .HasColumnName("u_consmin");

                entity.Property(e => e.UConsuman)
                    .HasColumnType("numeric(16, 2)")
                    .HasColumnName("u_consuman");

                entity.Property(e => e.UDesvio)
                    .HasColumnType("numeric(16, 0)")
                    .HasColumnName("u_desvio");

                entity.Property(e => e.UDiametro)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("u_diametro")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UDimramal)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("u_dimramal")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UDist)
                    .HasColumnType("numeric(8, 2)")
                    .HasColumnName("u_dist");

                entity.Property(e => e.UDistrit)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("u_distrit")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UDtactgps)
                    .HasColumnType("datetime")
                    .HasColumnName("u_dtactgps")
                    .HasDefaultValueSql("(CONVERT([datetime],'19000101'))");

                entity.Property(e => e.UDtprepg)
                    .HasColumnType("datetime")
                    .HasColumnName("u_dtprepg")
                    .HasDefaultValueSql("(CONVERT([datetime],'19000101'))");

                entity.Property(e => e.UDtultact)
                    .HasColumnType("datetime")
                    .HasColumnName("u_dtultact")
                    .HasDefaultValueSql("(CONVERT([datetime],'19000101'))");

                entity.Property(e => e.UEmpreit).HasColumnName("u_empreit");

                entity.Property(e => e.UEndereco)
                    .HasMaxLength(254)
                    .IsUnicode(false)
                    .HasColumnName("u_endereco")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UEndsede).HasColumnName("u_endsede");

                entity.Property(e => e.UEstabon).HasColumnName("u_estabon");

                entity.Property(e => e.UGpsx)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("u_gpsx")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UGpsy)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("u_gpsy")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UInicio)
                    .HasColumnType("datetime")
                    .HasColumnName("u_inicio")
                    .HasDefaultValueSql("(CONVERT([datetime],'19000101'))");

                entity.Property(e => e.UIniciof)
                    .HasColumnType("datetime")
                    .HasColumnName("u_iniciof")
                    .HasDefaultValueSql("(CONVERT([datetime],'19000101'))");

                entity.Property(e => e.ULacre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("u_lacre")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UMarcco)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("u_marcco")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UMcubico)
                    .HasColumnType("numeric(6, 0)")
                    .HasColumnName("u_mcubico");

                entity.Property(e => e.UMcubicob)
                    .HasColumnType("numeric(6, 0)")
                    .HasColumnName("u_mcubicob");

                entity.Property(e => e.UMemo)
                    .HasColumnType("text")
                    .HasColumnName("u_memo")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UMmedia)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("u_mmedia");

                entity.Property(e => e.UModeco)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("u_modeco")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UNacordo)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("u_nacordo")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UNcapita)
                    .HasColumnType("numeric(3, 0)")
                    .HasColumnName("u_ncapita");

                entity.Property(e => e.UNcasa)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("u_ncasa")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UNcontado)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("u_ncontado")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UNquart)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("u_nquart")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UNrua)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("u_nrua")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UNtalhao)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("u_ntalhao")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UOrdem)
                    .HasColumnType("numeric(12, 0)")
                    .HasColumnName("u_ordem");

                entity.Property(e => e.UOtpapp)
                    .HasColumnType("numeric(16, 0)")
                    .HasColumnName("u_otpapp");

                entity.Property(e => e.UPercdesv)
                    .HasColumnType("numeric(6, 2)")
                    .HasColumnName("u_percdesv");

                entity.Property(e => e.UPorta)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("u_porta")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UPref)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("u_pref")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UProcdesc)
                    .HasColumnType("text")
                    .HasColumnName("u_procdesc")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UProcesso)
                    .HasColumnType("numeric(16, 0)")
                    .HasColumnName("u_processo");

                entity.Property(e => e.UProvince)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("u_province")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.URefcon)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("u_refcon")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.URefpag)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("u_refpag")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.URua)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("u_rua")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UTemesc0).HasColumnName("u_temesc0");

                entity.Property(e => e.UTemtxdsp).HasColumnName("u_temtxdsp");

                entity.Property(e => e.UTemtxsan).HasColumnName("u_temtxsan");

                entity.Property(e => e.UTermino)
                    .HasColumnType("datetime")
                    .HasColumnName("u_termino")
                    .HasDefaultValueSql("(CONVERT([datetime],'19000101'))");

                entity.Property(e => e.Ubigeo)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("ubigeo")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Userid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("userid")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Usrdata)
                    .HasColumnType("datetime")
                    .HasColumnName("usrdata")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Usrhora)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("usrhora")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Usrinis)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("usrinis")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Validadecartao)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("validadecartao")
                    .HasDefaultValueSql("('')");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
