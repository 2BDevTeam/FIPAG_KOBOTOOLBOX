using System;
using System.Collections.Generic;

namespace FIPAG_KOBOTOOLBOX.Domains.Models
{
    public partial class Bo3
    {
        public string Bo3stamp { get; set; } = null!;
        public string Codpais { get; set; } = null!;
        public string Descpais { get; set; } = null!;
        public string Motanul { get; set; } = null!;
        public string Documentnumberori { get; set; } = null!;
        public string Distrito { get; set; } = null!;
        public DateTime Taxpointdt { get; set; }
        public string Motorista { get; set; } = null!;
        public string Codpromo { get; set; } = null!;
        public bool Cobradovpaypal { get; set; }
        public bool Cobradovunicre { get; set; }
        public string Entidademb { get; set; } = null!;
        public string Refmb1 { get; set; } = null!;
        public string Refmb2 { get; set; } = null!;
        public string Refmb3 { get; set; } = null!;
        public decimal Etotalmb { get; set; }
        public decimal Totalmb { get; set; }
        public bool Pagomb { get; set; }
        public string Barcode { get; set; } = null!;
        public string Ousrinis { get; set; } = null!;
        public DateTime Ousrdata { get; set; }
        public string Ousrhora { get; set; } = null!;
        public string Usrinis { get; set; } = null!;
        public DateTime Usrdata { get; set; }
        public string Usrhora { get; set; } = null!;
        public bool Marcada { get; set; }
        public bool Cobradovmbway { get; set; }
        public decimal Latitudecarga { get; set; }
        public decimal Longitudecarga { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Codmotiseimp { get; set; } = null!;
        public string Motiseimp { get; set; } = null!;
        public string Anulinis { get; set; } = null!;
        public DateTime Anuldata { get; set; }
        public string Anulhora { get; set; } = null!;
        public string Atcud { get; set; } = null!;
        public decimal Contingencia { get; set; }
        public string Codendereco { get; set; } = null!;
        public decimal Meiotranscv { get; set; }
        public string Telefone { get; set; } = null!;
        public string Codeuserpay { get; set; } = null!;
        public bool Onlinepay { get; set; }
    }
}
