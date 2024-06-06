using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using FIPAG_KOBOTOOLBOX.Domains.Models;
using FIPAG_KOBOTOOLBOX.Extensions;

namespace FIPAG_KOBOTOOLBOX.Persistence.Contexts
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Log> Log { get; set; } = null!;
        public virtual DbSet<ApiLogs> ApiLogs { get; set; } = null!;
        public virtual DbSet<UProvider> UProvider { get; set; } = null!;

        public virtual DbSet<Cl> Cl { get; set; } = null!;
        public virtual DbSet<Em> Em { get; set; } = null!;

        public virtual DbSet<Us> Us { get; set; } = null!;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DBconnect"));
        }

        public string GetModelNameForTable(string tableName)
        {
            var entityType = Model.GetEntityTypes()
             .FirstOrDefault(et => string.Equals(et.GetTableName(), tableName, StringComparison.OrdinalIgnoreCase));

            return entityType?.ClrType.Name ?? "Modelo não encontrado";
        }

        public string GetPropertyNameForColumn(string tableName, string columnName)
        {
            var entityType = Model.GetEntityTypes()
                .FirstOrDefault(et => string.Equals(et.GetTableName(), tableName, StringComparison.OrdinalIgnoreCase));

            if (entityType == null)
                return "Tabela não encontrada";

            foreach (var property in entityType.GetProperties())
            {
                if (string.Equals(property.GetColumnName(), columnName, StringComparison.OrdinalIgnoreCase))
                {
                    return property.Name;
                }
            }

            return "Coluna não encontrada";
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (Database.IsSqlServer())
            {
                modelBuilder.AddSqlConvertFunction();
            }
            modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

            
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

                entity.HasKey(e => e.Clstamp)
                    .HasName("pk_cl")
                    .IsClustered(false);

                entity.Property(e => e.Clstamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("clstamp")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.Nome)
                .HasColumnName("nome");

            });


            modelBuilder.Entity<Em>(entity =>
            {

                entity.HasKey(e => e.Emstamp)
                    .HasName("pk_em")
                    .IsClustered(false);

                entity.Property(e => e.Emstamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("emstamp")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.Nome)
                .HasColumnName("nome");

                entity.Property(e => e.No)
                    .HasColumnType("numeric(16, 0)")
                    .HasColumnName("no");

                entity.Property(e => e.UKoboId)
                    .HasColumnType("numeric(15, 0)")
                    .HasColumnName("u_koboId");

                entity.Property(e => e.Ultivisita)
                  .HasDefaultValueSql("(CONVERT([datetime],'19000101',0))")
                  .HasColumnType("datetime")
                  .HasColumnName("ULTIVISITA");

                entity.Property(e => e.Ousrdata)
                    .HasColumnType("datetime")
                    .HasColumnName("ousrdata")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Ousrhora)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("ousrhora")
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

                entity.Property(e => e.UAndar)
                .HasColumnName("u_andar");

                entity.Property(e => e.UBiData)
                .HasColumnName("u_bidata");

                entity.Property(e => e.UBiLocal)
                .HasColumnName("u_bilocal");

                entity.Property(e => e.UBiNo)
                .HasColumnName("u_bino");

                entity.Property(e => e.UDistrit)
                .HasColumnName("u_distrit");

                entity.Property(e => e.UEndereco)
                .HasColumnName("u_endereco");

                entity.Property(e => e.UNRua)
                .HasColumnName("u_nrua");

                entity.Property(e => e.UNascimen)
                .HasColumnName("u_nascimen");

                entity.Property(e => e.UNatural)
                .HasColumnName("u_natural");
                entity.Property(e => e.UNcasa)
                .HasColumnName("u_ncasa");
                entity.Property(e => e.UNquart)
                .HasColumnName("u_nquart");
                entity.Property(e => e.UNtalhao)
                .HasColumnName("u_ntalhao");
                entity.Property(e => e.UPorta)
                .HasColumnName("u_porta");
                entity.Property(e => e.UPref)
                .HasColumnName("u_pref");
                entity.Property(e => e.UProvince)
                .HasColumnName("u_province");
                entity.Property(e => e.URua)
                .HasColumnName("u_rua");

                entity.Property(e => e.ConsFinal)
               .HasColumnName("ConsFinal");
                entity.Property(e => e.Cont3Pos).HasMaxLength(50) 
                    .HasColumnName("Cont3Pos");
                entity.Property(e => e.Cont2Pos).HasMaxLength(50) 
                    .HasColumnName("Cont2Pos");
                entity.Property(e => e.Morada).HasMaxLength(250) 
                    .HasColumnName("Morada");
                entity.Property(e => e.CPost).HasMaxLength(10) 
                    .HasColumnName("CPost");
                entity.Property(e => e.CPostL).HasMaxLength(50) 
                    .HasColumnName("CPostL");
                entity.Property(e => e.CTacto).HasMaxLength(50) 
                    .HasColumnName("CTacto");
                entity.Property(e => e.CTactPos).HasMaxLength(50) 
                    .HasColumnName("CTactPos");
                entity.Property(e => e.CTactTel).HasMaxLength(20) 
                    .HasColumnName("CTactTel");
                entity.Property(e => e.Cont2).HasMaxLength(50) 
                    .HasColumnName("Cont2");
                entity.Property(e => e.Cont3).HasMaxLength(50) 
                    .HasColumnName("Cont3");
                entity.Property(e => e.Telefone).HasMaxLength(20) 
                    .HasColumnName("Telefone");
                entity.Property(e => e.Tlmvl).HasMaxLength(20) 
                    .HasColumnName("Tlmvl");
                entity.Property(e => e.Fax).HasMaxLength(20) 
                    .HasColumnName("Fax");
                entity.Property(e => e.Zona).HasMaxLength(50) 
                    .HasColumnName("Zona");
                entity.Property(e => e.Vendedor)
                    .HasColumnType("numeric(16, 0)")
                    .HasColumnName("Vendedor");
                entity.Property(e => e.VendNm).HasMaxLength(100) 
                    .HasColumnName("VendNm");
                entity.Property(e => e.Ncont).HasMaxLength(50) 
                    .HasColumnName("Ncont");
                entity.Property(e => e.Obs).HasColumnName("Obs"); 
                entity.Property(e => e.InstalConc).HasMaxLength(50) 
                    .HasColumnName("InstalConc");
                entity.Property(e => e.ProxVisita) 
                    .HasDefaultValueSql("(CONVERT([datetime],'1900-01-01',0))")
                    .HasColumnType("datetime")
                    .HasColumnName("ProxVisita");
                entity.Property(e => e.UltiActual) 
                    .HasDefaultValueSql("(CONVERT([datetime],'1900-01-01',0))")
                    .HasColumnType("datetime")
                    .HasColumnName("UltiActual");
                entity.Property(e => e.Preco) 
                    .HasColumnType("numeric(16, 3)")
                    .HasColumnName("Preco");
                entity.Property(e => e.Cartao).HasColumnName("Cartao");
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}


//AppDbContext