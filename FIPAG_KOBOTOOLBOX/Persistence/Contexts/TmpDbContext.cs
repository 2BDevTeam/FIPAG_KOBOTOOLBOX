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

        public virtual DbSet<Ft2> Ft2 { get; set; } = null!;

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

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
