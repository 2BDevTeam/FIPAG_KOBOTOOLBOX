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
        public virtual DbSet<Cl2> Cl2 { get; set; } = null!;
        public virtual DbSet<Em> Em { get; set; } = null!;
        public virtual DbSet<Bo> Bo { get; set; } = null!;
        public virtual DbSet<Bo2> Bo2 { get; set; } = null!;
        public virtual DbSet<Bo3> Bo3 { get; set; } = null!;
        public virtual DbSet<Ft> Ft { get; set; } = null!;
        public virtual DbSet<Ft3> Ft3 { get; set; } = null!;
        public virtual DbSet<Ft2> Ft2 { get; set; } = null!;

        public virtual DbSet<Us> Us { get; set; } = null!;
        public virtual DbSet<USyncQueue> USyncqueue { get; set; } = null!;


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

                entity.HasIndex(e => e.C2no, "in_ft2_c2no")
                    .HasFillFactor(70);

                entity.HasIndex(e => e.Ltstamp, "in_ft2_ltstamp")
                    .HasFillFactor(70);

                entity.HasIndex(e => e.Ltstamp2, "in_ft2_ltstamp2")
                    .HasFillFactor(70);

                entity.Property(e => e.Ft2stamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("ft2stamp")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.Acerto)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("acerto");

                entity.Property(e => e.Anexosstamp)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("anexosstamp")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Area)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("area")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

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

                entity.Property(e => e.C2codpost)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("c2codpost")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.C2estab)
                    .HasColumnType("numeric(3, 0)")
                    .HasColumnName("c2estab");

                entity.Property(e => e.C2local)
                    .HasMaxLength(43)
                    .IsUnicode(false)
                    .HasColumnName("c2local")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.C2morada)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("c2morada")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.C2ncont)
                    .HasMaxLength(18)
                    .IsUnicode(false)
                    .HasColumnName("c2ncont")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.C2no)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("c2no");

                entity.Property(e => e.C2nome)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("c2nome")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.C2pais)
                    .HasColumnType("numeric(1, 0)")
                    .HasColumnName("c2pais");

                entity.Property(e => e.C2pncont)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("c2pncont")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Cladrscl2).HasColumnName("cladrscl2");

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

                entity.Property(e => e.Cobradovpaypal).HasColumnName("cobradovpaypal");

                entity.Property(e => e.Cobradovunicre).HasColumnName("cobradovunicre");

                entity.Property(e => e.Codauttpaphc)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("codauttpaphc")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Codfornecedor)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("codfornecedor")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Codmotiseimp)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("codmotiseimp")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Codpentrega)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("codpentrega")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Codpost)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("codpost")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Compaga1)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("compaga1");

                entity.Property(e => e.Compaga2)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("compaga2");

                entity.Property(e => e.Compaga3)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("compaga3");

                entity.Property(e => e.Compaga4)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("compaga4");

                entity.Property(e => e.Compaga5)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("compaga5");

                entity.Property(e => e.Compaga6)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("compaga6");

                entity.Property(e => e.Contacto)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("contacto")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Csupdescricao)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("csupdescricao")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Csupstamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("csupstamp")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.Dataentrega)
                    .HasColumnType("datetime")
                    .HasColumnName("dataentrega")
                    .HasDefaultValueSql("(CONVERT([datetime],'19000101',0))");

                entity.Property(e => e.Dataexptpaphc)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("dataexptpaphc")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Datapagomb)
                    .HasColumnType("datetime")
                    .HasColumnName("datapagomb")
                    .HasDefaultValueSql("(CONVERT([datetime],'19000101',0))");

                entity.Property(e => e.Datarect)
                    .HasColumnType("datetime")
                    .HasColumnName("datarect")
                    .HasDefaultValueSql("(CONVERT([datetime],'19000101',0))");

                entity.Property(e => e.Datatpaphc)
                    .HasColumnType("datetime")
                    .HasColumnName("datatpaphc")
                    .HasDefaultValueSql("(CONVERT([datetime],'19000101',0))");

                entity.Property(e => e.Descnegocio)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("descnegocio")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Descregiva)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("descregiva")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Eacerto)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("eacerto");

                entity.Property(e => e.Ecompaga1)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("ecompaga1");

                entity.Property(e => e.Ecompaga2)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("ecompaga2");

                entity.Property(e => e.Ecompaga3)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("ecompaga3");

                entity.Property(e => e.Ecompaga4)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("ecompaga4");

                entity.Property(e => e.Ecompaga5)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("ecompaga5");

                entity.Property(e => e.Ecompaga6)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("ecompaga6");

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

                entity.Property(e => e.Emvdatatpaphc)
                    .HasColumnType("text")
                    .HasColumnName("emvdatatpaphc")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Entidademb)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("entidademb")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Envdata)
                    .HasColumnType("datetime")
                    .HasColumnName("envdata")
                    .HasDefaultValueSql("(CONVERT([datetime],'19000101',0))");

                entity.Property(e => e.Envhora)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("envhora")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Envinis)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("envinis")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Epaga1)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("epaga1");

                entity.Property(e => e.Epaga2)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("epaga2");

                entity.Property(e => e.Epaga3)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("epaga3");

                entity.Property(e => e.Epaga4)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("epaga4");

                entity.Property(e => e.Epaga5)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("epaga5");

                entity.Property(e => e.Epaga6)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("epaga6");

                entity.Property(e => e.Eparcial)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("eparcial");

                entity.Property(e => e.Etotalmb)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("etotalmb");

                entity.Property(e => e.Etparcial)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("etparcial");

                entity.Property(e => e.Etroco)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("etroco");

                entity.Property(e => e.Ettecoval)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("ettecoval");

                entity.Property(e => e.Ettecoval2)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("ettecoval2");

                entity.Property(e => e.Ettieca)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("ettieca");

                entity.Property(e => e.Evdinheiro)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("evdinheiro");

                entity.Property(e => e.Exportado).HasColumnName("exportado");

                entity.Property(e => e.Fnomanual)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("fnomanual");

                entity.Property(e => e.Formapag)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("formapag")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.FtaxamtA)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("ftaxamt_a");

                entity.Property(e => e.FtaxamtB)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("ftaxamt_b");

                entity.Property(e => e.Ftcctp).HasColumnName("ftcctp");

                entity.Property(e => e.Glncl)
                    .HasMaxLength(35)
                    .IsUnicode(false)
                    .HasColumnName("glncl")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Glnft)
                    .HasMaxLength(35)
                    .IsUnicode(false)
                    .HasColumnName("glnft")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Horaentrega)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("horaentrega")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Horasl)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("horasl")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Horatpaphc)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("horatpaphc")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Identdecexp)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("identdecexp")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Iecacertif)
                    .HasMaxLength(240)
                    .IsUnicode(false)
                    .HasColumnName("iecacertif")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Iecaduana).HasColumnName("iecaduana");

                entity.Property(e => e.Iecaexport)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("iecaexport")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Iecagarant)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("iecagarant")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Iecalocaladuana)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("iecalocaladuana")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Iecaobs)
                    .HasMaxLength(240)
                    .IsUnicode(false)
                    .HasColumnName("iecaobs")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Iecapais)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("iecapais")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Iecarfmorada)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("iecarfmorada")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Iecarfniva)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("iecarfniva")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Iecarfnome)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("iecarfnome")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Iecasign)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("iecasign")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Iecasignoutros)
                    .HasMaxLength(240)
                    .IsUnicode(false)
                    .HasColumnName("iecasignoutros")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Iecatransp)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("iecatransp")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Iecatviagem)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("iecatviagem")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Iecautmorada1)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("iecautmorada1")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Iecautmorada2)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("iecautmorada2")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Iecautmorada3)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("iecautmorada3")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Iecautnome1)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("iecautnome1")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Iecautnome2)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("iecautnome2")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Iecautnome3)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("iecautnome3")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Local)
                    .HasMaxLength(43)
                    .IsUnicode(false)
                    .HasColumnName("local")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Localentrega)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("localentrega")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Locallocent)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("locallocent")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Lrest).HasColumnName("lrest");

                entity.Property(e => e.Ltstamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("ltstamp")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.Ltstamp2)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("ltstamp2")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.Macerto)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("macerto");

                entity.Property(e => e.Marcada).HasColumnName("marcada");

                entity.Property(e => e.Mcompaga1)
                    .HasColumnType("numeric(15, 3)")
                    .HasColumnName("mcompaga1");

                entity.Property(e => e.Mcompaga2)
                    .HasColumnType("numeric(15, 3)")
                    .HasColumnName("mcompaga2");

                entity.Property(e => e.Mcompaga3)
                    .HasColumnType("numeric(15, 3)")
                    .HasColumnName("mcompaga3");

                entity.Property(e => e.Mcompaga4)
                    .HasColumnType("numeric(15, 3)")
                    .HasColumnName("mcompaga4");

                entity.Property(e => e.Mcompaga5)
                    .HasColumnType("numeric(15, 3)")
                    .HasColumnName("mcompaga5");

                entity.Property(e => e.Mcompaga6)
                    .HasColumnType("numeric(15, 3)")
                    .HasColumnName("mcompaga6");

                entity.Property(e => e.Modop1)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("modop1")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Modop2)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("modop2")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Modop3)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("modop3")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Modop4)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("modop4")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Modop5)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("modop5")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Modop6)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("modop6")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Morada)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("morada")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Moradaentrega)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("moradaentrega")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Motiseimp)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("motiseimp")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Mpaga1)
                    .HasColumnType("numeric(15, 3)")
                    .HasColumnName("mpaga1");

                entity.Property(e => e.Mpaga2)
                    .HasColumnType("numeric(15, 3)")
                    .HasColumnName("mpaga2");

                entity.Property(e => e.Mpaga3)
                    .HasColumnType("numeric(15, 3)")
                    .HasColumnName("mpaga3");

                entity.Property(e => e.Mpaga4)
                    .HasColumnType("numeric(15, 3)")
                    .HasColumnName("mpaga4");

                entity.Property(e => e.Mpaga5)
                    .HasColumnType("numeric(15, 3)")
                    .HasColumnName("mpaga5");

                entity.Property(e => e.Mpaga6)
                    .HasColumnType("numeric(15, 3)")
                    .HasColumnName("mpaga6");

                entity.Property(e => e.Mtroco)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("mtroco");

                entity.Property(e => e.Mttieca)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("mttieca");

                entity.Property(e => e.Multiint).HasColumnName("multiint");

                entity.Property(e => e.Mvdinheiro)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("mvdinheiro");

                entity.Property(e => e.Ndocmanual)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ndocmanual")
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

                entity.Property(e => e.Niecrf)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("niecrf")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Nometpaphc)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nometpaphc")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Numprtpaphc)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("numprtpaphc")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Numtrtpaphc)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("numtrtpaphc")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Obsdoc)
                    .HasColumnType("text")
                    .HasColumnName("obsdoc")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Olmoeda)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("olmoeda")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Operext).HasColumnName("operext");

                entity.Property(e => e.Orstdata)
                    .HasColumnType("datetime")
                    .HasColumnName("orstdata")
                    .HasDefaultValueSql("(CONVERT([datetime],'19000101',0))");

                entity.Property(e => e.Orsthora)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("orsthora")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Orstinis)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("orstinis")
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

                entity.Property(e => e.Paga1)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("paga1");

                entity.Property(e => e.Paga2)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("paga2");

                entity.Property(e => e.Paga3)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("paga3");

                entity.Property(e => e.Paga4)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("paga4");

                entity.Property(e => e.Paga5)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("paga5");

                entity.Property(e => e.Paga6)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("paga6");

                entity.Property(e => e.Pagomb).HasColumnName("pagomb");

                entity.Property(e => e.Pantpaphc)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("pantpaphc")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Parcial)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("parcial");

                entity.Property(e => e.Pncont)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("pncont")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Posidtpaphc)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("posidtpaphc")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Prestsrv).HasColumnName("prestsrv");

                entity.Property(e => e.Processo)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("processo")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Proddata)
                    .HasColumnType("datetime")
                    .HasColumnName("proddata")
                    .HasDefaultValueSql("(CONVERT([datetime],'19000101',0))");

                entity.Property(e => e.Prodhora)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("prodhora")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Prodinis)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("prodinis")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Recdata)
                    .HasColumnType("datetime")
                    .HasColumnName("recdata")
                    .HasDefaultValueSql("(CONVERT([datetime],'19000101',0))");

                entity.Property(e => e.Rechora)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("rechora")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Rectypetpaphc)
                    .HasColumnType("numeric(1, 0)")
                    .HasColumnName("rectypetpaphc");

                entity.Property(e => e.Reexgiva).HasColumnName("reexgiva");

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

                entity.Property(e => e.Rrsstamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("rrsstamp")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.Rstdata)
                    .HasColumnType("datetime")
                    .HasColumnName("rstdata")
                    .HasDefaultValueSql("(CONVERT([datetime],'19000101',0))");

                entity.Property(e => e.Rsthora)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("rsthora")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Rstinis)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("rstinis")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Subproc)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("subproc")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Telefone)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("telefone")
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

                entity.Property(e => e.Tkhmpmoney)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("tkhmpmoney")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Tkhmpround)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("tkhmpround")
                    .HasDefaultValueSql("('')");

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
                    .HasColumnType("numeric(2, 0)")
                    .HasColumnName("tkhtyp");

                entity.Property(e => e.Totalmb)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("totalmb");

                entity.Property(e => e.Tparcial)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("tparcial");

                entity.Property(e => e.Troco)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("troco");

                entity.Property(e => e.Ttecoval)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("ttecoval");

                entity.Property(e => e.Ttecoval2)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("ttecoval2");

                entity.Property(e => e.Ttieca)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("ttieca");

                entity.Property(e => e.Typetpaphc)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("typetpaphc")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UAcorpag)
                    .HasColumnType("numeric(16, 0)")
                    .HasColumnName("u_acorpag");

                entity.Property(e => e.UAutdat)
                    .HasColumnType("datetime")
                    .HasColumnName("u_autdat")
                    .HasDefaultValueSql("(CONVERT([datetime],'19000101',0))");

                entity.Property(e => e.UAutdes)
                    .HasColumnType("text")
                    .HasColumnName("u_autdes")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UAutorn)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("u_autorn")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UBarcode)
                    .HasColumnType("numeric(16, 0)")
                    .HasColumnName("u_barcode");

                entity.Property(e => e.UEndereco)
                    .HasMaxLength(254)
                    .IsUnicode(false)
                    .HasColumnName("u_endereco")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UEndsede)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("u_endsede")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UEntcd)
                    .HasColumnType("numeric(4, 0)")
                    .HasColumnName("u_entcd");

                entity.Property(e => e.UEntps)
                    .HasColumnType("numeric(5, 0)")
                    .HasColumnName("u_entps");

                entity.Property(e => e.UExtrt)
                    .HasColumnType("text")
                    .HasColumnName("u_extrt")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UExtrta)
                    .HasColumnType("text")
                    .HasColumnName("u_extrta")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UExtrtb)
                    .HasColumnType("text")
                    .HasColumnName("u_extrtb")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UExtrtc)
                    .HasColumnType("text")
                    .HasColumnName("u_extrtc")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UExtrtd)
                    .HasColumnType("text")
                    .HasColumnName("u_extrtd")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UExtrte)
                    .HasColumnType("text")
                    .HasColumnName("u_extrte")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UKobosync).HasColumnName("u_kobosync");

                entity.Property(e => e.UNentreg).HasColumnName("u_nentreg");

                entity.Property(e => e.UNextsal)
                    .HasColumnType("numeric(16, 2)")
                    .HasColumnName("u_nextsal");

                entity.Property(e => e.UNibps2)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("u_nibps2")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UNomesede)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("u_nomesede")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UNuitsede)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("u_nuitsede")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UProcdesc)
                    .HasColumnType("text")
                    .HasColumnName("u_procdesc")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UProcesso)
                    .HasColumnType("numeric(16, 0)")
                    .HasColumnName("u_processo");

                entity.Property(e => e.URecdiv)
                    .HasColumnType("numeric(10, 2)")
                    .HasColumnName("u_recdiv");

                entity.Property(e => e.UReclama).HasColumnName("u_reclama");

                entity.Property(e => e.URefps2)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("u_refps2")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.URespond).HasColumnName("u_respond");

                entity.Property(e => e.UTelsede)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("u_telsede")
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

                entity.Property(e => e.Valortpaphc)
                    .HasColumnType("numeric(10, 2)")
                    .HasColumnName("valortpaphc");

                entity.Property(e => e.Vdcontado)
                    .HasColumnType("numeric(4, 0)")
                    .HasColumnName("vdcontado");

                entity.Property(e => e.Vdinheiro)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("vdinheiro");

                entity.Property(e => e.Vdlocal)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("vdlocal")
                    .HasDefaultValueSql("('C')");

                entity.Property(e => e.Vdollocal)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("vdollocal")
                    .HasDefaultValueSql("('Caixa')");

                entity.Property(e => e.Versaochave)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("versaochave")
                    .HasDefaultValueSql("('')");
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

                entity.HasIndex(e => e.Ncont, "in_cl_ncont")
                    .HasFillFactor(70);

                entity.HasIndex(e => e.No, "in_cl_no")
                    .HasFillFactor(70);

                entity.HasIndex(e => e.Nome, "in_cl_nome")
                    .HasFillFactor(70);

                entity.HasIndex(e => e.Rowid, "in_cl_rowid")
                    .IsUnique()
                    .IsClustered()
                    .HasFillFactor(70);

                entity.HasIndex(e => e.Clstamp, "in_cl_stamp")
                    .IsUnique()
                    .HasFillFactor(70);

                entity.HasIndex(e => e.Vendedor, "in_cl_vendedor")
                    .HasFillFactor(70);

                entity.Property(e => e.No)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("no");

                entity.Property(e => e.Estab)
                    .HasColumnType("numeric(3, 0)")
                    .HasColumnName("estab");

                entity.Property(e => e.Acc)
                    .HasColumnType("numeric(1, 0)")
                    .HasColumnName("acc");

                entity.Property(e => e.Acmfact)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("acmfact");

                entity.Property(e => e.Addd).HasColumnName("addd");

                entity.Property(e => e.Agno)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("agno");

                entity.Property(e => e.Alimite)
                    .HasColumnType("numeric(5, 0)")
                    .HasColumnName("alimite");

                entity.Property(e => e.Area)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("area")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.Autofact).HasColumnName("autofact");

                entity.Property(e => e.UOrigem).HasColumnName("u_origem");

                entity.Property(e => e.Autorizacaoactiva).HasColumnName("autorizacaoactiva");

                entity.Property(e => e.Bic)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("bic")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Bidata)
                    .HasColumnType("datetime")
                    .HasColumnName("bidata")
                    .HasDefaultValueSql("(CONVERT([datetime],'19000101',0))");

                entity.Property(e => e.Bilocal)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("bilocal")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Bino)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("bino")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Bizzaddress)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("bizzaddress")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Bizzproto)
                    .HasColumnType("numeric(1, 0)")
                    .HasColumnName("bizzproto");

                entity.Property(e => e.Blck).HasColumnName("blck");

                entity.Property(e => e.C1email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("c1email")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.C1fax)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("c1fax")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.C1func)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("c1func")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.C1tele)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("c1tele")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.C2email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("c2email")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.C2fax)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("c2fax")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.C2func)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("c2func")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.C2tacto)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("c2tacto")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.C2tele)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("c2tele")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.C3email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("c3email")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.C3fax)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("c3fax")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.C3func)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("c3func")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.C3tacto)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("c3tacto")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.C3tele)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("c3tele")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Cancpos).HasColumnName("cancpos");

                entity.Property(e => e.Carr).HasColumnName("carr");

                entity.Property(e => e.Cass)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("cass")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Ccadmin).HasColumnName("ccadmin");

                entity.Property(e => e.Ccusto)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ccusto")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Classe)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("classe")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Clifactor).HasColumnName("clifactor");

                entity.Property(e => e.Clinica).HasColumnName("clinica");

                entity.Property(e => e.Clivd).HasColumnName("clivd");

                entity.Property(e => e.Clstamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("clstamp")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.Cobemail)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("cobemail")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Cobfax)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("cobfax")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Cobfunc)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("cobfunc")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Cobnao).HasColumnName("cobnao");

                entity.Property(e => e.Cobrador)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("cobrador")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Cobranca)
                    .HasMaxLength(22)
                    .IsUnicode(false)
                    .HasColumnName("cobranca")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Cobtacto)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("cobtacto")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Cobtele)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("cobtele")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Codfornecedor)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("codfornecedor")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Codmotiseimp)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("codmotiseimp")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Codpost)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("codpost")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Codprovincia)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("codprovincia")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Consfinal).HasColumnName("consfinal");

                entity.Property(e => e.Conta)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("conta")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Contaacer)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("contaacer")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Contaainc)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("contaainc")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Contacto)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("contacto")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Contadivinc)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("contadivinc")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Contado)
                    .HasColumnType("numeric(4, 0)")
                    .HasColumnName("contado");

                entity.Property(e => e.Contafac)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("contafac")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Contalet)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("contalet")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Contaletdes)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("contaletdes")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Contaletsac)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("contaletsac")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Contamovinc)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("contamovinc")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Contatit)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("contatit")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Cw).HasColumnName("cw");

                entity.Property(e => e.Datasdd)
                    .HasColumnType("datetime")
                    .HasColumnName("datasdd")
                    .HasDefaultValueSql("(CONVERT([datetime],'19000101',0))");

                entity.Property(e => e.Descarga)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("descarga")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Desccmb)
                    .HasColumnType("numeric(10, 3)")
                    .HasColumnName("desccmb");

                entity.Property(e => e.Descloj)
                    .HasColumnType("numeric(6, 2)")
                    .HasColumnName("descloj");

                entity.Property(e => e.Desconto)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("desconto");

                entity.Property(e => e.Descpp)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("descpp");

                entity.Property(e => e.Descregiva)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("descregiva")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Dformacao).HasColumnName("dformacao");

                entity.Property(e => e.Dfront).HasColumnName("dfront");

                entity.Property(e => e.Diaspag)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("diaspag")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Did).HasColumnName("did");

                entity.Property(e => e.Distrito)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("distrito")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Dqtt).HasColumnName("dqtt");

                entity.Property(e => e.Dqttval)
                    .HasColumnType("numeric(1, 0)")
                    .HasColumnName("dqttval");

                entity.Property(e => e.Dsuporte).HasColumnName("dsuporte");

                entity.Property(e => e.Dteam).HasColumnName("dteam");

                entity.Property(e => e.Eacmfact)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("eacmfact");

                entity.Property(e => e.Eag).HasColumnName("eag");

                entity.Property(e => e.Eancl)
                    .HasMaxLength(35)
                    .IsUnicode(false)
                    .HasColumnName("eancl")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Ecoisento).HasColumnName("ecoisento");

                entity.Property(e => e.Ediexp).HasColumnName("ediexp");

                entity.Property(e => e.Eem).HasColumnName("eem");

                entity.Property(e => e.Efl).HasColumnName("efl");

                entity.Property(e => e.Eid).HasColumnName("eid");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Emno)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("emno");

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

                entity.Property(e => e.Encrpin)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("encrpin")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Eplafond)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("eplafond");

                entity.Property(e => e.Erentval)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("erentval");

                entity.Property(e => e.Esaldlet)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("esaldlet");

                entity.Property(e => e.Esaldo)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("esaldo");

                entity.Property(e => e.Excm)
                    .HasColumnType("numeric(2, 0)")
                    .HasColumnName("excm");

                entity.Property(e => e.Excmdesc)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("excmdesc")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Exporpos).HasColumnName("exporpos");

                entity.Property(e => e.Fax)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("fax")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Filtrast).HasColumnName("filtrast");

                entity.Property(e => e.Flestab)
                    .HasColumnType("numeric(3, 0)")
                    .HasColumnName("flestab");

                entity.Property(e => e.Flno)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("flno");

                entity.Property(e => e.Fref)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("fref")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Ftdatasmr).HasColumnName("ftdatasmr");

                entity.Property(e => e.Ftdiasmr).HasColumnName("ftdiasmr");

                entity.Property(e => e.Ftidbi).HasColumnName("ftidbi");

                entity.Property(e => e.Ftidcob).HasColumnName("ftidcob");

                entity.Property(e => e.Ftidcont).HasColumnName("ftidcont");

                entity.Property(e => e.Ftidcontacto).HasColumnName("ftidcontacto");

                entity.Property(e => e.Ftidnac).HasColumnName("ftidnac");

                entity.Property(e => e.Ftidnome).HasColumnName("ftidnome");

                entity.Property(e => e.Ftidutente).HasColumnName("ftidutente");

                entity.Property(e => e.Ftmrtot).HasColumnName("ftmrtot");

                entity.Property(e => e.Ftndias).HasColumnName("ftndias");

                entity.Property(e => e.Ftnid).HasColumnName("ftnid");

                entity.Property(e => e.Ftumamr).HasColumnName("ftumamr");

                entity.Property(e => e.Fuels)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("fuels")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Gaecstamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("gaecstamp")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.Gaenome)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("gaenome")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.Geramb).HasColumnName("geramb");

                entity.Property(e => e.Glncl)
                    .HasMaxLength(35)
                    .IsUnicode(false)
                    .HasColumnName("glncl")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Iban)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("iban")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Id)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("id")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Idno)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("idno");

                entity.Property(e => e.Iectisento).HasColumnName("iectisento");

                entity.Property(e => e.Imagem)
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("imagem")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Inactivo).HasColumnName("inactivo");

                entity.Property(e => e.Isperson).HasColumnName("isperson");

                entity.Property(e => e.Lang)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("lang")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.Lmlt)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("lmlt");

                entity.Property(e => e.Local)
                    .HasMaxLength(43)
                    .IsUnicode(false)
                    .HasColumnName("local")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Localentrega)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("localentrega")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Ltyp)
                    .HasColumnType("numeric(1, 0)")
                    .HasColumnName("ltyp");

                entity.Property(e => e.Marcada).HasColumnName("marcada");

                entity.Property(e => e.Matric)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("matric")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Mesesnaopag)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("mesesnaopag")
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

                entity.Property(e => e.Motiseimp)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("motiseimp")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Naoencomenda).HasColumnName("naoencomenda");

                entity.Property(e => e.Naomail).HasColumnName("naomail");

                entity.Property(e => e.Naood).HasColumnName("naood");

                entity.Property(e => e.Nascimento)
                    .HasColumnType("datetime")
                    .HasColumnName("nascimento")
                    .HasDefaultValueSql("(CONVERT([datetime],'19000101',0))");

                entity.Property(e => e.Naturalid)
                    .HasMaxLength(17)
                    .IsUnicode(false)
                    .HasColumnName("naturalid")
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

                entity.Property(e => e.Nib)
                    .HasMaxLength(28)
                    .IsUnicode(false)
                    .HasColumnName("nib")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Niec)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("niec")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Nocredit).HasColumnName("nocredit");

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

                entity.Property(e => e.Ntcm)
                    .HasColumnType("numeric(2, 0)")
                    .HasColumnName("ntcm");

                entity.Property(e => e.Numautorizacaosdd)
                    .HasMaxLength(35)
                    .IsUnicode(false)
                    .HasColumnName("numautorizacaosdd")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Numcontrepres)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("numcontrepres")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Numseqaut)
                    .HasColumnType("numeric(3, 0)")
                    .HasColumnName("numseqaut");

                entity.Property(e => e.Obs)
                    .HasMaxLength(240)
                    .IsUnicode(false)
                    .HasColumnName("obs")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Obsdoc)
                    .HasColumnType("text")
                    .HasColumnName("obsdoc")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Odatraso)
                    .HasColumnType("numeric(3, 0)")
                    .HasColumnName("odatraso");

                entity.Property(e => e.Odo).HasColumnName("odo");

                entity.Property(e => e.Ollocal)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ollocal")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Operext).HasColumnName("operext");

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

                entity.Property(e => e.Pagamento)
                    .HasMaxLength(28)
                    .IsUnicode(false)
                    .HasColumnName("pagamento")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Pais)
                    .HasColumnType("numeric(1, 0)")
                    .HasColumnName("pais");

                entity.Property(e => e.Paramr).HasColumnName("paramr");

                entity.Property(e => e.Particular).HasColumnName("particular");

                entity.Property(e => e.Passaporte)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("passaporte")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Pcktsyncdate)
                    .HasColumnType("datetime")
                    .HasColumnName("pcktsyncdate")
                    .HasDefaultValueSql("(CONVERT([datetime],'19000101',0))");

                entity.Property(e => e.Pcktsynctime)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("pcktsynctime")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Pin)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("pin")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Plafond)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("plafond");

                entity.Property(e => e.Pncont)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("pncont")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Preco)
                    .HasColumnType("numeric(1, 0)")
                    .HasColumnName("preco");

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

                entity.Property(e => e.Radicaltipoemp)
                    .HasColumnType("numeric(1, 0)")
                    .HasColumnName("radicaltipoemp");

                entity.Property(e => e.Rbal)
                    .HasColumnType("numeric(1, 0)")
                    .HasColumnName("rbal");

                entity.Property(e => e.Recdocdig).HasColumnName("recdocdig");

                entity.Property(e => e.Refcli)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("refcli")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Rentval)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("rentval");

                entity.Property(e => e.Repl).HasColumnName("repl");

                entity.Property(e => e.Rota)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("rota")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Rowid)
                    .HasColumnName("rowid")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Saldlet)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("saldlet");

                entity.Property(e => e.Saldo)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("saldo");

                entity.Property(e => e.Saldoini)
                    .HasColumnType("numeric(16, 2)")
                    .HasColumnName("saldoini");

                entity.Property(e => e.Saldopa)
                    .HasColumnType("numeric(16, 2)")
                    .HasColumnName("saldopa");

                entity.Property(e => e.Segmento)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("segmento")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Sepacode)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("sepacode")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Shop)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("shop")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Site)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("site")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Statuspda)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("statuspda")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Tabiva)
                    .HasColumnType("numeric(1, 0)")
                    .HasColumnName("tabiva");

                entity.Property(e => e.Taxairs)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("taxairs");

                entity.Property(e => e.Tbprcod)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("tbprcod")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Telefone)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("telefone")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Temcred).HasColumnName("temcred");

                entity.Property(e => e.Temftglob).HasColumnName("temftglob");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("tipo")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Tipodesc)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("tipodesc")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Tlmvl)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("tlmvl")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Tpdesc)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("tpdesc")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Tpstamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("tpstamp")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.Track)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("track")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Tracknr)
                    .HasColumnType("numeric(1, 0)")
                    .HasColumnName("tracknr");

                entity.Property(e => e.Txftdata)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("txftdata")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Txftdias)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("txftdias")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Txftidbi)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("txftidbi")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Txftidcob)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("txftidcob")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Txftidcont)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("txftidcont")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Txftidcontacto)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("txftidcontacto")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Txftidnac)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("txftidnac")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Txftidnome)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("txftidnome")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Txftidutente)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("txftidutente")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Txftmrtot)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("txftmrtot")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Txftndias)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("txftndias")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Txftnid)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("txftnid")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Txirspersonalizada).HasColumnName("txirspersonalizada");

                entity.Property(e => e.UKoboid)
                    .HasColumnType("numeric(15, 0)")
                    .HasColumnName("u_koboid");

                entity.Property(e => e.UKoboOri).HasColumnName("u_koboOri");
                entity.Property(e => e.UKoboSync).HasColumnName("u_kobosync");

                entity.Property(e => e.UNumetica)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("u_numetica");

                entity.Property(e => e.Url)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("url")
                    .HasDefaultValueSql("('')");

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
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Vencimento)
                    .HasColumnType("numeric(3, 0)")
                    .HasColumnName("vencimento");

                entity.Property(e => e.Vendedor)
                    .HasColumnType("numeric(4, 0)")
                    .HasColumnName("vendedor");

                entity.Property(e => e.Vendnm)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("vendnm")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Zncm)
                    .HasColumnType("numeric(2, 0)")
                    .HasColumnName("zncm");

                entity.Property(e => e.Znregiao)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("znregiao")
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

                entity.Property(e => e.Actprinc)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("actprinc")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Adata1)
                    .HasColumnType("datetime")
                    .HasColumnName("adata1")
                    .HasDefaultValueSql("(convert(datetime,'19000101'))");

                entity.Property(e => e.Adata2)
                    .HasColumnType("datetime")
                    .HasColumnName("adata2")
                    .HasDefaultValueSql("(convert(datetime,'19000101'))");

                entity.Property(e => e.Agno)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("agno");

                entity.Property(e => e.Area)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("area")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.Asign1)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("asign1")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Asign2)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("asign2")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Bizzaddress)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("bizzaddress")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Bizzproto)
                    .HasColumnType("numeric(1, 0)")
                    .HasColumnName("bizzproto");

                entity.Property(e => e.Cae)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cae")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Capsocial)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("capsocial");

                entity.Property(e => e.Cartao).HasColumnName("cartao");

                entity.Property(e => e.Clestab)
                    .HasColumnType("numeric(3, 0)")
                    .HasColumnName("clestab");

                entity.Property(e => e.Clno)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("clno");

                entity.Property(e => e.Codig1)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("codig1")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Codig2)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("codig2")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Codig3)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("codig3")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Codpais)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("codpais")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Codpost)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("codpost")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Comprou).HasColumnName("comprou");

                entity.Property(e => e.Con10mail)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("con10mail")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Con10trat)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("con10trat")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Con1mail)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("con1mail")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Con1trat)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("con1trat")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Con2mail)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("con2mail")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Con2trat)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("con2trat")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Con3mail)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("con3mail")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Con3trat)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("con3trat")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Con4mail)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("con4mail")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Con4trat)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("con4trat")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Con5mail)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("con5mail")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Con5trat)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("con5trat")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Con6mail)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("con6mail")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Con6trat)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("con6trat")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Con7mail)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("con7mail")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Con7trat)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("con7trat")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Con8mail)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("con8mail")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Con8trat)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("con8trat")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Con9mail)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("con9mail")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Con9trat)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("con9trat")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Concelho)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("concelho")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Consfinal).HasColumnName("consfinal");

                entity.Property(e => e.Cont10)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("cont10")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Cont10pos)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("cont10pos")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Cont2)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("cont2")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Cont2pos)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("cont2pos")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Cont3)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("cont3")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Cont3pos)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("cont3pos")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Cont4)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("cont4")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Cont4pos)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("cont4pos")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Cont5)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("cont5")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Cont5pos)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("cont5pos")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Cont6)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("cont6")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Cont6pos)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("cont6pos")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Cont7)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("cont7")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Cont7pos)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("cont7pos")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Cont8)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("cont8")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Cont8pos)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("cont8pos")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Cont9)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("cont9")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Cont9pos)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("cont9pos")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Cpost)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("cpost")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Cpostl)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("cpostl")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Cpotencial)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("cpotencial")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Cprioridad)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("cprioridad")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Ctact10tel)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ctact10tel")
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

                entity.Property(e => e.Ctact4tel)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ctact4tel")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Ctact5tel)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ctact5tel")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Ctact6tel)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ctact6tel")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Ctact7tel)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ctact7tel")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Ctact8tel)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ctact8tel")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Ctact9tel)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ctact9tel")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Ctacto)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("ctacto")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Ctactpos)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("ctactpos")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Ctacttel)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ctacttel")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Ctipo)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ctipo")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Dataconst)
                    .HasColumnType("datetime")
                    .HasColumnName("dataconst")
                    .HasDefaultValueSql("(CONVERT([datetime],'19000101',0))");

                entity.Property(e => e.Denominacao)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("denominacao")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Dias)
                    .HasColumnType("numeric(3, 0)")
                    .HasColumnName("dias");

                entity.Property(e => e.Distrito)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("distrito")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Eag).HasColumnName("eag");

                entity.Property(e => e.Eanem)
                    .HasMaxLength(35)
                    .IsUnicode(false)
                    .HasColumnName("eanem")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Ecapsocial)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("ecapsocial");

                entity.Property(e => e.Eclp).HasColumnName("eclp");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Emstamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("emstamp")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.Evolvendas)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("evolvendas");

                entity.Property(e => e.Fax)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("fax")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Fref)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("fref")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Idlinkedin)
                    .HasMaxLength(254)
                    .IsUnicode(false)
                    .HasColumnName("idlinkedin")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Imagem)
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("imagem")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Instalconc)
                    .HasColumnType("text")
                    .HasColumnName("instalconc")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Lang)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("lang")
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

                entity.Property(e => e.Logic1).HasColumnName("logic1");

                entity.Property(e => e.Logic2).HasColumnName("logic2");

                entity.Property(e => e.Longitude)
                    .HasColumnType("numeric(10, 6)")
                    .HasColumnName("longitude");

                entity.Property(e => e.Marcada).HasColumnName("marcada");

                entity.Property(e => e.Monitignios).HasColumnName("monitignios");

                entity.Property(e => e.Morada)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("morada")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Natjurid)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("natjurid")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Ncont)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ncont")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Nfuncionarios)
                    .HasColumnType("numeric(7, 0)")
                    .HasColumnName("nfuncionarios");

                entity.Property(e => e.Noclp)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("noclp");

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

                entity.Property(e => e.Preco)
                    .HasColumnType("numeric(1, 0)")
                    .HasColumnName("preco");

                entity.Property(e => e.Proxaccao)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("proxaccao")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Proxhora)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("proxhora")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Proxvisita)
                    .HasColumnType("datetime")
                    .HasColumnName("proxvisita")
                    .HasDefaultValueSql("(convert(datetime,'19000101'))");

                entity.Property(e => e.Segmento)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("segmento")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Semvis)
                    .IsRequired()
                    .HasColumnName("semvis")
                    .HasDefaultValueSql("(1)");

                entity.Property(e => e.Statuspda)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("statuspda")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Tbprcod)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("tbprcod")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Telefone)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("telefone")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Tlmvl)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("tlmvl")
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

                entity.Property(e => e.Ultiactual)
                    .HasColumnType("datetime")
                    .HasColumnName("ultiactual")
                    .HasDefaultValueSql("(convert(datetime,'19000101'))");

                entity.Property(e => e.Ultihora)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("ultihora")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Ultivisita)
                    .HasColumnType("datetime")
                    .HasColumnName("ultivisita")
                    .HasDefaultValueSql("(convert(datetime,'19000101'))");

                entity.Property(e => e.Url)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("url")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Urlfacebook)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("urlfacebook")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Urllinkedin)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("urllinkedin")
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

                entity.Property(e => e.Volvendas)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("volvendas");

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

                entity.HasIndex(e => new { e.Obranome, e.Fechada, e.Ndos }, "IX_bo_obranome_fechada_ndos_bostamp")
                    .HasFillFactor(70);

                entity.HasIndex(e => new { e.Obranome, e.Ndos, e.Fref }, "IX_bo_obranome_ndos_fref_obrano_totaldeb_no")
                    .HasFillFactor(70);

                entity.HasIndex(e => new { e.Obranome, e.Ndos }, "IX_bo_obranome_ndos_obrano_nome_totaldeb_no_maquina_marca_serie_estab_tabela1_segmento_fref_bo11_iva_bo21_iva_bo31_iva_u_leiact_")
                    .HasFillFactor(70);

                entity.HasIndex(e => new { e.Nmdos, e.Obrano, e.Dataobra, e.Nome, e.No, e.Totaldeb, e.Etotaldeb, e.Bostamp }, "in_bo_bolist")
                    .HasFillFactor(70);

                entity.HasIndex(e => e.Cxstamp, "in_bo_cxstamp")
                    .HasFillFactor(70);

                entity.HasIndex(e => e.Mastamp, "in_bo_mastamp")
                    .HasFillFactor(70);

                entity.HasIndex(e => new { e.Ndos, e.Obrano, e.Boano }, "in_bo_ndos_ano")
                    .HasFillFactor(70);

                entity.HasIndex(e => new { e.Ndos, e.No, e.Obrano }, "in_bo_ndos_no")
                    .HasFillFactor(70);

                entity.HasIndex(e => e.No, "in_bo_no")
                    .HasFillFactor(70);

                entity.HasIndex(e => e.Obrano, "in_bo_obrano")
                    .HasFillFactor(70);

                entity.HasIndex(e => e.Rowid, "in_bo_rowid")
                    .IsUnique()
                    .IsClustered()
                    .HasFillFactor(70);

                entity.HasIndex(e => e.Snstamp, "in_bo_snstamp")
                    .HasFillFactor(70);

                entity.HasIndex(e => e.Ssstamp, "in_bo_ssstamp")
                    .HasFillFactor(70);

                entity.HasIndex(e => e.Bostamp, "in_bo_stamp")
                    .IsUnique()
                    .HasFillFactor(70);

                entity.HasIndex(e => e.Tpstamp, "in_bo_tpstamp")
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

                entity.Property(e => e.Alldescli).HasColumnName("alldescli");

                entity.Property(e => e.Alldesfor).HasColumnName("alldesfor");

                entity.Property(e => e.Aprovado).HasColumnName("aprovado");

                entity.Property(e => e.Bo11Bins)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("bo11_bins");

                entity.Property(e => e.Bo11Iva)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("bo11_iva");

                entity.Property(e => e.Bo12Bins)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("bo12_bins");

                entity.Property(e => e.Bo12Iva)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("bo12_iva");

                entity.Property(e => e.Bo1tvall)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("bo_1tvall");

                entity.Property(e => e.Bo21Bins)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("bo21_bins");

                entity.Property(e => e.Bo21Iva)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("bo21_iva");

                entity.Property(e => e.Bo22Bins)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("bo22_bins");

                entity.Property(e => e.Bo22Iva)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("bo22_iva");

                entity.Property(e => e.Bo2tdesc1)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("bo_2tdesc1");

                entity.Property(e => e.Bo2tdesc2)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("bo_2tdesc2");

                entity.Property(e => e.Bo2tvall)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("bo_2tvall");

                entity.Property(e => e.Bo31Bins)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("bo31_bins");

                entity.Property(e => e.Bo31Iva)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("bo31_iva");

                entity.Property(e => e.Bo32Bins)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("bo32_bins");

                entity.Property(e => e.Bo32Iva)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("bo32_iva");

                entity.Property(e => e.Bo41Bins)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("bo41_bins");

                entity.Property(e => e.Bo41Iva)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("bo41_iva");

                entity.Property(e => e.Bo42Bins)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("bo42_bins");

                entity.Property(e => e.Bo42Iva)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("bo42_iva");

                entity.Property(e => e.Bo51Bins)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("bo51_bins");

                entity.Property(e => e.Bo51Iva)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("bo51_iva");

                entity.Property(e => e.Bo52Bins)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("bo52_bins");

                entity.Property(e => e.Bo52Iva)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("bo52_iva");

                entity.Property(e => e.Bo61Bins)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("bo61_bins");

                entity.Property(e => e.Bo61Iva)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("bo61_iva");

                entity.Property(e => e.Bo62Bins)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("bo62_bins");

                entity.Property(e => e.Bo62Iva)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("bo62_iva");

                entity.Property(e => e.BoTotp1)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("bo_totp1");

                entity.Property(e => e.BoTotp2)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("bo_totp2");

                entity.Property(e => e.Boclose).HasColumnName("boclose");

                entity.Property(e => e.Boid)
                    .HasColumnType("numeric(12, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("boid");

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

                entity.Property(e => e.Cxstamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("cxstamp")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.Cxusername)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("cxusername")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Datafecho)
                    .HasColumnType("datetime")
                    .HasColumnName("datafecho")
                    .HasDefaultValueSql("(convert(datetime,'19000101'))");

                entity.Property(e => e.Datafinal)
                    .HasColumnType("datetime")
                    .HasColumnName("datafinal")
                    .HasDefaultValueSql("(convert(datetime,'19000101'))");

                entity.Property(e => e.Dataobra)
                    .HasColumnType("datetime")
                    .HasColumnName("dataobra")
                    .HasDefaultValueSql("(convert(datetime,'19000101'))");

                entity.Property(e => e.Dataopen)
                    .HasColumnType("datetime")
                    .HasColumnName("dataopen")
                    .HasDefaultValueSql("(convert(datetime,'19000101'))");

                entity.Property(e => e.Descc)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("descc");

                entity.Property(e => e.Dtclose)
                    .HasColumnType("datetime")
                    .HasColumnName("dtclose")
                    .HasDefaultValueSql("(convert(datetime,'19000101'))");

                entity.Property(e => e.Ean)
                    .HasMaxLength(35)
                    .IsUnicode(false)
                    .HasColumnName("ean")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Ebo11Bins)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("ebo11_bins");

                entity.Property(e => e.Ebo11Iva)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("ebo11_iva");

                entity.Property(e => e.Ebo12Bins)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("ebo12_bins");

                entity.Property(e => e.Ebo12Iva)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("ebo12_iva");

                entity.Property(e => e.Ebo1tvall)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("ebo_1tvall");

                entity.Property(e => e.Ebo21Bins)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("ebo21_bins");

                entity.Property(e => e.Ebo21Iva)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("ebo21_iva");

                entity.Property(e => e.Ebo22Bins)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("ebo22_bins");

                entity.Property(e => e.Ebo22Iva)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("ebo22_iva");

                entity.Property(e => e.Ebo2tdes1)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("ebo_2tdes1");

                entity.Property(e => e.Ebo2tdes2)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("ebo_2tdes2");

                entity.Property(e => e.Ebo2tvall)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("ebo_2tvall");

                entity.Property(e => e.Ebo31Bins)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("ebo31_bins");

                entity.Property(e => e.Ebo31Iva)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("ebo31_iva");

                entity.Property(e => e.Ebo32Bins)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("ebo32_bins");

                entity.Property(e => e.Ebo32Iva)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("ebo32_iva");

                entity.Property(e => e.Ebo41Bins)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("ebo41_bins");

                entity.Property(e => e.Ebo41Iva)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("ebo41_iva");

                entity.Property(e => e.Ebo42Bins)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("ebo42_bins");

                entity.Property(e => e.Ebo42Iva)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("ebo42_iva");

                entity.Property(e => e.Ebo51Bins)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("ebo51_bins");

                entity.Property(e => e.Ebo51Iva)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("ebo51_iva");

                entity.Property(e => e.Ebo52Bins)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("ebo52_bins");

                entity.Property(e => e.Ebo52Iva)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("ebo52_iva");

                entity.Property(e => e.Ebo61Bins)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("ebo61_bins");

                entity.Property(e => e.Ebo61Iva)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("ebo61_iva");

                entity.Property(e => e.Ebo62Bins)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("ebo62_bins");

                entity.Property(e => e.Ebo62Iva)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("ebo62_iva");

                entity.Property(e => e.EboTotp1)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("ebo_totp1");

                entity.Property(e => e.EboTotp2)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("ebo_totp2");

                entity.Property(e => e.Ecusto)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("ecusto");

                entity.Property(e => e.Edescc)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("edescc");

                entity.Property(e => e.Edi).HasColumnName("edi");

                entity.Property(e => e.Emconf).HasColumnName("emconf");

                entity.Property(e => e.Esdeb1)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("esdeb1");

                entity.Property(e => e.Esdeb2)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("esdeb2");

                entity.Property(e => e.Esdeb3)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("esdeb3");

                entity.Property(e => e.Esdeb4)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("esdeb4");

                entity.Property(e => e.Estab)
                    .HasColumnType("numeric(3, 0)")
                    .HasColumnName("estab");

                entity.Property(e => e.Estot1)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("estot1");

                entity.Property(e => e.Estot2)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("estot2");

                entity.Property(e => e.Estot3)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("estot3");

                entity.Property(e => e.Estot4)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("estot4");

                entity.Property(e => e.Etotal)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("etotal");

                entity.Property(e => e.Etotaldeb)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("etotaldeb");

                entity.Property(e => e.Evqtt21)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("evqtt21");

                entity.Property(e => e.Evqtt22)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("evqtt22");

                entity.Property(e => e.Evqtt23)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("evqtt23");

                entity.Property(e => e.Evqtt24)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("evqtt24");

                entity.Property(e => e.Fechada).HasColumnName("fechada");

                entity.Property(e => e.Fref)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("fref")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Iecacodisen)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("iecacodisen")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Iemail)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("iemail")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Iiva).HasColumnName("iiva");

                entity.Property(e => e.Impresso).HasColumnName("impresso");

                entity.Property(e => e.Infref).HasColumnName("infref");

                entity.Property(e => e.Inome)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("inome")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Itotais).HasColumnName("itotais");

                entity.Property(e => e.Itotaisiva).HasColumnName("itotaisiva");

                entity.Property(e => e.Iunit).HasColumnName("iunit");

                entity.Property(e => e.Iunitiva).HasColumnName("iunitiva");

                entity.Property(e => e.Lang)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("lang")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.Lifref).HasColumnName("lifref");

                entity.Property(e => e.Local)
                    .HasMaxLength(43)
                    .IsUnicode(false)
                    .HasColumnName("local")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Logi1).HasColumnName("logi1");

                entity.Property(e => e.Logi2).HasColumnName("logi2");

                entity.Property(e => e.Logi3).HasColumnName("logi3");

                entity.Property(e => e.Logi4).HasColumnName("logi4");

                entity.Property(e => e.Logi5).HasColumnName("logi5");

                entity.Property(e => e.Logi6).HasColumnName("logi6");

                entity.Property(e => e.Logi7).HasColumnName("logi7");

                entity.Property(e => e.Logi8).HasColumnName("logi8");

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

                entity.Property(e => e.Marcada).HasColumnName("marcada");

                entity.Property(e => e.Mastamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("mastamp")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

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

                entity.Property(e => e.Moetotal)
                    .HasColumnType("numeric(13, 3)")
                    .HasColumnName("moetotal");

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

                entity.Property(e => e.Nomquina)
                    .HasColumnType("numeric(6, 0)")
                    .HasColumnName("nomquina");

                entity.Property(e => e.Nopat)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("nopat");

                entity.Property(e => e.Obranome)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("obranome")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Obs)
                    .HasMaxLength(67)
                    .IsUnicode(false)
                    .HasColumnName("obs")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Obstab2)
                    .HasColumnType("text")
                    .HasColumnName("obstab2")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Ocupacao)
                    .HasColumnType("numeric(1, 0)")
                    .HasColumnName("ocupacao");

                entity.Property(e => e.Origem)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("origem")
                    .HasDefaultValueSql("('BO')");

                entity.Property(e => e.Orinopat)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("orinopat");

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

                entity.Property(e => e.Pastamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("pastamp")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.Period)
                    .HasColumnType("numeric(4, 0)")
                    .HasColumnName("period");

                entity.Property(e => e.Pno)
                    .HasColumnType("numeric(3, 0)")
                    .HasColumnName("pno");

                entity.Property(e => e.Pnome)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("pnome")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Quarto)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("quarto")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Rowid)
                    .HasColumnName("rowid")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Sdeb1)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("sdeb1");

                entity.Property(e => e.Sdeb2)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("sdeb2");

                entity.Property(e => e.Sdeb3)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("sdeb3");

                entity.Property(e => e.Sdeb4)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("sdeb4");

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

                entity.Property(e => e.Series)
                    .HasColumnType("text")
                    .HasColumnName("series")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Series2)
                    .HasColumnType("text")
                    .HasColumnName("series2")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Site)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("site")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Situacao)
                    .HasColumnType("numeric(1, 0)")
                    .HasColumnName("situacao");

                entity.Property(e => e.Smoe1)
                    .HasColumnType("numeric(13, 3)")
                    .HasColumnName("smoe1");

                entity.Property(e => e.Smoe2)
                    .HasColumnType("numeric(13, 3)")
                    .HasColumnName("smoe2");

                entity.Property(e => e.Smoe3)
                    .HasColumnType("numeric(13, 3)")
                    .HasColumnName("smoe3");

                entity.Property(e => e.Smoe4)
                    .HasColumnType("numeric(13, 3)")
                    .HasColumnName("smoe4");

                entity.Property(e => e.Snstamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("snstamp")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.Sqtt11)
                    .HasColumnType("numeric(13, 3)")
                    .HasColumnName("sqtt11");

                entity.Property(e => e.Sqtt12)
                    .HasColumnType("numeric(13, 2)")
                    .HasColumnName("sqtt12");

                entity.Property(e => e.Sqtt13)
                    .HasColumnType("numeric(13, 3)")
                    .HasColumnName("sqtt13");

                entity.Property(e => e.Sqtt14)
                    .HasColumnType("numeric(13, 3)")
                    .HasColumnName("sqtt14");

                entity.Property(e => e.Sqtt21)
                    .HasColumnType("numeric(13, 3)")
                    .HasColumnName("sqtt21");

                entity.Property(e => e.Sqtt22)
                    .HasColumnType("numeric(13, 3)")
                    .HasColumnName("sqtt22");

                entity.Property(e => e.Sqtt23)
                    .HasColumnType("numeric(13, 3)")
                    .HasColumnName("sqtt23");

                entity.Property(e => e.Sqtt24)
                    .HasColumnType("numeric(13, 3)")
                    .HasColumnName("sqtt24");

                entity.Property(e => e.Ssstamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("ssstamp")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.Ssusername)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("ssusername")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Statuspda)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("statuspda")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Stot1)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("stot1");

                entity.Property(e => e.Stot2)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("stot2");

                entity.Property(e => e.Stot3)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("stot3");

                entity.Property(e => e.Stot4)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("stot4");

                entity.Property(e => e.Tabela1)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("tabela1")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Tabela2)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("tabela2")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Tecnico)
                    .HasColumnType("numeric(4, 0)")
                    .HasColumnName("tecnico");

                entity.Property(e => e.Tecnnm)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("tecnnm")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("tipo")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Total)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("total");

                entity.Property(e => e.Totaldeb)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("totaldeb");

                entity.Property(e => e.Tpdesc)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("tpdesc")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Tpstamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("tpstamp")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

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

                entity.Property(e => e.UAnomab)
                    .HasColumnType("numeric(3, 0)")
                    .HasColumnName("u_anomab");

                entity.Property(e => e.UCalibre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("u_calibre")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UCodleit)
                    .HasColumnType("numeric(5, 0)")
                    .HasColumnName("u_codleit");

                entity.Property(e => e.UCodleitb)
                    .HasColumnType("numeric(5, 0)")
                    .HasColumnName("u_codleitb");

                entity.Property(e => e.UContador)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("u_contador")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UDataleib)
                    .HasColumnType("datetime")
                    .HasColumnName("u_dataleib")
                    .HasDefaultValueSql("(convert(datetime,'19000101'))");

                entity.Property(e => e.UDiametro)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("u_diametro")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UEndereco)
                    .HasMaxLength(254)
                    .IsUnicode(false)
                    .HasColumnName("u_endereco")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UFact)
                    .HasColumnType("numeric(16, 0)")
                    .HasColumnName("u_fact");

                entity.Property(e => e.UInspec)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("u_inspec")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ULeiact)
                    .HasColumnType("numeric(16, 0)")
                    .HasColumnName("u_leiact");

                entity.Property(e => e.ULeiactb)
                    .HasColumnType("numeric(16, 0)")
                    .HasColumnName("u_leiactb");

                entity.Property(e => e.ULeiant)
                    .HasColumnType("numeric(16, 0)")
                    .HasColumnName("u_leiant");

                entity.Property(e => e.UMemo)
                    .HasColumnType("text")
                    .HasColumnName("u_memo")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UNomleit)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("u_nomleit")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UNomleitb)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("u_nomleitb")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UOrdem)
                    .HasColumnType("numeric(12, 0)")
                    .HasColumnName("u_ordem");

                entity.Property(e => e.UPeriodo)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("u_periodo")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UProft)
                    .HasColumnType("numeric(16, 0)")
                    .HasColumnName("u_proft");

                entity.Property(e => e.UReal)
                    .HasColumnType("numeric(16, 0)")
                    .HasColumnName("u_real");

                entity.Property(e => e.URealb)
                    .HasColumnType("numeric(16, 0)")
                    .HasColumnName("u_realb");

                entity.Property(e => e.Ultfact)
                    .HasColumnType("datetime")
                    .HasColumnName("ultfact")
                    .HasDefaultValueSql("(convert(datetime,'19000101'))");

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

                entity.Property(e => e.Vendedor)
                    .HasColumnType("numeric(4, 0)")
                    .HasColumnName("vendedor");

                entity.Property(e => e.Vendnm)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("vendnm")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Vqtt21)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("vqtt21");

                entity.Property(e => e.Vqtt22)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("vqtt22");

                entity.Property(e => e.Vqtt23)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("vqtt23");

                entity.Property(e => e.Vqtt24)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("vqtt24");

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

                entity.HasIndex(e => e.Arstamp, "in_ft_arstamp")
                    .HasFillFactor(70);

                entity.HasIndex(e => e.Cxstamp, "in_ft_cxstamp")
                    .HasFillFactor(70);

                entity.HasIndex(e => e.Dostamp, "in_ft_dostamp")
                    .HasFillFactor(70);

                entity.HasIndex(e => e.Estab, "in_ft_estab")
                    .HasFillFactor(70);

                entity.HasIndex(e => e.Fdata, "in_ft_fdata")
                    .HasFillFactor(70);

                entity.HasIndex(e => e.Nome, "in_ft_ftlist")
                    .HasFillFactor(70);

                entity.HasIndex(e => e.Lpstamp, "in_ft_lpstamp")
                    .HasFillFactor(70);

                entity.HasIndex(e => e.Lrstamp, "in_ft_lrstamp")
                    .HasFillFactor(70);

                entity.HasIndex(e => e.Mhstamp, "in_ft_mhstamp")
                    .HasFillFactor(70);

                entity.HasIndex(e => e.No, "in_ft_no")
                    .HasFillFactor(70);

                entity.HasIndex(e => e.Nome, "in_ft_nome")
                    .HasFillFactor(70);

                entity.HasIndex(e => e.Rowid, "in_ft_rowid")
                    .IsUnique()
                    .IsClustered()
                    .HasFillFactor(70);

                entity.HasIndex(e => e.Rpclstamp, "in_ft_rpclstamp")
                    .HasFillFactor(70);

                entity.HasIndex(e => new { e.Site, e.Pnome, e.Cxstamp, e.Anulado, e.Fno, e.Tipodoc }, "in_ft_sangrias")
                    .HasFillFactor(70);

                entity.HasIndex(e => e.Snstamp, "in_ft_snstamp")
                    .HasFillFactor(70);

                entity.HasIndex(e => e.Ssstamp, "in_ft_ssstamp")
                    .HasFillFactor(70);

                entity.HasIndex(e => e.Ftstamp, "in_ft_stamp")
                    .IsUnique()
                    .HasFillFactor(70);

                entity.HasIndex(e => e.Tipodoc, "in_ft_tipo_anula_ft_ndoc")
                    .HasFillFactor(70);

                entity.HasIndex(e => e.Tipodoc, "in_ft_tipodoc")
                    .HasFillFactor(70);

                entity.HasIndex(e => e.Tpstamp, "in_ft_tpstamp")
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

                entity.Property(e => e.Aprovado).HasColumnName("aprovado");

                entity.Property(e => e.Arno)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("arno");

                entity.Property(e => e.Arstamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("arstamp")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.Bidata)
                    .HasColumnType("datetime")
                    .HasColumnName("bidata")
                    .HasDefaultValueSql("(convert(datetime,'19000101'))");

                entity.Property(e => e.Bilocal)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("bilocal")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Bino)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("bino")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Cambio)
                    .HasColumnType("numeric(20, 12)")
                    .HasColumnName("cambio");

                entity.Property(e => e.Cambiofixo).HasColumnName("cambiofixo");

                entity.Property(e => e.Carga)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("carga")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Ccusto)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ccusto")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Cdata)
                    .HasColumnType("datetime")
                    .HasColumnName("cdata")
                    .HasDefaultValueSql("(convert(datetime,'19000101'))");

                entity.Property(e => e.Chdata)
                    .HasColumnType("datetime")
                    .HasColumnName("chdata")
                    .HasDefaultValueSql("(convert(datetime,'19000101'))");

                entity.Property(e => e.Cheque).HasColumnName("cheque");

                entity.Property(e => e.Chmoeda)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("chmoeda")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Chora)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("chora")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Chtmoeda)
                    .HasColumnType("numeric(15, 3)")
                    .HasColumnName("chtmoeda");

                entity.Property(e => e.Chtotal)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("chtotal");

                entity.Property(e => e.Classe)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("classe")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Clbanco)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("clbanco")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Clcheque)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("clcheque")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Cobrado).HasColumnName("cobrado");

                entity.Property(e => e.Cobrador)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("cobrador")
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

                entity.Property(e => e.Cxstamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("cxstamp")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.Cxusername)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("cxusername")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Debreg)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("debreg");

                entity.Property(e => e.Debregm)
                    .HasColumnType("numeric(15, 3)")
                    .HasColumnName("debregm");

                entity.Property(e => e.Descar)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("descar")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Descc)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("descc");

                entity.Property(e => e.Descm)
                    .HasColumnType("numeric(13, 3)")
                    .HasColumnName("descm");

                entity.Property(e => e.Diaplano)
                    .HasColumnType("numeric(2, 0)")
                    .HasColumnName("diaplano");

                entity.Property(e => e.Diferido)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("diferido");

                entity.Property(e => e.Dilnoplano)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("dilnoplano");

                entity.Property(e => e.Dinoplano)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("dinoplano")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Dostamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("dostamp")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.Dplano)
                    .HasColumnType("datetime")
                    .HasColumnName("dplano")
                    .HasDefaultValueSql("(convert(datetime,'19000101'))");

                entity.Property(e => e.Eancl)
                    .HasMaxLength(35)
                    .IsUnicode(false)
                    .HasColumnName("eancl")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Eanft)
                    .HasMaxLength(35)
                    .IsUnicode(false)
                    .HasColumnName("eanft")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Echtotal)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("echtotal");

                entity.Property(e => e.Ecusto)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("ecusto");

                entity.Property(e => e.Edebreg)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("edebreg");

                entity.Property(e => e.Edescc)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("edescc");

                entity.Property(e => e.Ediferido)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("ediferido");

                entity.Property(e => e.Efinv)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("efinv");

                entity.Property(e => e.Eivain1)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("eivain1");

                entity.Property(e => e.Eivain2)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("eivain2");

                entity.Property(e => e.Eivain3)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("eivain3");

                entity.Property(e => e.Eivain4)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("eivain4");

                entity.Property(e => e.Eivain5)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("eivain5");

                entity.Property(e => e.Eivain6)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("eivain6");

                entity.Property(e => e.Eivain7)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("eivain7");

                entity.Property(e => e.Eivain8)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("eivain8");

                entity.Property(e => e.Eivain9)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("eivain9");

                entity.Property(e => e.Eivav1)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("eivav1");

                entity.Property(e => e.Eivav2)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("eivav2");

                entity.Property(e => e.Eivav3)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("eivav3");

                entity.Property(e => e.Eivav4)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("eivav4");

                entity.Property(e => e.Eivav5)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("eivav5");

                entity.Property(e => e.Eivav6)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("eivav6");

                entity.Property(e => e.Eivav7)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("eivav7");

                entity.Property(e => e.Eivav8)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("eivav8");

                entity.Property(e => e.Eivav9)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("eivav9");

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

                entity.Property(e => e.Encomenda)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("encomenda")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Eportes)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("eportes");

                entity.Property(e => e.Erdtotal)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("erdtotal");

                entity.Property(e => e.Estab)
                    .HasColumnType("numeric(3, 0)")
                    .HasColumnName("estab");

                entity.Property(e => e.Etot1)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("etot1");

                entity.Property(e => e.Etot2)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("etot2");

                entity.Property(e => e.Etot3)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("etot3");

                entity.Property(e => e.Etot4)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("etot4");

                entity.Property(e => e.Etotal)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("etotal");

                entity.Property(e => e.Ettiliq)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("ettiliq");

                entity.Property(e => e.Ettiva)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("ettiva");

                entity.Property(e => e.Evirs)
                    .HasColumnType("numeric(19, 6)")
                    .HasColumnName("evirs");

                entity.Property(e => e.Excm)
                    .HasColumnType("numeric(2, 0)")
                    .HasColumnName("excm");

                entity.Property(e => e.Excmdesc)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("excmdesc")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Expedicao)
                    .HasMaxLength(28)
                    .IsUnicode(false)
                    .HasColumnName("expedicao")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Facturada).HasColumnName("facturada");

                entity.Property(e => e.Fdata)
                    .HasColumnType("datetime")
                    .HasColumnName("fdata")
                    .HasDefaultValueSql("(convert(datetime,'19000101'))");

                entity.Property(e => e.Fin)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("fin");

                entity.Property(e => e.Final)
                    .HasMaxLength(75)
                    .IsUnicode(false)
                    .HasColumnName("final")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Finv)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("finv");

                entity.Property(e => e.Finvm)
                    .HasColumnType("numeric(13, 3)")
                    .HasColumnName("finvm");

                entity.Property(e => e.Fnoft)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("fnoft");

                entity.Property(e => e.Fref)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("fref")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Ftid)
                    .HasColumnType("numeric(12, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ftid");

                entity.Property(e => e.Ftpos)
                    .HasColumnType("numeric(2, 0)")
                    .HasColumnName("ftpos");

                entity.Property(e => e.Ftstamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("ftstamp")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.Iecacodisen)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("iecacodisen")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Iecadoccod)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("iecadoccod")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Iectisento).HasColumnName("iectisento");

                entity.Property(e => e.Impresso).HasColumnName("impresso");

                entity.Property(e => e.Intid)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("intid")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Introfin).HasColumnName("introfin");

                entity.Property(e => e.Ivain1)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("ivain1");

                entity.Property(e => e.Ivain2)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("ivain2");

                entity.Property(e => e.Ivain3)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("ivain3");

                entity.Property(e => e.Ivain4)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("ivain4");

                entity.Property(e => e.Ivain5)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("ivain5");

                entity.Property(e => e.Ivain6)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("ivain6");

                entity.Property(e => e.Ivain7)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("ivain7");

                entity.Property(e => e.Ivain8)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("ivain8");

                entity.Property(e => e.Ivain9)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("ivain9");

                entity.Property(e => e.Ivamin1)
                    .HasColumnType("numeric(16, 3)")
                    .HasColumnName("ivamin1");

                entity.Property(e => e.Ivamin2)
                    .HasColumnType("numeric(16, 3)")
                    .HasColumnName("ivamin2");

                entity.Property(e => e.Ivamin3)
                    .HasColumnType("numeric(16, 3)")
                    .HasColumnName("ivamin3");

                entity.Property(e => e.Ivamin4)
                    .HasColumnType("numeric(16, 3)")
                    .HasColumnName("ivamin4");

                entity.Property(e => e.Ivamin5)
                    .HasColumnType("numeric(16, 3)")
                    .HasColumnName("ivamin5");

                entity.Property(e => e.Ivamin6)
                    .HasColumnType("numeric(16, 3)")
                    .HasColumnName("ivamin6");

                entity.Property(e => e.Ivamin7)
                    .HasColumnType("numeric(16, 3)")
                    .HasColumnName("ivamin7");

                entity.Property(e => e.Ivamin8)
                    .HasColumnType("numeric(16, 3)")
                    .HasColumnName("ivamin8");

                entity.Property(e => e.Ivamin9)
                    .HasColumnType("numeric(16, 3)")
                    .HasColumnName("ivamin9");

                entity.Property(e => e.Ivamv1)
                    .HasColumnType("numeric(15, 3)")
                    .HasColumnName("ivamv1");

                entity.Property(e => e.Ivamv2)
                    .HasColumnType("numeric(15, 3)")
                    .HasColumnName("ivamv2");

                entity.Property(e => e.Ivamv3)
                    .HasColumnType("numeric(15, 3)")
                    .HasColumnName("ivamv3");

                entity.Property(e => e.Ivamv4)
                    .HasColumnType("numeric(15, 3)")
                    .HasColumnName("ivamv4");

                entity.Property(e => e.Ivamv5)
                    .HasColumnType("numeric(15, 3)")
                    .HasColumnName("ivamv5");

                entity.Property(e => e.Ivamv6)
                    .HasColumnType("numeric(15, 3)")
                    .HasColumnName("ivamv6");

                entity.Property(e => e.Ivamv7)
                    .HasColumnType("numeric(15, 3)")
                    .HasColumnName("ivamv7");

                entity.Property(e => e.Ivamv8)
                    .HasColumnType("numeric(15, 3)")
                    .HasColumnName("ivamv8");

                entity.Property(e => e.Ivamv9)
                    .HasColumnType("numeric(15, 3)")
                    .HasColumnName("ivamv9");

                entity.Property(e => e.Ivatx1)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("ivatx1");

                entity.Property(e => e.Ivatx2)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("ivatx2");

                entity.Property(e => e.Ivatx3)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("ivatx3");

                entity.Property(e => e.Ivatx4)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("ivatx4");

                entity.Property(e => e.Ivatx5)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("ivatx5");

                entity.Property(e => e.Ivatx6)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("ivatx6");

                entity.Property(e => e.Ivatx7)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("ivatx7");

                entity.Property(e => e.Ivatx8)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("ivatx8");

                entity.Property(e => e.Ivatx9)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("ivatx9");

                entity.Property(e => e.Ivav1)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("ivav1");

                entity.Property(e => e.Ivav2)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("ivav2");

                entity.Property(e => e.Ivav3)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("ivav3");

                entity.Property(e => e.Ivav4)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("ivav4");

                entity.Property(e => e.Ivav5)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("ivav5");

                entity.Property(e => e.Ivav6)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("ivav6");

                entity.Property(e => e.Ivav7)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("ivav7");

                entity.Property(e => e.Ivav8)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("ivav8");

                entity.Property(e => e.Ivav9)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("ivav9");

                entity.Property(e => e.Jaexpedi).HasColumnName("jaexpedi");

                entity.Property(e => e.Lang)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("lang")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.Lifref).HasColumnName("lifref");

                entity.Property(e => e.Local)
                    .HasMaxLength(43)
                    .IsUnicode(false)
                    .HasColumnName("local")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Lpstamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("lpstamp")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.Lrstamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("lrstamp")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.Marcada).HasColumnName("marcada");

                entity.Property(e => e.Matricula)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("matricula")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Meiost).HasColumnName("meiost");

                entity.Property(e => e.Memissao)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("memissao")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Mhstamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("mhstamp")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

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

                entity.Property(e => e.Multi).HasColumnName("multi");

                entity.Property(e => e.Ncin).HasColumnName("ncin");

                entity.Property(e => e.Ncont)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ncont")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Ncout).HasColumnName("ncout");

                entity.Property(e => e.Ncusto)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ncusto")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Niec)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("niec")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Nmdoc)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("nmdoc")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Nmdocft)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("nmdocft")
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

                entity.Property(e => e.Nprotri).HasColumnName("nprotri");

                entity.Property(e => e.Ntcm)
                    .HasColumnType("numeric(2, 0)")
                    .HasColumnName("ntcm");

                entity.Property(e => e.Optri).HasColumnName("optri");

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

                entity.Property(e => e.Pagamento)
                    .HasMaxLength(28)
                    .IsUnicode(false)
                    .HasColumnName("pagamento")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Pais)
                    .HasColumnType("numeric(1, 0)")
                    .HasColumnName("pais");

                entity.Property(e => e.Pdata)
                    .HasColumnType("datetime")
                    .HasColumnName("pdata")
                    .HasDefaultValueSql("(convert(datetime,'19000101'))");

                entity.Property(e => e.Peso)
                    .HasColumnType("numeric(14, 3)")
                    .HasColumnName("peso");

                entity.Property(e => e.Plano).HasColumnName("plano");

                entity.Property(e => e.Planoonline).HasColumnName("planoonline");

                entity.Property(e => e.Pno)
                    .HasColumnType("numeric(3, 0)")
                    .HasColumnName("pno");

                entity.Property(e => e.Pnome)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("pnome")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Portes)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("portes");

                entity.Property(e => e.Procomss).HasColumnName("procomss");

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

                entity.Property(e => e.Ptcm)
                    .HasMaxLength(43)
                    .IsUnicode(false)
                    .HasColumnName("ptcm")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Ptcmdesc)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("ptcmdesc")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Qtt1)
                    .HasColumnType("numeric(16, 3)")
                    .HasColumnName("qtt1");

                entity.Property(e => e.Qtt2)
                    .HasColumnType("numeric(16, 3)")
                    .HasColumnName("qtt2");

                entity.Property(e => e.Qtt3)
                    .HasColumnType("numeric(16, 3)")
                    .HasColumnName("qtt3");

                entity.Property(e => e.Qtt4)
                    .HasColumnType("numeric(16, 3)")
                    .HasColumnName("qtt4");

                entity.Property(e => e.Rdtotal)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("rdtotal");

                entity.Property(e => e.Rdtotalm)
                    .HasColumnType("numeric(15, 3)")
                    .HasColumnName("rdtotalm");

                entity.Property(e => e.Rota)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("rota")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Rowid)
                    .HasColumnName("rowid")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Rpcldfim)
                    .HasColumnType("datetime")
                    .HasColumnName("rpcldfim")
                    .HasDefaultValueSql("(convert(datetime,'19000101'))");

                entity.Property(e => e.Rpcldini)
                    .HasColumnType("datetime")
                    .HasColumnName("rpcldini")
                    .HasDefaultValueSql("(convert(datetime,'19000101'))");

                entity.Property(e => e.Rpclnome)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("rpclnome")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Rpclstamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("rpclstamp")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Saida)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("saida")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Segmento)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("segmento")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Series)
                    .HasColumnType("text")
                    .HasColumnName("series")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Series2)
                    .HasColumnType("text")
                    .HasColumnName("series2")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Site)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("site")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Snstamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("snstamp")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.Ssstamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("ssstamp")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.Ssusername)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("ssusername")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Telefone)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("telefone")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("tipo")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Tipodoc)
                    .HasColumnType("numeric(1, 0)")
                    .HasColumnName("tipodoc");

                entity.Property(e => e.Tmiliq)
                    .HasColumnType("numeric(15, 3)")
                    .HasColumnName("tmiliq");

                entity.Property(e => e.Tmiva)
                    .HasColumnType("numeric(15, 3)")
                    .HasColumnName("tmiva");

                entity.Property(e => e.Tot1)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("tot1");

                entity.Property(e => e.Tot2)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("tot2");

                entity.Property(e => e.Tot3)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("tot3");

                entity.Property(e => e.Tot4)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("tot4");

                entity.Property(e => e.Total)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("total");

                entity.Property(e => e.Totalmoeda)
                    .HasColumnType("numeric(15, 3)")
                    .HasColumnName("totalmoeda");

                entity.Property(e => e.Totqtt)
                    .HasColumnType("numeric(15, 3)")
                    .HasColumnName("totqtt");

                entity.Property(e => e.Tpdesc)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("tpdesc")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Tpstamp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("tpstamp")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();

                entity.Property(e => e.Tptit)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("tptit")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Ttiliq)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("ttiliq");

                entity.Property(e => e.Ttiva)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("ttiva");

                entity.Property(e => e.UOrdem)
                    .HasColumnType("numeric(12, 0)")
                    .HasColumnName("u_ordem");

                entity.Property(e => e.Usaintra).HasColumnName("usaintra");

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

                entity.Property(e => e.Valorm2)
                    .HasColumnType("numeric(15, 2)")
                    .HasColumnName("valorm2");

                entity.Property(e => e.Vendedor)
                    .HasColumnType("numeric(4, 0)")
                    .HasColumnName("vendedor");

                entity.Property(e => e.Vendnm)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("vendnm")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Virs)
                    .HasColumnType("numeric(18, 5)")
                    .HasColumnName("virs");

                entity.Property(e => e.Zncm)
                    .HasColumnType("numeric(2, 0)")
                    .HasColumnName("zncm");

                entity.Property(e => e.Znregiao)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("znregiao")
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


//AppDbContext