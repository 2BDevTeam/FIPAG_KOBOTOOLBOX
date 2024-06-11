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

        public virtual DbSet<Ft3> Ft3 { get; set; } = null!;

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

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
