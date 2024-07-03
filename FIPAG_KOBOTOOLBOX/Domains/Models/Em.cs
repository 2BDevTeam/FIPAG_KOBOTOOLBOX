using System;
using System.Collections.Generic;

namespace FIPAG_KOBOTOOLBOX.Domains.Models
{
    public partial class Em
    {
        public string Emstamp { get; set; } = null!;
        public string Nome { get; set; } = null!;
        public decimal No { get; set; }
        public string Morada { get; set; } = null!;
        public string Cont2 { get; set; } = null!;
        public string Cont3 { get; set; } = null!;
        public string Telefone { get; set; } = null!;
        public string Zona { get; set; } = null!;
        public decimal Vendedor { get; set; }
        public string Vendnm { get; set; } = null!;
        public string Ncont { get; set; } = null!;
        public string Obs { get; set; } = null!;
        public string Local { get; set; } = null!;
        public decimal Clno { get; set; }
        public string Ctact2tel { get; set; } = null!;
        public string Ctact3tel { get; set; } = null!;
        public string Url { get; set; } = null!;
        public string Segmento { get; set; } = null!;
        public string Area { get; set; } = null!;
        public string Ousrinis { get; set; } = null!;
        public DateTime Ousrdata { get; set; }
        public string Ousrhora { get; set; } = null!;
        public string Usrinis { get; set; } = null!;
        public DateTime Usrdata { get; set; }
        public string Usrhora { get; set; } = null!;
        public DateTime UBidata { get; set; }
        public string UBilocal { get; set; } = null!;
        public string UBino { get; set; } = null!;
        public string UNatural { get; set; } = null!;
        public decimal UNcasa { get; set; }
        public decimal UNquart { get; set; }
        public decimal UNtalhao { get; set; }
        public string UProvince { get; set; } = null!;
        public string UDistrit { get; set; } = null!;
        public string Codpais { get; set; } = null!;
        public string Pais { get; set; } = null!;
        public string Concelho { get; set; } = null!;
        public string Distrito { get; set; } = null!;
        public string Pncont { get; set; } = null!;
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public DateTime UNascimen { get; set; }
        public string URua { get; set; } = null!;
        public string UPorta { get; set; } = null!;
        public string UAndar { get; set; } = null!;
        public string UPref { get; set; } = null!;
        public string UEndereco { get; set; } = null!;
        public decimal UNrua { get; set; }
        public decimal UKoboid { get; set; }
        public bool UKoboori { get; set; }
        public string UOrigem { get; set; } = null!;
    }
}
