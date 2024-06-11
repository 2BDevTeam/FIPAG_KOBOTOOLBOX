using System;
using System.Collections.Generic;

namespace FIPAG_KOBOTOOLBOX.Domains.Models
{
    public partial class Bo2
    {
        public string Bo2stamp { get; set; } = null!;
        public string Pscm { get; set; } = null!;
        public decimal Zncm { get; set; }
        public decimal Excm { get; set; }
        public string Ptcm { get; set; } = null!;
        public string Encm { get; set; } = null!;
        public decimal Ntcm { get; set; }
        public string Pscmdesc { get; set; } = null!;
        public string Znregiao { get; set; } = null!;
        public string Excmdesc { get; set; } = null!;
        public string Ptcmdesc { get; set; } = null!;
        public string Encmdesc { get; set; } = null!;
        public bool Ncin { get; set; }
        public bool Ncout { get; set; }
        public bool Usaintra { get; set; }
        public decimal Ettieca { get; set; }
        public decimal Ttieca { get; set; }
        public decimal Mttieca { get; set; }
        public bool Iectisento { get; set; }
        public string Adjbostamp { get; set; } = null!;
        public string Processo { get; set; } = null!;
        public string Subproc { get; set; } = null!;
        public bool Adjudicado { get; set; }
        public bool Orcamento { get; set; }
        public decimal Mcomercial { get; set; }
        public string Dpedidopv { get; set; } = null!;
        public bool Autos { get; set; }
        public decimal Autotipo { get; set; }
        public decimal Autoper { get; set; }
        public decimal Mensaldia { get; set; }
        public decimal Vencimento { get; set; }
        public decimal Autono { get; set; }
        public bool Tbrsemmed { get; set; }
        public decimal Pdtipo { get; set; }
        public string Ousrinis { get; set; } = null!;
        public DateTime Ousrdata { get; set; }
        public string Ousrhora { get; set; } = null!;
        public string Usrinis { get; set; } = null!;
        public DateTime Usrdata { get; set; }
        public string Usrhora { get; set; } = null!;
        public bool Marcada { get; set; }
        public bool Planeamento { get; set; }
        public string Calistamp { get; set; } = null!;
        public string Tipoobra { get; set; } = null!;
        public string Descobra { get; set; } = null!;
        public bool Sujrvp { get; set; }
        public decimal Ttecoval { get; set; }
        public decimal Ettecoval { get; set; }
        public string Ngstamp { get; set; } = null!;
        public string Negocio { get; set; } = null!;
        public string Descnegocio { get; set; } = null!;
        public string Ngstatus { get; set; } = null!;
        public decimal Ttecoval2 { get; set; }
        public decimal Ettecoval2 { get; set; }
        public string Pscmori { get; set; } = null!;
        public string Pscmoridesc { get; set; } = null!;
        public string Autobostamp { get; set; } = null!;
        public string Versaocrono { get; set; } = null!;
        public string Crpstamp { get; set; } = null!;
        public decimal Bo71Bins { get; set; }
        public decimal Bo71Iva { get; set; }
        public decimal Ebo71Bins { get; set; }
        public decimal Ebo71Iva { get; set; }
        public decimal Bo81Bins { get; set; }
        public decimal Bo81Iva { get; set; }
        public decimal Ebo81Bins { get; set; }
        public decimal Ebo81Iva { get; set; }
        public decimal Bo91Bins { get; set; }
        public decimal Bo91Iva { get; set; }
        public decimal Ebo91Bins { get; set; }
        public decimal Ebo91Iva { get; set; }
        public decimal Bo72Bins { get; set; }
        public decimal Bo72Iva { get; set; }
        public decimal Ebo72Bins { get; set; }
        public decimal Ebo72Iva { get; set; }
        public decimal Bo82Bins { get; set; }
        public decimal Bo82Iva { get; set; }
        public decimal Ebo82Bins { get; set; }
        public decimal Ebo82Iva { get; set; }
        public decimal Bo92Bins { get; set; }
        public decimal Bo92Iva { get; set; }
        public decimal Ebo92Bins { get; set; }
        public decimal Ebo92Iva { get; set; }
        public string Alvstamp1 { get; set; } = null!;
        public string Identificacao1 { get; set; } = null!;
        public string Szzstamp1 { get; set; } = null!;
        public string Zona1 { get; set; } = null!;
        public string Alvstamp2 { get; set; } = null!;
        public string Identificacao2 { get; set; } = null!;
        public string Szzstamp2 { get; set; } = null!;
        public string Zona2 { get; set; } = null!;
        public decimal Armazem { get; set; }
        public decimal Ar2mazem { get; set; }
        public string Dprocesso { get; set; } = null!;
        public decimal Diasde { get; set; }
        public decimal Diasate { get; set; }
        public decimal Nopkng { get; set; }
        public string Xpdviatura { get; set; } = null!;
        public DateTime Xpddata { get; set; }
        public string Xpdhora { get; set; } = null!;
        public bool Exportado { get; set; }
        public string Cladrsdesc { get; set; } = null!;
        public string Morada { get; set; } = null!;
        public string Local { get; set; } = null!;
        public string Codpost { get; set; } = null!;
        public string Cladrszona { get; set; } = null!;
        public string Telefone { get; set; } = null!;
        public string Contacto { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Cladrsstamp { get; set; } = null!;
        public string Area { get; set; } = null!;
        public string Nomects { get; set; } = null!;
        public decimal Nocts { get; set; }
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
        public string Tkhsttnr { get; set; } = null!;
        public string Tkhposcstamp { get; set; } = null!;
        public decimal Mtotalciva { get; set; }
        public decimal Etotalciva { get; set; }
        public decimal Totalciva { get; set; }
        public decimal Etotiva { get; set; }
        public decimal Totiva { get; set; }
        public decimal Mtotiva { get; set; }
        public bool Cambiofixo { get; set; }
        public decimal Cambio { get; set; }
        public string Assinatura { get; set; } = null!;
        public string Versaochave { get; set; } = null!;
        public bool Reorcamento { get; set; }
        public string Stamporcamento { get; set; } = null!;
        public decimal Obranoorcamento { get; set; }
        public string Versaorcamento { get; set; } = null!;
        public decimal Custototalorc { get; set; }
        public decimal Ecustototalorc { get; set; }
        public decimal Custototalreorc { get; set; }
        public decimal Ecustototalreorc { get; set; }
        public decimal Custototaldif { get; set; }
        public decimal Ecustototaldif { get; set; }
        public decimal Margemtotalorc { get; set; }
        public decimal Emargemtotalorc { get; set; }
        public decimal Margemtotalreorc { get; set; }
        public decimal Emargemtotalreorc { get; set; }
        public decimal Margemtotaldif { get; set; }
        public decimal Emargemtotaldif { get; set; }
        public decimal Margemorcperc { get; set; }
        public decimal Margemreorcperc { get; set; }
        public string Horasl { get; set; } = null!;
        public string Ndosmanual { get; set; } = null!;
        public decimal Obranomanual { get; set; }
        public string Tiposaft { get; set; } = null!;
        public string Idserie { get; set; } = null!;
        public bool Anulado { get; set; }
        public string Atcodeid { get; set; } = null!;
        public string Carga { get; set; } = null!;
        public string Descar { get; set; } = null!;
        public bool Consfinal { get; set; }
    }
}
