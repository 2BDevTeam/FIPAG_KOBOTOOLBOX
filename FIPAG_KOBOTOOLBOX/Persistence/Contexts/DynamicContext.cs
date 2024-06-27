using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using FIPAG_KOBOTOOLBOX.Domains.Models;
using FIPAG_KOBOTOOLBOX.Extensions;

namespace FIPAG_KOBOTOOLBOX.Persistence.Contexts
{
    public partial class DynamicContext : DbContext
    {

        public DynamicContext(DbContextOptions<DynamicContext> options) : base(options)
        {
        }


        public virtual DbSet<Obaclientes> Obaclientes { get; set; } = null!;



        public virtual DbSet<Log> Log { get; set; } = null!;
        public virtual DbSet<ApiLogs> ApiLogs { get; set; } = null!;
        public virtual DbSet<UProvider> UProvider { get; set; } = null!;
        public virtual DbSet<UFormid> UFormid { get; set; } = null!;
        public virtual DbSet<USyncreport> USyncreport { get; set; } = null!;

        public virtual DbSet<Cl> Cl { get; set; } = null!;
        public virtual DbSet<Cl2> Cl2 { get; set; } = null!;
        public virtual DbSet<Em> Em { get; set; } = null!;
        public virtual DbSet<Bo> Bo { get; set; } = null!;
        public virtual DbSet<Bo2> Bo2 { get; set; } = null!;
        public virtual DbSet<Bo3> Bo3 { get; set; } = null!;
        public virtual DbSet<Ft> Ft { get; set; } = null!;
        public virtual DbSet<Ft3> Ft3 { get; set; } = null!;
        public virtual DbSet<Ft2> Ft2 { get; set; } = null!;
        public virtual DbSet<UBasedados> UBasedados { get; set; } = null!;

        public virtual DbSet<Us> Us { get; set; } = null!;
        public virtual DbSet<USyncQueue> USyncqueue { get; set; } = null!;
        public virtual DbSet<ULibasedado> ULibasedado { get; set; } = null!;


        private string _connectionString;
       
        public DynamicContext(string connectionString)
        {
            _connectionString = connectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (Database.IsSqlServer())
            {
                modelBuilder.AddSqlConvertFunction();
            }
            modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<ULibasedado>(entity =>
            {
                entity.HasKey(e => e.ULibasedadostamp)
                    .HasName("pk_u_libasedado")
                    .IsClustered(false);

                entity.ToTable("u_libasedado");

                entity.Property(e => e.ULibasedadostamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("u_libasedadostamp")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.Basedadosstamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("basedadosstamp")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.Formid)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("formid")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Marcada).HasColumnName("marcada");

                entity.Property(e => e.Nome)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nome")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.SubNome)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("SUBNOME")
                    .HasDefaultValueSql("('')");

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
            });


            modelBuilder.Entity<UBasedados>(entity =>
            {
                entity.HasKey(e => e.UBasedadosstamp)
                    .HasName("pk_u_basedados")
                    .IsClustered(false);

                entity.ToTable("u_basedados");

                entity.Property(e => e.UBasedadosstamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("u_basedadosstamp")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.Marcada).HasColumnName("marcada");

                entity.Property(e => e.Nomebd)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nomebd")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Nomesrv)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nomesrv")
                    .HasDefaultValueSql("('')");

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

                entity.Property(e => e.Url)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("url")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("username")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PASSWORD")
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
            });

            modelBuilder.Entity<USyncreport>(entity =>
            {
                entity.HasKey(e => e.USyncreportstamp)
                    .HasName("pk_u_syncreport")
                    .IsClustered(false);

                entity.ToTable("u_syncreport");

                entity.Property(e => e.USyncreportstamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("u_syncreportstamp")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.Koboid)
                    .HasColumnType("numeric(12, 0)")
                    .HasColumnName("koboid");

                entity.Property(e => e.Marcada).HasColumnName("marcada");

                entity.Property(e => e.Obs)
                    .HasColumnType("text")
                    .HasColumnName("obs")
                    .HasDefaultValueSql("('')");

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

                entity.Property(e => e.Status)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("status")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Tabno)
                    .HasColumnType("numeric(12, 0)")
                    .HasColumnName("tabno");

                entity.Property(e => e.Tabstamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("tabstamp")
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
            });


            modelBuilder.Entity<UFormid>(entity =>
            {
                entity.HasKey(e => e.UFormidstamp)
                    .HasName("pk_u_formid")
                    .IsClustered(false);

                entity.ToTable("u_formid");

                entity.Property(e => e.UFormidstamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("u_formidstamp")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.Cidade)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cidade")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Formid)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("formid")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Marcada).HasColumnName("marcada");

                entity.Property(e => e.Nome)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nome")
                    .HasDefaultValueSql("('')");

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
            });


            modelBuilder.Entity<USyncQueue>(entity =>
            {
                entity.HasKey(e => e.USyncqueuestamp)
                    .HasName("pk_u_syncqueue")
                    .IsClustered(false);

                entity.ToTable("u_syncqueue");

                entity.Property(e => e.USyncqueuestamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("u_syncqueuestamp")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.Accao)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("accao")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Marcada).HasColumnName("marcada");

                entity.Property(e => e.Nometabela)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("nometabela")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.campo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("campo")
                .HasDefaultValueSql("('')");

                entity.Property(e => e.valor)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("valor")
                .HasDefaultValueSql("('')");

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

                entity.Property(e => e.Stamptabela)
                    .HasMaxLength(26)
                    .IsUnicode(false)
                    .HasColumnName("stamptabela")
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
            });

            modelBuilder.Entity<Ft2>(entity =>
            {
                entity.HasKey(e => e.Ft2stamp)
                    .HasName("pk_ft2")
                    .IsClustered(false);

                entity.ToTable("ft2");

                entity.Property(e => e.Ft2stamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("ft2stamp")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.Marcada).HasColumnName("marcada");

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

                entity.Property(e => e.UKobosync).HasColumnName("u_kobosync");

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

            });

            modelBuilder.Entity<Obaclientes>(entity =>
            {
                entity.HasKey(e => e.KoboId)
                    .HasName("PK_OBAClientes_KoboId");

                entity.ToTable("OBAClientes");

                entity.Property(e => e.KoboId).HasColumnName("koboId");

                entity.Property(e => e.Error)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("error")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.KoboNome)
                    .HasMaxLength(255)
                    .HasColumnName("koboNome");

                entity.Property(e => e.PhcId).HasColumnName("phcId");

                entity.Property(e => e.PhcNome)
                    .HasMaxLength(255)
                    .HasColumnName("phcNome");

                entity.Property(e => e.Sync).HasColumnName("sync");
            });

            modelBuilder.Entity<Us>(entity =>
            {
                entity.HasKey(e => e.Userno)
                    .HasName("pk_us")
                    .IsClustered(false);

                entity.ToTable("us");

                entity.HasIndex(e => e.Usstamp, "in_us_stamp")
                    .IsUnique()
                    .HasFillFactor(80);

                entity.HasIndex(e => e.Usercode, "in_us_usercode")
                    .IsUnique()
                    .HasFillFactor(80);

                entity.HasIndex(e => e.Username, "in_us_username")
                    .HasFillFactor(80);

                entity.HasIndex(e => e.Userno, "in_us_userno")
                    .HasFillFactor(80);

                entity.Property(e => e.Userno)
                    .HasColumnType("numeric(6, 0)")
                    .HasColumnName("userno");

                entity.Property(e => e.Abrecalfis).HasColumnName("abrecalfis");

                entity.Property(e => e.Abremonrelcred).HasColumnName("abremonrelcred");

                entity.Property(e => e.Actk2fa)
                    .HasMaxLength(254)
                    .IsUnicode(false)
                    .HasColumnName("actk2fa")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Admdcont).HasColumnName("admdcont");

                entity.Property(e => e.Admdpess).HasColumnName("admdpess");

                entity.Property(e => e.Aextpw)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("aextpw")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Aextra).HasColumnName("aextra");

                entity.Property(e => e.Agencalfis).HasColumnName("agencalfis");

                entity.Property(e => e.Alertsweb).HasColumnName("alertsweb");

                entity.Property(e => e.Antf).HasColumnName("antf");

                entity.Property(e => e.Area)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("area")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.Asusername)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("asusername")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Asuserno)
                    .HasColumnType("numeric(6, 0)")
                    .HasColumnName("asuserno");

                entity.Property(e => e.Autposmv).HasColumnName("autposmv");

                entity.Property(e => e.Avstamp)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("avstamp")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Avstimer)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("avstimer")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.Bwscodigo)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("bwscodigo")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Bwspw)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("bwspw")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Centrolog)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("centrolog")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Checkhelpt).HasColumnName("checkhelpt");

                entity.Property(e => e.Clbadm).HasColumnName("clbadm");

                entity.Property(e => e.Clbno)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("clbno");

                entity.Property(e => e.Clbnome)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("clbnome")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.Clbstamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("clbstamp")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Cliadm).HasColumnName("cliadm");

                entity.Property(e => e.Cvenome)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("cvenome")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.Cvestamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("cvestamp")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Dataultpass)
                    .HasColumnType("datetime")
                    .HasColumnName("dataultpass")
                    .HasDefaultValueSql("(CONVERT([datetime],'19000101',0))");

                entity.Property(e => e.Dexp2fa)
                    .HasColumnType("datetime")
                    .HasColumnName("dexp2fa")
                    .HasDefaultValueSql("(CONVERT([datetime],'19000101'))");

                entity.Property(e => e.Dexptk2fa)
                    .HasColumnType("datetime")
                    .HasColumnName("dexptk2fa")
                    .HasDefaultValueSql("(CONVERT([datetime],'19000101'))");

                entity.Property(e => e.Diasani).HasColumnName("diasani");

                entity.Property(e => e.Diascalfis)
                    .HasColumnType("numeric(3, 0)")
                    .HasColumnName("diascalfis");

                entity.Property(e => e.Diascon).HasColumnName("diascon");

                entity.Property(e => e.Dntf)
                    .HasColumnType("datetime")
                    .HasColumnName("dntf")
                    .HasDefaultValueSql("(CONVERT([datetime],'19000101',0))");

                entity.Property(e => e.Dpt)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("dpt")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Drno)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("drno");

                entity.Property(e => e.Drnome)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("drnome")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Drstamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("drstamp")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Dultrs)
                    .HasColumnType("datetime")
                    .HasColumnName("dultrs")
                    .HasDefaultValueSql("(CONVERT([datetime],'19000101',0))");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Emaxposmv)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("emaxposmv");

                entity.Property(e => e.Empregado)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("empregado")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Esa).HasColumnName("esa");

                entity.Property(e => e.Estatuto)
                    .HasColumnType("text")
                    .HasColumnName("estatuto")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Etiquetascodigo)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("etiquetascodigo")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Etiquetasdescricao)
                    .HasColumnType("text")
                    .HasColumnName("etiquetasdescricao")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Exchangepass)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("exchangepass")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Fazfxmanutencao).HasColumnName("fazfxmanutencao");

                entity.Property(e => e.Filtrocts)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("filtrocts")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Filtroctsstamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("filtroctsstamp")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Filtroem)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("filtroem")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Filtroemstamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("filtroemstamp")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Filtromx)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("filtromx")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Filtromxstamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("filtromxstamp")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Filtrotta)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("filtrotta")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Filtrottastamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("filtrottastamp")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Filtrovi)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("filtrovi")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Filtrovistamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("filtrovistamp")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Fntf)
                    .HasColumnType("numeric(2, 0)")
                    .HasColumnName("fntf");

                entity.Property(e => e.Forgotdate)
                    .HasColumnType("datetime")
                    .HasColumnName("forgotdate")
                    .HasDefaultValueSql("(CONVERT([datetime],'19000101'))");

                entity.Property(e => e.Forgotid)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("forgotid")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Gestordenuncias).HasColumnName("gestordenuncias");

                entity.Property(e => e.Grupo)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("grupo")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Hntf)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("hntf")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Homeus)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("homeus")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Hultrs)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("hultrs")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Idioma)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("idioma")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Idiomakey)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("idiomakey")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Imagem)
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("imagem")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Inactivo).HasColumnName("inactivo");

                entity.Property(e => e.Iniciais)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("iniciais")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Inirs).HasColumnName("inirs");

                entity.Property(e => e.Jaidirecto).HasColumnName("jaidirecto");

                entity.Property(e => e.Jaini).HasColumnName("jaini");

                entity.Property(e => e.Loginerrado)
                    .HasColumnType("numeric(6, 0)")
                    .HasColumnName("loginerrado");

                entity.Property(e => e.Marcada).HasColumnName("marcada");

                entity.Property(e => e.Maxposmv)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("maxposmv");

                entity.Property(e => e.Mcdata)
                    .HasColumnType("datetime")
                    .HasColumnName("mcdata")
                    .HasDefaultValueSql("(CONVERT([datetime],'19000101',0))");

                entity.Property(e => e.Mcmes)
                    .HasColumnType("numeric(2, 0)")
                    .HasColumnName("mcmes");

                entity.Property(e => e.Menuesquerda).HasColumnName("menuesquerda");

                entity.Property(e => e.Nivelaprovacao)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("nivelaprovacao")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Notifypw)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("notifypw")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Notifytk)
                    .HasMaxLength(254)
                    .IsUnicode(false)
                    .HasColumnName("notifytk")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Notifyus)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("notifyus")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Ntfma).HasColumnName("ntfma");

                entity.Property(e => e.Nusamntlb).HasColumnName("nusamntlb");

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

                entity.Property(e => e.Pederelcred).HasColumnName("pederelcred");

                entity.Property(e => e.Peno)
                    .HasColumnType("numeric(6, 0)")
                    .HasColumnName("peno");

                entity.Property(e => e.Pestamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("pestamp")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.Pntf).HasColumnName("pntf");

                entity.Property(e => e.Profission)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("profission")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Pwautent)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("pwautent")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Pwpos)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("pwpos")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Rgpdadm).HasColumnName("rgpdadm");

                entity.Property(e => e.Setpasswd).HasColumnName("setpasswd");

                entity.Property(e => e.Setpasswdintra).HasColumnName("setpasswdintra");

                entity.Property(e => e.Sgqadm).HasColumnName("sgqadm");

                entity.Property(e => e.Skypeid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("skypeid")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Smsemail)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("smsemail")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Susername)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("susername")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Suserno)
                    .HasColumnType("numeric(6, 0)")
                    .HasColumnName("suserno");

                entity.Property(e => e.Synactcem).HasColumnName("synactcem");

                entity.Property(e => e.Synactcvi).HasColumnName("synactcvi");

                entity.Property(e => e.Syncactcts).HasColumnName("syncactcts");

                entity.Property(e => e.Syncactmx).HasColumnName("syncactmx");

                entity.Property(e => e.Syncacttda).HasColumnName("syncacttda");

                entity.Property(e => e.Synccts).HasColumnName("synccts");

                entity.Property(e => e.Syncem).HasColumnName("syncem");

                entity.Property(e => e.Syncimpnovatda).HasColumnName("syncimpnovatda");

                entity.Property(e => e.Syncimpnovatta).HasColumnName("syncimpnovatta");

                entity.Property(e => e.Syncmx).HasColumnName("syncmx");

                entity.Property(e => e.Synctda).HasColumnName("synctda");

                entity.Property(e => e.Synctta).HasColumnName("synctta");

                entity.Property(e => e.Syncvi).HasColumnName("syncvi");

                entity.Property(e => e.Tecnico)
                    .HasColumnType("numeric(6, 0)")
                    .HasColumnName("tecnico");

                entity.Property(e => e.Tecnnm)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("tecnnm")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Tema)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("tema")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Tembws).HasColumnName("tembws");

                entity.Property(e => e.Temveriprstock).HasColumnName("temveriprstock");

                entity.Property(e => e.Tipoacd)
                    .HasColumnType("numeric(1, 0)")
                    .HasColumnName("tipoacd");

                entity.Property(e => e.Tipoacdvs)
                    .HasColumnType("numeric(1, 0)")
                    .HasColumnName("tipoacdvs");

                entity.Property(e => e.Tlmvl)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("tlmvl")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Tntf)
                    .HasColumnType("numeric(2, 0)")
                    .HasColumnName("tntf");

                entity.Property(e => e.UAssin)
                    .HasColumnType("text")
                    .HasColumnName("u_assin")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UBztpass)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("u_bztpass")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UCancela)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("u_cancela");

                entity.Property(e => e.UCct)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("u_cct")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UCctst)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("u_cctst")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UCcusto)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("u_ccusto")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UClient1)
                    .HasMaxLength(253)
                    .IsUnicode(false)
                    .HasColumnName("u_client1")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UContrat).HasColumnName("u_contrat");

                entity.Property(e => e.UCu)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("u_cu")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UNaoper)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("u_naoper")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UNapag)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("u_napag")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UNapess)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("u_napess")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UNapessmv)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("u_napessmv")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UNaproj)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("u_naproj")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UNaprov)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("u_naprov")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UNcontaoc).HasColumnName("u_ncontaoc");

                entity.Property(e => e.UNo)
                    .HasColumnType("numeric(6, 0)")
                    .HasColumnName("u_no");

                entity.Property(e => e.UNome)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("u_nome")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UResponsa).HasColumnName("u_responsa");

                entity.Property(e => e.USuperus).HasColumnName("u_superus");

                entity.Property(e => e.UTerminal)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("u_terminal")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UVmax)
                    .HasColumnType("numeric(16, 2)")
                    .HasColumnName("u_vmax");

                entity.Property(e => e.Ugstamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("ugstamp")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Usaarea).HasColumnName("usaarea");

                entity.Property(e => e.Usatimezone).HasColumnName("usatimezone");

                entity.Property(e => e.Usavanc).HasColumnName("usavanc");

                entity.Property(e => e.Use2fa).HasColumnName("use2fa");

                entity.Property(e => e.Usercode)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("usercode")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Username)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("username")
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

                entity.Property(e => e.Usstamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("usstamp")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.Utcbrowser)
                    .HasColumnType("numeric(1, 0)")
                    .HasColumnName("utcbrowser");

                entity.Property(e => e.Utcdisplayname)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("utcdisplayname")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Utcuserid)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("utcuserid")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Vendedor)
                    .HasColumnType("numeric(6, 0)")
                    .HasColumnName("vendedor");

                entity.Property(e => e.Vendnm)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("vendnm")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Verificachamadas).HasColumnName("verificachamadas");

                entity.Property(e => e.Vsstamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("vsstamp")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();
            });

            modelBuilder.Entity<ApiLogs>(entity =>
            {
                entity.HasKey(e => e.UApilogstamp)
                    .HasName("PK__api_logs__D377A6C4446863E2");

                entity.ToTable("u_api_logs");

                entity.Property(e => e.UApilogstamp)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("u_apilogstamp");

                entity.Property(e => e.Code)
                    .IsUnicode(false)
                    .HasColumnName("code");

                entity.Property(e => e.Content)
                    .IsUnicode(false)
                    .HasColumnName("content");

                entity.Property(e => e.Data)
                    .HasColumnType("datetime")
                    .HasColumnName("data");

                entity.Property(e => e.Operation)
                    .IsUnicode(false)
                    .HasColumnName("operation");

                entity.Property(e => e.RequestId)
                    .IsUnicode(false)
                    .HasColumnName("requestId");

                entity.Property(e => e.ResponseDesc)
                    .IsUnicode(false)
                    .HasColumnName("responseDesc");

                entity.Property(e => e.ResponseText)
                    .IsUnicode(false)
                    .HasColumnName("responsetext");
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.HasKey(e => e.Logstamp)
                    .HasName("PK__u_logs__9803C30F60140409");

                entity.ToTable("u_logs");

                entity.Property(e => e.Logstamp)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("u_logsstamp");

                entity.Property(e => e.Code)
                    .IsUnicode(false)
                    .HasColumnName("code");

                entity.Property(e => e.Content)
                    .IsUnicode(false)
                    .HasColumnName("content");

                entity.Property(e => e.ResponseText)
                    .IsUnicode(false)
                    .HasColumnName("responsetext");

                entity.Property(e => e.Data)
                    .HasColumnType("datetime")
                    .HasColumnName("data");

                entity.Property(e => e.Operation)
                    .IsUnicode(false)
                    .HasColumnName("operation");

                entity.Property(e => e.RequestId)
                    .IsUnicode(false)
                    .HasColumnName("requestId");

                entity.Property(e => e.ResponseDesc)
                    .IsUnicode(false)
                    .HasColumnName("responseDesc");
            });

            modelBuilder.Entity<UProvider>(entity =>
            {

                entity.HasKey(e => e.u_providerstamp);
                entity.ToTable("u_provider");
                entity.Property(e => e.u_providerstamp).HasColumnName("u_providerstamp");
                entity.Property(e => e.chave);
                entity.Property(e => e.grupo);
                entity.Property(e => e.codigo);
                entity.Property(e => e.valor);

            });


            modelBuilder.Entity<Cl>(entity =>
            {
                entity.HasKey(e => new { e.No, e.Estab })
                    .HasName("pk_cl")
                    .IsClustered(false);

                entity.ToTable("cl");

                entity.HasIndex(e => new { e.Nome, e.Nome2, e.No, e.Estab, e.Clstamp }, "in_cl_cllist")
                    .HasFillFactor(70);

                entity.HasIndex(e => e.No, "in_cl_no")
                    .HasFillFactor(70);

                entity.HasIndex(e => e.Nome, "in_cl_nome")
                    .HasFillFactor(70);

                entity.HasIndex(e => e.Clstamp, "in_cl_stamp")
                    .IsUnique()
                    .HasFillFactor(70);

                entity.Property(e => e.No)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("no");

                entity.Property(e => e.Estab)
                    .HasColumnType("numeric(3, 0)")
                    .HasColumnName("estab");

                entity.Property(e => e.Clstamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("clstamp")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.Fref)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("fref")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Id)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("id")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Inactivo).HasColumnName("inactivo");

                entity.Property(e => e.Local)
                    .HasMaxLength(43)
                    .IsUnicode(false)
                    .HasColumnName("local")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Marcada).HasColumnName("marcada");

                entity.Property(e => e.Morada)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("morada")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Naturalid)
                    .HasMaxLength(17)
                    .IsUnicode(false)
                    .HasColumnName("naturalid")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Nib)
                    .HasMaxLength(28)
                    .IsUnicode(false)
                    .HasColumnName("nib")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Nome)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("nome")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Nome2)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("nome2")
                    .HasDefaultValueSql("('')");

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

                entity.Property(e => e.Segmento)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("segmento")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("tipo")
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

                entity.Property(e => e.Zona)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("zona")
                    .HasDefaultValueSql("('')");
            });


            modelBuilder.Entity<Em>(entity =>
            {
                entity.HasKey(e => e.No)
                    .HasName("pk_em")
                    .IsClustered(false);

                entity.ToTable("em");

                entity.HasIndex(e => new { e.Nome, e.No, e.Emstamp }, "in_em_emlist")
                    .HasFillFactor(70);

                entity.HasIndex(e => e.Ncont, "in_em_ncont")
                    .HasFillFactor(70);

                entity.HasIndex(e => e.No, "in_em_no")
                    .HasFillFactor(70);

                entity.HasIndex(e => e.Nome, "in_em_nome")
                    .HasFillFactor(70);

                entity.HasIndex(e => e.Emstamp, "in_em_stamp")
                    .IsUnique()
                    .HasFillFactor(70);

                entity.Property(e => e.No)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("no");

                entity.Property(e => e.Area)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("area")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.Clno)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("clno");

                entity.Property(e => e.Codpais)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("codpais")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Concelho)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("concelho")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Cont2)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("cont2")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Cont3)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("cont3")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Ctact2tel)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ctact2tel")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Ctact3tel)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ctact3tel")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Distrito)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("distrito")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Emstamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("emstamp")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.Latitude)
                    .HasColumnType("numeric(10, 6)")
                    .HasColumnName("latitude");

                entity.Property(e => e.Local)
                    .HasMaxLength(43)
                    .IsUnicode(false)
                    .HasColumnName("local")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Longitude)
                    .HasColumnType("numeric(10, 6)")
                    .HasColumnName("longitude");

                entity.Property(e => e.Morada)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("morada")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Ncont)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ncont")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Nome)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("nome")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Obs)
                    .HasColumnType("text")
                    .HasColumnName("obs")
                    .HasDefaultValueSql("('')");

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

                entity.Property(e => e.Pais)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("pais")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Pncont)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("pncont")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Segmento)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("segmento")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Telefone)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("telefone")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UAndar)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("u_andar")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UBidata)
                    .HasColumnType("datetime")
                    .HasColumnName("u_bidata")
                    .HasDefaultValueSql("(CONVERT([datetime],'19000101',0))");

                entity.Property(e => e.UBilocal)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("u_bilocal")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UBino)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("u_bino")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UDistrit)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("u_distrit")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UEndereco)
                    .HasMaxLength(254)
                    .IsUnicode(false)
                    .HasColumnName("u_endereco")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UKoboid)
                    .HasColumnType("numeric(15, 0)")
                    .HasColumnName("u_koboid");

                entity.Property(e => e.UKoboori).HasColumnName("u_koboori");

                entity.Property(e => e.UNascimen)
                    .HasColumnType("datetime")
                    .HasColumnName("u_nascimen")
                    .HasDefaultValueSql("(CONVERT([datetime],'19000101'))");

                entity.Property(e => e.UNatural)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("u_natural")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UNcasa)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("u_ncasa");

                entity.Property(e => e.UNquart)
                    .HasColumnType("numeric(5, 0)")
                    .HasColumnName("u_nquart");

                entity.Property(e => e.UNrua)
                    .HasColumnType("numeric(16, 0)")
                    .HasColumnName("u_nrua");

                entity.Property(e => e.UNtalhao)
                    .HasColumnType("numeric(5, 0)")
                    .HasColumnName("u_ntalhao");

                entity.Property(e => e.UOrigem)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("u_origem")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UPorta)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("u_porta")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UPref)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("u_pref")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UProvince)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("u_province")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.URua)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("u_rua")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Url)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("url")
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

                entity.Property(e => e.Vendedor)
                    .HasColumnType("numeric(4, 0)")
                    .HasColumnName("vendedor");

                entity.Property(e => e.Vendnm)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("vendnm")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Zona)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("zona")
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<Bo>(entity =>
            {
                entity.HasKey(e => new { e.Ndos, e.Obrano, e.Boano })
                    .HasName("pk_bo")
                    .IsClustered(false);

                entity.ToTable("bo");

                entity.HasIndex(e => new { e.Ndos, e.Obrano, e.Boano }, "in_bo_ndos_ano")
                    .HasFillFactor(70);

                entity.HasIndex(e => new { e.Ndos, e.No, e.Obrano }, "in_bo_ndos_no")
                    .HasFillFactor(70);

                entity.HasIndex(e => e.No, "in_bo_no")
                    .HasFillFactor(70);

                entity.HasIndex(e => e.Obrano, "in_bo_obrano")
                    .HasFillFactor(70);

                entity.HasIndex(e => e.Bostamp, "in_bo_stamp")
                    .IsUnique()
                    .HasFillFactor(70);

                entity.Property(e => e.Ndos)
                    .HasColumnType("numeric(3, 0)")
                    .HasColumnName("ndos");

                entity.Property(e => e.Obrano)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("obrano");

                entity.Property(e => e.Boano)
                    .HasColumnType("numeric(4, 0)")
                    .HasColumnName("boano");

                entity.Property(e => e.Bostamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("bostamp")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.Ccusto)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ccusto")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Cobranca)
                    .HasMaxLength(22)
                    .IsUnicode(false)
                    .HasColumnName("cobranca")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Codpost)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("codpost")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Custo)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("custo");


                entity.Property(e => e.Dataobra)
                    .HasColumnType("datetime")
                    .HasColumnName("dataobra")
                    .HasDefaultValueSql("(CONVERT([datetime],'19000101',0))");

                entity.Property(e => e.Ecusto)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("ecusto");


                entity.Property(e => e.Fref)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("fref")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Impresso).HasColumnName("impresso");

                entity.Property(e => e.Infref).HasColumnName("infref");

                entity.Property(e => e.Lifref).HasColumnName("lifref");

                entity.Property(e => e.Local)
                    .HasMaxLength(43)
                    .IsUnicode(false)
                    .HasColumnName("local")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Maquina)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("maquina")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Marca)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("marca")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Memissao)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("memissao")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Moeda)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("moeda")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Morada)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("morada")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Ncont)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ncont")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Ncusto)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ncusto")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Nmdos)
                    .HasMaxLength(24)
                    .IsUnicode(false)
                    .HasColumnName("nmdos")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.No)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("no");

                entity.Property(e => e.Nome)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("nome")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.Nome2)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("nome2")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Obs)
                    .HasMaxLength(67)
                    .IsUnicode(false)
                    .HasColumnName("obs")
                    .HasDefaultValueSql("('')");

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

                entity.Property(e => e.Period)
                    .HasColumnType("numeric(4, 0)")
                    .HasColumnName("period");

                entity.Property(e => e.Segmento)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("segmento")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Serie)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("serie")
                    .HasDefaultValueSql("('')");


                entity.Property(e => e.Tabela1)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("tabela1")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Trab1)
                    .HasMaxLength(67)
                    .IsUnicode(false)
                    .HasColumnName("trab1")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Trab2)
                    .HasMaxLength(67)
                    .IsUnicode(false)
                    .HasColumnName("trab2")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Trab3)
                    .HasMaxLength(67)
                    .IsUnicode(false)
                    .HasColumnName("trab3")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Trab4)
                    .HasMaxLength(67)
                    .IsUnicode(false)
                    .HasColumnName("trab4")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Trab5)
                    .HasMaxLength(67)
                    .IsUnicode(false)
                    .HasColumnName("trab5")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Ultfact)
                    .HasColumnType("datetime")
                    .HasColumnName("ultfact")
                    .HasDefaultValueSql("(CONVERT([datetime],'19000101',0))");

                entity.Property(e => e.Userimpresso)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("userimpresso")
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

                entity.Property(e => e.Zona)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("zona")
                    .HasDefaultValueSql("('')");
            });



            modelBuilder.Entity<Bo2>(entity =>
            {
                entity.HasKey(e => e.Bo2stamp)
                    .HasName("pk_bo2")
                    .IsClustered(false);

                entity.ToTable("bo2");

                entity.HasIndex(e => new { e.Bo2stamp, e.Anulado }, "in_bo2_anulado")
                    .HasFillFactor(70);

                entity.Property(e => e.Bo2stamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("bo2stamp")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.Adjbostamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("adjbostamp")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.Adjudicado).HasColumnName("adjudicado");

                entity.Property(e => e.Alvstamp1)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("alvstamp1")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Alvstamp2)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("alvstamp2")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Anulado).HasColumnName("anulado");

                entity.Property(e => e.Ar2mazem)
                    .HasColumnType("numeric(5, 0)")
                    .HasColumnName("ar2mazem");

                entity.Property(e => e.Area)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("area")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.Armazem)
                    .HasColumnType("numeric(5, 0)")
                    .HasColumnName("armazem");

                entity.Property(e => e.Assinatura)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("assinatura")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Atcodeid)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("atcodeid")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Autobostamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("autobostamp")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.Autono)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("autono");

                entity.Property(e => e.Autoper)
                    .HasColumnType("numeric(5, 0)")
                    .HasColumnName("autoper");

                entity.Property(e => e.Autos).HasColumnName("autos");

                entity.Property(e => e.Autotipo)
                    .HasColumnType("numeric(1, 0)")
                    .HasColumnName("autotipo");

                entity.Property(e => e.Bo71Bins)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("bo71_bins");

                entity.Property(e => e.Bo71Iva)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("bo71_iva");

                entity.Property(e => e.Bo72Bins)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("bo72_bins");

                entity.Property(e => e.Bo72Iva)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("bo72_iva");

                entity.Property(e => e.Bo81Bins)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("bo81_bins");

                entity.Property(e => e.Bo81Iva)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("bo81_iva");

                entity.Property(e => e.Bo82Bins)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("bo82_bins");

                entity.Property(e => e.Bo82Iva)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("bo82_iva");

                entity.Property(e => e.Bo91Bins)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("bo91_bins");

                entity.Property(e => e.Bo91Iva)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("bo91_iva");

                entity.Property(e => e.Bo92Bins)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("bo92_bins");

                entity.Property(e => e.Bo92Iva)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("bo92_iva");

                entity.Property(e => e.Calistamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("calistamp")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.Cambio)
                    .HasColumnType("numeric(20, 12)")
                    .HasColumnName("cambio");

                entity.Property(e => e.Cambiofixo).HasColumnName("cambiofixo");

                entity.Property(e => e.Carga)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("carga")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Cladrsdesc)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cladrsdesc")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Cladrsstamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("cladrsstamp")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Cladrszona)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("cladrszona")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Codpost)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("codpost")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Consfinal).HasColumnName("consfinal");

                entity.Property(e => e.Contacto)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("contacto")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Crpstamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("crpstamp")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.Custototaldif)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("custototaldif");

                entity.Property(e => e.Custototalorc)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("custototalorc");

                entity.Property(e => e.Custototalreorc)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("custototalreorc");

                entity.Property(e => e.Descar)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("descar")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Descnegocio)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("descnegocio")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Descobra)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("descobra")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Diasate)
                    .HasColumnType("numeric(4, 0)")
                    .HasColumnName("diasate");

                entity.Property(e => e.Diasde)
                    .HasColumnType("numeric(4, 0)")
                    .HasColumnName("diasde");

                entity.Property(e => e.Dpedidopv)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("dpedidopv")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Dprocesso)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("dprocesso")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Ebo71Bins)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("ebo71_bins");

                entity.Property(e => e.Ebo71Iva)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("ebo71_iva");

                entity.Property(e => e.Ebo72Bins)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("ebo72_bins");

                entity.Property(e => e.Ebo72Iva)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("ebo72_iva");

                entity.Property(e => e.Ebo81Bins)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("ebo81_bins");

                entity.Property(e => e.Ebo81Iva)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("ebo81_iva");

                entity.Property(e => e.Ebo82Bins)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("ebo82_bins");

                entity.Property(e => e.Ebo82Iva)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("ebo82_iva");

                entity.Property(e => e.Ebo91Bins)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("ebo91_bins");

                entity.Property(e => e.Ebo91Iva)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("ebo91_iva");

                entity.Property(e => e.Ebo92Bins)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("ebo92_bins");

                entity.Property(e => e.Ebo92Iva)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("ebo92_iva");

                entity.Property(e => e.Ecustototaldif)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("ecustototaldif");

                entity.Property(e => e.Ecustototalorc)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("ecustototalorc");

                entity.Property(e => e.Ecustototalreorc)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("ecustototalreorc");

                entity.Property(e => e.EftaxamtA)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("eftaxamt_a");

                entity.Property(e => e.EftaxamtB)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("eftaxamt_b");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Emargemtotaldif)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("emargemtotaldif");

                entity.Property(e => e.Emargemtotalorc)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("emargemtotalorc");

                entity.Property(e => e.Emargemtotalreorc)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("emargemtotalreorc");

                entity.Property(e => e.Encm)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("encm")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Encmdesc)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("encmdesc")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Etotalciva)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("etotalciva");

                entity.Property(e => e.Etotiva)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("etotiva");

                entity.Property(e => e.Ettecoval)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("ettecoval");

                entity.Property(e => e.Ettecoval2)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("ettecoval2");

                entity.Property(e => e.Ettieca)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("ettieca");

                entity.Property(e => e.Excm)
                    .HasColumnType("numeric(2, 0)")
                    .HasColumnName("excm");

                entity.Property(e => e.Excmdesc)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("excmdesc")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Exportado).HasColumnName("exportado");

                entity.Property(e => e.FtaxamtA)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("ftaxamt_a");

                entity.Property(e => e.FtaxamtB)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("ftaxamt_b");

                entity.Property(e => e.Horasl)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("horasl")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Identificacao1)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("identificacao1")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Identificacao2)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("identificacao2")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Idserie)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("idserie")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Iectisento).HasColumnName("iectisento");

                entity.Property(e => e.Local)
                    .HasMaxLength(43)
                    .IsUnicode(false)
                    .HasColumnName("local")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Marcada).HasColumnName("marcada");

                entity.Property(e => e.Margemorcperc)
                    .HasColumnType("numeric(15, 2)")
                    .HasColumnName("margemorcperc");

                entity.Property(e => e.Margemreorcperc)
                    .HasColumnType("numeric(15, 2)")
                    .HasColumnName("margemreorcperc");

                entity.Property(e => e.Margemtotaldif)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("margemtotaldif");

                entity.Property(e => e.Margemtotalorc)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("margemtotalorc");

                entity.Property(e => e.Margemtotalreorc)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("margemtotalreorc");

                entity.Property(e => e.Mcomercial)
                    .HasColumnType("numeric(6, 2)")
                    .HasColumnName("mcomercial");

                entity.Property(e => e.Mensaldia)
                    .HasColumnType("numeric(2, 0)")
                    .HasColumnName("mensaldia");

                entity.Property(e => e.Morada)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("morada")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Mtotalciva)
                    .HasColumnType("numeric(13, 3)")
                    .HasColumnName("mtotalciva");

                entity.Property(e => e.Mtotiva)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("mtotiva");

                entity.Property(e => e.Mttieca)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("mttieca");

                entity.Property(e => e.Ncin).HasColumnName("ncin");

                entity.Property(e => e.Ncout).HasColumnName("ncout");

                entity.Property(e => e.Ndosmanual)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ndosmanual")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Negocio)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("negocio")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Ngstamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("ngstamp")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Ngstatus)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("ngstatus")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Nocts)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("nocts");

                entity.Property(e => e.Nomects)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("nomects")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Nopkng)
                    .HasColumnType("numeric(8, 0)")
                    .HasColumnName("nopkng");

                entity.Property(e => e.Ntcm)
                    .HasColumnType("numeric(2, 0)")
                    .HasColumnName("ntcm");

                entity.Property(e => e.Obranomanual)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("obranomanual");

                entity.Property(e => e.Obranoorcamento)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("obranoorcamento");

                entity.Property(e => e.Orcamento).HasColumnName("orcamento");

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
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.Pdtipo)
                    .HasColumnType("numeric(1, 0)")
                    .HasColumnName("pdtipo");

                entity.Property(e => e.Planeamento).HasColumnName("planeamento");

                entity.Property(e => e.Processo)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("processo")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Pscm)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("pscm")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Pscmdesc)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("pscmdesc")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Pscmori)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("pscmori")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Pscmoridesc)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("pscmoridesc")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Ptcm)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("ptcm")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Ptcmdesc)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("ptcmdesc")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Reorcamento).HasColumnName("reorcamento");

                entity.Property(e => e.Stamporcamento)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("stamporcamento")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Subproc)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("subproc")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Sujrvp).HasColumnName("sujrvp");

                entity.Property(e => e.Szzstamp1)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("szzstamp1")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Szzstamp2)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("szzstamp2")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Tbrsemmed).HasColumnName("tbrsemmed");

                entity.Property(e => e.Telefone)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("telefone")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Tipoobra)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("tipoobra")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Tiposaft)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("tiposaft")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Tkhcarr)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("tkhcarr")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Tkhdata)
                    .HasColumnType("datetime")
                    .HasColumnName("tkhdata")
                    .HasDefaultValueSql("(CONVERT([datetime],'19000101',0))");

                entity.Property(e => e.Tkhdid)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("tkhdid");

                entity.Property(e => e.Tkhhora)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("tkhhora")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Tkhid)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("tkhid")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Tkhlpnt)
                    .HasColumnType("numeric(15, 5)")
                    .HasColumnName("tkhlpnt");

                entity.Property(e => e.Tkhlref)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("tkhlref")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Tkhltyp)
                    .HasColumnType("numeric(3, 0)")
                    .HasColumnName("tkhltyp");

                entity.Property(e => e.Tkhodo)
                    .HasColumnType("numeric(8, 0)")
                    .HasColumnName("tkhodo");

                entity.Property(e => e.Tkhopid)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("tkhopid")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Tkhpan)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("tkhpan")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Tkhposcstamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("tkhposcstamp")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Tkhref)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("tkhref")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Tkhshf)
                    .HasColumnType("numeric(6, 0)")
                    .HasColumnName("tkhshf");

                entity.Property(e => e.Tkhsttnr)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("tkhsttnr")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Tkhtyp)
                    .HasColumnType("numeric(1, 0)")
                    .HasColumnName("tkhtyp");

                entity.Property(e => e.Totalciva)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("totalciva");

                entity.Property(e => e.Totiva)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("totiva");

                entity.Property(e => e.Ttecoval)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("ttecoval");

                entity.Property(e => e.Ttecoval2)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("ttecoval2");

                entity.Property(e => e.Ttieca)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("ttieca");

                entity.Property(e => e.Usaintra).HasColumnName("usaintra");

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
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.Vencimento)
                    .HasColumnType("numeric(5, 0)")
                    .HasColumnName("vencimento");

                entity.Property(e => e.Versaochave)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("versaochave")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Versaocrono)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("versaocrono")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Versaorcamento)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("versaorcamento")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Xpddata)
                    .HasColumnType("datetime")
                    .HasColumnName("xpddata")
                    .HasDefaultValueSql("(CONVERT([datetime],'19000101',0))");

                entity.Property(e => e.Xpdhora)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("xpdhora")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Xpdviatura)
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("xpdviatura")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Zncm)
                    .HasColumnType("numeric(2, 0)")
                    .HasColumnName("zncm");

                entity.Property(e => e.Znregiao)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("znregiao")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Zona1)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("zona1")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Zona2)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("zona2")
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<Bo3>(entity =>
            {
                entity.HasKey(e => e.Bo3stamp)
                    .HasName("pk_bo3")
                    .IsClustered(false);

                entity.ToTable("bo3");

                entity.Property(e => e.Bo3stamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("bo3stamp")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.Anuldata)
                    .HasColumnType("datetime")
                    .HasColumnName("anuldata")
                    .HasDefaultValueSql("(CONVERT([datetime],'19000101'))");

                entity.Property(e => e.Anulhora)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("anulhora")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Anulinis)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("anulinis")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Atcud)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("atcud")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Barcode)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("barcode")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Cobradovmbway).HasColumnName("cobradovmbway");

                entity.Property(e => e.Cobradovpaypal).HasColumnName("cobradovpaypal");

                entity.Property(e => e.Cobradovunicre).HasColumnName("cobradovunicre");

                entity.Property(e => e.Codendereco)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("codendereco")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Codeuserpay)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("codeuserpay")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Codmotiseimp)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("codmotiseimp")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Codpais)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("codpais")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Codpromo)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("codpromo")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Contingencia)
                    .HasColumnType("numeric(1, 0)")
                    .HasColumnName("contingencia");

                entity.Property(e => e.Descpais)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("descpais")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Distrito)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("distrito")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Documentnumberori)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("documentnumberori")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Entidademb)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("entidademb")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Etotalmb)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("etotalmb");

                entity.Property(e => e.Latitude)
                    .HasColumnType("numeric(10, 6)")
                    .HasColumnName("latitude");

                entity.Property(e => e.Latitudecarga)
                    .HasColumnType("numeric(10, 6)")
                    .HasColumnName("latitudecarga");

                entity.Property(e => e.Longitude)
                    .HasColumnType("numeric(10, 6)")
                    .HasColumnName("longitude");

                entity.Property(e => e.Longitudecarga)
                    .HasColumnType("numeric(10, 6)")
                    .HasColumnName("longitudecarga");

                entity.Property(e => e.Marcada).HasColumnName("marcada");

                entity.Property(e => e.Meiotranscv)
                    .HasColumnType("numeric(1, 0)")
                    .HasColumnName("meiotranscv");

                entity.Property(e => e.Motanul)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("motanul")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Motiseimp)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("motiseimp")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Motorista)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("motorista")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Onlinepay).HasColumnName("onlinepay");

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

                entity.Property(e => e.Pagomb).HasColumnName("pagomb");

                entity.Property(e => e.Refmb1)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("refmb1")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Refmb2)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("refmb2")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Refmb3)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("refmb3")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Taxpointdt)
                    .HasColumnType("datetime")
                    .HasColumnName("taxpointdt")
                    .HasDefaultValueSql("(CONVERT([datetime],'19000101',0))");

                entity.Property(e => e.Telefone)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("telefone")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Totalmb)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("totalmb");

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
            });

            modelBuilder.Entity<Ft>(entity =>
            {
                entity.HasKey(e => new { e.Ndoc, e.Fno, e.Ftano })
                    .HasName("pk_ft")
                    .IsClustered(false);

                entity.ToTable("ft");

                entity.HasIndex(e => e.Anulado, "in_ft_anulado")
                    .HasFillFactor(70);

                entity.HasIndex(e => e.Fdata, "in_ft_fdata")
                    .HasFillFactor(70);

                entity.HasIndex(e => e.Nome, "in_ft_ftlist")
                    .HasFillFactor(70);

                entity.HasIndex(e => e.No, "in_ft_no")
                    .HasFillFactor(70);

                entity.HasIndex(e => e.Nome, "in_ft_nome")
                    .HasFillFactor(70);

                entity.HasIndex(e => e.Ftstamp, "in_ft_stamp")
                    .IsUnique()
                    .HasFillFactor(70);

                entity.HasIndex(e => e.Tipodoc, "in_ft_tipo_anula_ft_ndoc")
                    .HasFillFactor(70);

                entity.HasIndex(e => e.Tipodoc, "in_ft_tipodoc")
                    .HasFillFactor(70);

                entity.HasIndex(e => e.Vendedor, "in_ft_vendedor")
                    .HasFillFactor(70);

                entity.Property(e => e.Ndoc)
                    .HasColumnType("numeric(3, 0)")
                    .HasColumnName("ndoc");

                entity.Property(e => e.Fno)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("fno");

                entity.Property(e => e.Ftano)
                    .HasColumnType("numeric(4, 0)")
                    .HasColumnName("ftano");

                entity.Property(e => e.Anulado).HasColumnName("anulado");

                entity.Property(e => e.Bidata)
                    .HasColumnType("datetime")
                    .HasColumnName("bidata")
                    .HasDefaultValueSql("(convert(datetime,'19000101'))");

                entity.Property(e => e.Bilocal)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("bilocal")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Codpost)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("codpost")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Descc)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("descc");

                entity.Property(e => e.Descm)
                    .HasColumnType("numeric(13, 3)")
                    .HasColumnName("descm");

                entity.Property(e => e.Fdata)
                    .HasColumnType("datetime")
                    .HasColumnName("fdata")
                    .HasDefaultValueSql("(convert(datetime,'19000101'))");

                entity.Property(e => e.Ftid)
                    .HasColumnType("numeric(12, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ftid");

                entity.Property(e => e.Ftstamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("ftstamp")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.Local)
                    .HasMaxLength(43)
                    .IsUnicode(false)
                    .HasColumnName("local")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Morada)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("morada")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Ncont)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ncont")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Nmdoc)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("nmdoc")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.No)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("no");

                entity.Property(e => e.Nome)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("nome")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

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

                entity.Property(e => e.Pais)
                    .HasColumnType("numeric(1, 0)")
                    .HasColumnName("pais");

                entity.Property(e => e.Telefone)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("telefone")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Tipodoc)
                    .HasColumnType("numeric(1, 0)")
                    .HasColumnName("tipodoc");

                entity.Property(e => e.Tmiliq)
                    .HasColumnType("numeric(15, 3)")
                    .HasColumnName("tmiliq");

                entity.Property(e => e.Total)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("total");

                entity.Property(e => e.Ttiliq)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("ttiliq");

                entity.Property(e => e.Ttiva)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("ttiva");

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

                entity.Property(e => e.Vendedor)
                    .HasColumnType("numeric(4, 0)")
                    .HasColumnName("vendedor");

                entity.Property(e => e.Vendnm)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("vendnm")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Zona)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("zona")
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<Ft3>(entity =>
            {
                entity.HasKey(e => e.Ft3stamp)
                    .HasName("pk_ft3")
                    .IsClustered(false);

                entity.ToTable("ft3");

                entity.HasIndex(e => new { e.Seriecode, e.Docno }, "in_ft3_docno")
                    .IsUnique()
                    .HasFillFactor(70);

                entity.Property(e => e.Ft3stamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("ft3stamp")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.Anexo40)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("anexo40")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Anexo41)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("anexo41")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Anularetif)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("anularetif")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Anuldata)
                    .HasColumnType("datetime")
                    .HasColumnName("anuldata")
                    .HasDefaultValueSql("(CONVERT([datetime],'19000101'))");

                entity.Property(e => e.Anulhora)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("anulhora")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Anulinis)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("anulinis")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Atcud)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("atcud")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Barcode)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("barcode")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.C2codpais)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("c2codpais")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.C2descpais)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("c2descpais")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.C2distrito)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("c2distrito")
                    .HasDefaultValueSql("('')");

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

                entity.Property(e => e.Cobradovmbway).HasColumnName("cobradovmbway");

                entity.Property(e => e.Codendereco)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("codendereco")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Codmotivreg)
                    .HasColumnType("numeric(1, 0)")
                    .HasColumnName("codmotivreg");

                entity.Property(e => e.Codpais)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("codpais")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Contingencia)
                    .HasColumnType("numeric(1, 0)")
                    .HasColumnName("contingencia");

                entity.Property(e => e.Descpais)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("descpais")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Diaplano)
                    .HasColumnType("numeric(2, 0)")
                    .HasColumnName("diaplano");

                entity.Property(e => e.Dilnoplano)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("dilnoplano");

                entity.Property(e => e.Dinoplano)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("dinoplano")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Distrito)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("distrito")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Docno)
                    .HasColumnType("numeric(8, 0)")
                    .HasColumnName("docno");

                entity.Property(e => e.Dostamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("dostamp")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.Dplano)
                    .HasColumnType("datetime")
                    .HasColumnName("dplano")
                    .HasDefaultValueSql("(CONVERT([datetime],'19000101'))");

                entity.Property(e => e.Eivacativado)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("eivacativado");

                entity.Property(e => e.Etotgroj)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("etotgroj");

                entity.Property(e => e.Etotrc)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("etotrc");

                entity.Property(e => e.Fiscalcode)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("fiscalcode")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Fttxirs)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("fttxirs");

                entity.Property(e => e.Invoicenoori)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("invoicenoori")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Invoiceyear)
                    .HasColumnType("numeric(4, 0)")
                    .HasColumnName("invoiceyear");

                entity.Property(e => e.Ivacativado)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("ivacativado");

                entity.Property(e => e.Latitude)
                    .HasColumnType("numeric(10, 6)")
                    .HasColumnName("latitude");

                entity.Property(e => e.Latitudecarga)
                    .HasColumnType("numeric(10, 6)")
                    .HasColumnName("latitudecarga");

                entity.Property(e => e.Longitude)
                    .HasColumnType("numeric(10, 6)")
                    .HasColumnName("longitude");

                entity.Property(e => e.Longitudecarga)
                    .HasColumnType("numeric(10, 6)")
                    .HasColumnName("longitudecarga");

                entity.Property(e => e.Marcada).HasColumnName("marcada");

                entity.Property(e => e.Meiotranscv)
                    .HasColumnType("numeric(1, 0)")
                    .HasColumnName("meiotranscv");

                entity.Property(e => e.Mivacativado)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("mivacativado");

                entity.Property(e => e.Motanul)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("motanul")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Motivreg)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("motivreg")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Motivregoutro)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("motivregoutro")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Motorista)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("motorista")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Motretif)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("motretif")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Mtotgroj)
                    .HasColumnType("numeric(15, 3)")
                    .HasColumnName("mtotgroj");

                entity.Property(e => e.Mtotrc)
                    .HasColumnType("numeric(15, 3)")
                    .HasColumnName("mtotrc");

                entity.Property(e => e.Naoisenta).HasColumnName("naoisenta");

                entity.Property(e => e.Oricae)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("oricae")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Oridata)
                    .HasColumnType("datetime")
                    .HasColumnName("oridata")
                    .HasDefaultValueSql("(CONVERT([datetime],'19000101'))");

                entity.Property(e => e.Orihora)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("orihora")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Oriinis)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("oriinis")
                    .HasDefaultValueSql("('')");

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

                entity.Property(e => e.Plano).HasColumnName("plano");

                entity.Property(e => e.Planoonline).HasColumnName("planoonline");

                entity.Property(e => e.Ppallowpaynow).HasColumnName("ppallowpaynow");

                entity.Property(e => e.Ppallowsubscription).HasColumnName("ppallowsubscription");

                entity.Property(e => e.Ppbillingagreement)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("ppbillingagreement")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Ppperiodicidade)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("ppperiodicidade")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Ppperiodo)
                    .HasColumnType("numeric(3, 0)")
                    .HasColumnName("ppperiodo");

                entity.Property(e => e.Ppprofileid)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("ppprofileid")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Ppsbstamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("ppsbstamp")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.Pptransactionid)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("pptransactionid")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Refmbdtvalidade)
                    .HasColumnType("datetime")
                    .HasColumnName("refmbdtvalidade")
                    .HasDefaultValueSql("(CONVERT([datetime],'19000101'))");

                entity.Property(e => e.Region)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("region")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Seriecode)
                    .HasColumnType("numeric(12, 0)")
                    .HasColumnName("seriecode");

                entity.Property(e => e.Subscritovpaypal).HasColumnName("subscritovpaypal");

                entity.Property(e => e.Taxfree).HasColumnName("taxfree");

                entity.Property(e => e.Taxpointdt)
                    .HasColumnType("datetime")
                    .HasColumnName("taxpointdt")
                    .HasDefaultValueSql("(CONVERT([datetime],'19000101',0))");

                entity.Property(e => e.Tfdatanasc)
                    .HasColumnType("datetime")
                    .HasColumnName("tfdatanasc")
                    .HasDefaultValueSql("(CONVERT([datetime],'19000101'))");

                entity.Property(e => e.Tfdocid)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("tfdocid")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Tfdocnum)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("tfdocnum")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Tfdocpais)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("tfdocpais")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Tfdocpaisdesc)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("tfdocpaisdesc")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Tfdoctipo)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("tfdoctipo")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Tfeivain1)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("tfeivain1");

                entity.Property(e => e.Tfeivain2)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("tfeivain2");

                entity.Property(e => e.Tfeivain3)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("tfeivain3");

                entity.Property(e => e.Tfeivain4)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("tfeivain4");

                entity.Property(e => e.Tfeivain5)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("tfeivain5");

                entity.Property(e => e.Tfeivain6)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("tfeivain6");

                entity.Property(e => e.Tfeivain7)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("tfeivain7");

                entity.Property(e => e.Tfeivain8)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("tfeivain8");

                entity.Property(e => e.Tfeivain9)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("tfeivain9");

                entity.Property(e => e.Tfeivav1)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("tfeivav1");

                entity.Property(e => e.Tfeivav2)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("tfeivav2");

                entity.Property(e => e.Tfeivav3)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("tfeivav3");

                entity.Property(e => e.Tfeivav4)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("tfeivav4");

                entity.Property(e => e.Tfeivav5)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("tfeivav5");

                entity.Property(e => e.Tfeivav6)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("tfeivav6");

                entity.Property(e => e.Tfeivav7)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("tfeivav7");

                entity.Property(e => e.Tfeivav8)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("tfeivav8");

                entity.Property(e => e.Tfeivav9)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("tfeivav9");

                entity.Property(e => e.Tfgrossamount)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("tfgrossamount");

                entity.Property(e => e.Tfivatx1)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("tfivatx1");

                entity.Property(e => e.Tfivatx2)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("tfivatx2");

                entity.Property(e => e.Tfivatx3)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("tfivatx3");

                entity.Property(e => e.Tfivatx4)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("tfivatx4");

                entity.Property(e => e.Tfivatx5)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("tfivatx5");

                entity.Property(e => e.Tfivatx6)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("tfivatx6");

                entity.Property(e => e.Tfivatx7)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("tfivatx7");

                entity.Property(e => e.Tfivatx8)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("tfivatx8");

                entity.Property(e => e.Tfivatx9)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("tfivatx9");

                entity.Property(e => e.Tfpaisori)
                    .HasColumnType("numeric(1, 0)")
                    .HasColumnName("tfpaisori");

                entity.Property(e => e.Tfrefund)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("tfrefund");

                entity.Property(e => e.Tfserviceid)
                    .HasColumnType("numeric(3, 0)")
                    .HasColumnName("tfserviceid");

                entity.Property(e => e.Totgroj)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("totgroj");

                entity.Property(e => e.Totrc)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("totrc");

                entity.Property(e => e.Tpaaccountperiodnum)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("tpaaccountperiodnum")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Tpaamountconverted)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("tpaamountconverted")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Tpacommissionsignal)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("tpacommissionsignal")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Tpacommissionvalue)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("tpacommissionvalue")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Tpaissuername)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("tpaissuername")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Tpamessagenum)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("tpamessagenum")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Tpamreceipt)
                    .HasColumnType("text")
                    .HasColumnName("tpamreceipt")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Tpapan)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("tpapan")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Tpaposid)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("tpaposid")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Tpapurchase).HasColumnName("tpapurchase");

                entity.Property(e => e.Tpareceipt)
                    .HasColumnType("text")
                    .HasColumnName("tpareceipt")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Tpareceiptformat)
                    .HasColumnType("numeric(1, 0)")
                    .HasColumnName("tpareceiptformat");

                entity.Property(e => e.Tparesponsedatetime)
                    .HasMaxLength(14)
                    .IsUnicode(false)
                    .HasColumnName("tparesponsedatetime")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Tpasan)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("tpasan")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Tpatextforclientreceipt)
                    .HasColumnType("text")
                    .HasColumnName("tpatextforclientreceipt")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Tpatransactionseqnum)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("tpatransactionseqnum")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Tpatype)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("tpatype")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UAnomal)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("u_anomal")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UCalibre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("u_calibre")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UCalpstp)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("u_calpstp")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UContador)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("u_contador")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UEtotliq)
                    .HasColumnType("numeric(16, 5)")
                    .HasColumnName("u_etotliq");

                entity.Property(e => e.UFactu)
                    .HasColumnType("numeric(6, 0)")
                    .HasColumnName("u_factu");

                entity.Property(e => e.UFactura)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("u_factura");

                entity.Property(e => e.UGestcont)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("u_gestcont")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UGestnome)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("u_gestnome")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ULeiact)
                    .HasColumnType("numeric(16, 0)")
                    .HasColumnName("u_leiact");

                entity.Property(e => e.ULeiant)
                    .HasColumnType("numeric(16, 0)")
                    .HasColumnName("u_leiant");

                entity.Property(e => e.UMultado).HasColumnName("u_multado");

                entity.Property(e => e.UOfdata)
                    .HasColumnType("datetime")
                    .HasColumnName("u_ofdata")
                    .HasDefaultValueSql("(CONVERT([datetime],'19000101'))");

                entity.Property(e => e.UOftstamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("u_oftstamp")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UOristamp)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("u_oristamp")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UPeriodo)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("u_periodo")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UReal)
                    .HasColumnType("numeric(6, 0)")
                    .HasColumnName("u_real");

                entity.Property(e => e.UTipofac)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("u_tipofac")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UTotliq)
                    .HasColumnType("numeric(16, 5)")
                    .HasColumnName("u_totliq");

                entity.Property(e => e.UTotliqm)
                    .HasColumnType("numeric(16, 5)")
                    .HasColumnName("u_totliqm");

                entity.Property(e => e.UTx1)
                    .HasColumnType("numeric(16, 2)")
                    .HasColumnName("u_tx1");

                entity.Property(e => e.UWwfid)
                    .HasColumnType("numeric(12, 0)")
                    .HasColumnName("u_wwfid");

                entity.Property(e => e.UWwfstamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("u_wwfstamp")
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

                entity.Property(e => e.Walletreceiptid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("walletreceiptid")
                    .HasDefaultValueSql("('')");
            });

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

                entity.Property(e => e.UOrigem).HasColumnName("u_origem");
                entity.Property(e => e.UKoboid)
                    .HasColumnType("numeric(15, 0)")
                    .HasColumnName("u_koboid");

                entity.Property(e => e.UKoboOri).HasColumnName("u_koboOri");
                entity.Property(e => e.UKoboSync).HasColumnName("u_kobosync");

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

                entity.Property(e => e.Latitude)
                    .HasColumnType("numeric(10, 6)")
                    .HasColumnName("latitude");

                entity.Property(e => e.Longitude)
                    .HasColumnType("numeric(10, 6)")
                    .HasColumnName("longitude");

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

                entity.Property(e => e.UAndar)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("u_andar")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UClientid)
                    .HasColumnType("text")
                    .HasColumnName("u_clientid")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UDistrit)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("u_distrit")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UEndereco)
                    .HasMaxLength(254)
                    .IsUnicode(false)
                    .HasColumnName("u_endereco")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UInicio)
                    .HasColumnType("datetime")
                    .HasColumnName("u_inicio")
                    .HasDefaultValueSql("(CONVERT([datetime],'19000101'))");

                entity.Property(e => e.UIniciof)
                    .HasColumnType("datetime")
                    .HasColumnName("u_iniciof")
                    .HasDefaultValueSql("(CONVERT([datetime],'19000101'))");

                entity.Property(e => e.UNcasa)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("u_ncasa")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UNrua)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("u_nrua")
                    .HasDefaultValueSql("('')");

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

                entity.Property(e => e.UProvince)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("u_province")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.URua)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("u_rua")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UTermino)
                    .HasColumnType("datetime")
                    .HasColumnName("u_termino")
                    .HasDefaultValueSql("(CONVERT([datetime],'19000101'))");

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
            });


            OnModelCreatingPartial(modelBuilder);
        }


        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
