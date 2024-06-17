using System;
using System.Collections.Generic;

namespace FIPAG_KOBOTOOLBOX.Domains.Models
{
    public partial class Ft2
    {
        public string Ft2stamp { get; set; } = null!;
        public string Modop1 { get; set; } = null!;
        public decimal Epaga1 { get; set; }
        public decimal Paga1 { get; set; }
        public decimal Mpaga1 { get; set; }
        public decimal Ecompaga1 { get; set; }
        public decimal Compaga1 { get; set; }
        public decimal Mcompaga1 { get; set; }
        public string Modop2 { get; set; } = null!;
        public decimal Epaga2 { get; set; }
        public decimal Paga2 { get; set; }
        public decimal Mpaga2 { get; set; }
        public decimal Ecompaga2 { get; set; }
        public decimal Compaga2 { get; set; }
        public decimal Mcompaga2 { get; set; }
        public string Modop3 { get; set; } = null!;
        public decimal Epaga3 { get; set; }
        public decimal Paga3 { get; set; }
        public decimal Mpaga3 { get; set; }
        public decimal Ecompaga3 { get; set; }
        public decimal Compaga3 { get; set; }
        public decimal Mcompaga3 { get; set; }
        public string Modop4 { get; set; } = null!;
        public decimal Epaga4 { get; set; }
        public decimal Paga4 { get; set; }
        public decimal Mpaga4 { get; set; }
        public decimal Ecompaga4 { get; set; }
        public decimal Compaga4 { get; set; }
        public decimal Mcompaga4 { get; set; }
        public string Modop5 { get; set; } = null!;
        public decimal Epaga5 { get; set; }
        public decimal Paga5 { get; set; }
        public decimal Mpaga5 { get; set; }
        public decimal Ecompaga5 { get; set; }
        public decimal Compaga5 { get; set; }
        public decimal Mcompaga5 { get; set; }
        public string Modop6 { get; set; } = null!;
        public decimal Epaga6 { get; set; }
        public decimal Paga6 { get; set; }
        public decimal Mpaga6 { get; set; }
        public decimal Ecompaga6 { get; set; }
        public decimal Compaga6 { get; set; }
        public decimal Mcompaga6 { get; set; }
        public decimal Acerto { get; set; }
        public decimal Troco { get; set; }
        public decimal Vdinheiro { get; set; }
        public decimal Parcial { get; set; }
        public decimal Tparcial { get; set; }
        public decimal Eacerto { get; set; }
        public decimal Etroco { get; set; }
        public decimal Evdinheiro { get; set; }
        public decimal Eparcial { get; set; }
        public decimal Etparcial { get; set; }
        public decimal Macerto { get; set; }
        public decimal Mtroco { get; set; }
        public decimal Mvdinheiro { get; set; }
        public bool Exportado { get; set; }
        public decimal Ettieca { get; set; }
        public decimal Ttieca { get; set; }
        public decimal Mttieca { get; set; }
        public bool Iecaduana { get; set; }
        public string Iecatviagem { get; set; } = null!;
        public string Iecapais { get; set; } = null!;
        public string Iecagarant { get; set; } = null!;
        public string Iecaexport { get; set; } = null!;
        public string Iecatransp { get; set; } = null!;
        public string Iecaobs { get; set; } = null!;
        public string Iecasign { get; set; } = null!;
        public string Iecalocaladuana { get; set; } = null!;
        public string Iecarfnome { get; set; } = null!;
        public string Iecarfmorada { get; set; } = null!;
        public string Iecarfniva { get; set; } = null!;
        public string Niecrf { get; set; } = null!;
        public string Iecacertif { get; set; } = null!;
        public string Iecautnome1 { get; set; } = null!;
        public string Iecautmorada1 { get; set; } = null!;
        public string Iecautnome2 { get; set; } = null!;
        public string Iecautmorada2 { get; set; } = null!;
        public string Iecautnome3 { get; set; } = null!;
        public string Iecautmorada3 { get; set; } = null!;
        public string Iecasignoutros { get; set; } = null!;
        public string Processo { get; set; } = null!;
        public string Subproc { get; set; } = null!;
        public decimal Ttecoval { get; set; }
        public decimal Ettecoval { get; set; }
        public decimal Ttecoval2 { get; set; }
        public decimal Ettecoval2 { get; set; }
        public string Ngstamp { get; set; } = null!;
        public string Negocio { get; set; } = null!;
        public string Descnegocio { get; set; } = null!;
        public string Ngstatus { get; set; } = null!;
        public DateTime Proddata { get; set; }
        public string Prodhora { get; set; } = null!;
        public string Prodinis { get; set; } = null!;
        public DateTime Envdata { get; set; }
        public string Envhora { get; set; } = null!;
        public string Envinis { get; set; } = null!;
        public DateTime Recdata { get; set; }
        public string Rechora { get; set; } = null!;
        public string Anexosstamp { get; set; } = null!;
        public string Vdollocal { get; set; } = null!;
        public decimal Vdcontado { get; set; }
        public string Vdlocal { get; set; } = null!;
        public string Olmoeda { get; set; } = null!;
        public decimal C2no { get; set; }
        public string C2nome { get; set; } = null!;
        public decimal C2estab { get; set; }
        public string C2morada { get; set; } = null!;
        public string C2local { get; set; } = null!;
        public string C2codpost { get; set; } = null!;
        public decimal C2pais { get; set; }
        public string C2ncont { get; set; } = null!;
        public string C2pncont { get; set; } = null!;
        public string Pncont { get; set; } = null!;
        public string Glnft { get; set; } = null!;
        public string Glncl { get; set; } = null!;
        public string Codfornecedor { get; set; } = null!;
        public string Localentrega { get; set; } = null!;
        public DateTime Dataentrega { get; set; }
        public string Horaentrega { get; set; } = null!;
        public string Moradaentrega { get; set; } = null!;
        public string Locallocent { get; set; } = null!;
        public string Codpentrega { get; set; } = null!;
        public string Obsdoc { get; set; } = null!;
        public bool Cladrscl2 { get; set; }
        public string Cladrsdesc { get; set; } = null!;
        public string Morada { get; set; } = null!;
        public string Local { get; set; } = null!;
        public string Codpost { get; set; } = null!;
        public string Cladrszona { get; set; } = null!;
        public string Telefone { get; set; } = null!;
        public string Contacto { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Cladrsstamp { get; set; } = null!;
        public bool Lrest { get; set; }
        public DateTime Orstdata { get; set; }
        public string Orsthora { get; set; } = null!;
        public string Orstinis { get; set; } = null!;
        public DateTime Rstdata { get; set; }
        public string Rsthora { get; set; } = null!;
        public string Rstinis { get; set; } = null!;
        public string Rrsstamp { get; set; } = null!;
        public decimal Tkhtyp { get; set; }
        public string Tkhcarr { get; set; } = null!;
        public string Tkhref { get; set; } = null!;
        public decimal EftaxamtA { get; set; }
        public decimal EftaxamtB { get; set; }
        public decimal FtaxamtA { get; set; }
        public decimal FtaxamtB { get; set; }
        public string Tkhhora { get; set; } = null!;
        public DateTime Tkhdata { get; set; }
        public string Tkhlref { get; set; } = null!;
        public decimal Tkhshf { get; set; }
        public string Tkhopid { get; set; } = null!;
        public decimal Tkhltyp { get; set; }
        public decimal Tkhlpnt { get; set; }
        public string Tkhid { get; set; } = null!;
        public string Tkhpan { get; set; } = null!;
        public decimal Tkhodo { get; set; }
        public decimal Tkhdid { get; set; }
        public string Tkhmpmoney { get; set; } = null!;
        public string Tkhmpround { get; set; } = null!;
        public string Tkhsttnr { get; set; } = null!;
        public string Tkhposcstamp { get; set; } = null!;
        public string Identdecexp { get; set; } = null!;
        public string Posidtpaphc { get; set; } = null!;
        public DateTime Datatpaphc { get; set; }
        public string Horatpaphc { get; set; } = null!;
        public string Numtrtpaphc { get; set; } = null!;
        public string Numprtpaphc { get; set; } = null!;
        public string Typetpaphc { get; set; } = null!;
        public decimal Rectypetpaphc { get; set; }
        public string Nometpaphc { get; set; } = null!;
        public string Pantpaphc { get; set; } = null!;
        public string Dataexptpaphc { get; set; } = null!;
        public string Codauttpaphc { get; set; } = null!;
        public decimal Valortpaphc { get; set; }
        public string Emvdatatpaphc { get; set; } = null!;
        public string Area { get; set; } = null!;
        public bool Ftcctp { get; set; }
        public string Ltstamp { get; set; } = null!;
        public string Ltstamp2 { get; set; } = null!;
        public bool Reexgiva { get; set; }
        public bool Multiint { get; set; }
        public string Motiseimp { get; set; } = null!;
        public string Codmotiseimp { get; set; } = null!;
        public string Csupstamp { get; set; } = null!;
        public string Csupdescricao { get; set; } = null!;
        public string Formapag { get; set; } = null!;
        public bool Prestsrv { get; set; }
        public string Assinatura { get; set; } = null!;
        public string Versaochave { get; set; } = null!;
        public string Descregiva { get; set; } = null!;
        public DateTime Datarect { get; set; }
        public string Horasl { get; set; } = null!;
        public string Entidademb { get; set; } = null!;
        public string Refmb1 { get; set; } = null!;
        public string Refmb2 { get; set; } = null!;
        public string Refmb3 { get; set; } = null!;
        public bool Pagomb { get; set; }
        public DateTime Datapagomb { get; set; }
        public decimal Etotalmb { get; set; }
        public decimal Totalmb { get; set; }
        public bool Cobradovunicre { get; set; }
        public string Ndocmanual { get; set; } = null!;
        public decimal Fnomanual { get; set; }
        public string Tiposaft { get; set; } = null!;
        public bool Operext { get; set; }
        public bool Cobradovpaypal { get; set; }
        public string Atcodeid { get; set; } = null!;
        public decimal UProcesso { get; set; }
        public string UProcdesc { get; set; } = null!;
        public string URefps2 { get; set; } = null!;
        public decimal URecdiv { get; set; }
        public decimal UAcorpag { get; set; }
        public string UTelsede { get; set; } = null!;
        public string UNuitsede { get; set; } = null!;
        public string UNomesede { get; set; } = null!;
        public string UEndsede { get; set; } = null!;
        public DateTime UAutdat { get; set; }
        public string UAutdes { get; set; } = null!;
        public string UAutorn { get; set; } = null!;
        public string UExtrt { get; set; } = null!;
        public string UExtrta { get; set; } = null!;
        public string UExtrtb { get; set; } = null!;
        public string UExtrtc { get; set; } = null!;
        public string UExtrtd { get; set; } = null!;
        public string UExtrte { get; set; } = null!;
        public string UEndereco { get; set; } = null!;
        public decimal UNextsal { get; set; }
        public bool UNentreg { get; set; }
        public bool UReclama { get; set; }
        public bool URespond { get; set; }
        public decimal UBarcode { get; set; }
        public string UNibps2 { get; set; } = null!;
        public decimal UEntps { get; set; }
        public decimal UEntcd { get; set; }
        public string Ousrinis { get; set; } = null!;
        public DateTime Ousrdata { get; set; }
        public string Ousrhora { get; set; } = null!;
        public string Usrinis { get; set; } = null!;
        public DateTime Usrdata { get; set; }
        public string Usrhora { get; set; } = null!;
        public bool Marcada { get; set; }
        public bool UKobosync { get; set; }
    }
}
