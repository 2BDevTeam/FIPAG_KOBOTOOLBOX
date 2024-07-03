using System;
using System.Collections.Generic;

namespace FIPAG_KOBOTOOLBOX.Domains.Models
{
    public partial class Cl
    {
        public string Clstamp { get; set; } = null!;
        public string Nome { get; set; } = null!;
        public decimal No { get; set; }
        public decimal Estab { get; set; }
        public string Nome2 { get; set; } = null!;
        public string Morada { get; set; } = null!;
        public string Local { get; set; } = null!;
        public string Zona { get; set; } = null!;
        public string Tipo { get; set; } = null!;
        public string Naturalid { get; set; } = null!;
        public string Nib { get; set; } = null!;
        public string Segmento { get; set; } = null!;
        public string Fref { get; set; } = null!;
        public string Ousrinis { get; set; } = null!;
        public DateTime Ousrdata { get; set; }
        public string Ousrhora { get; set; } = null!;
        public string Usrinis { get; set; } = null!;
        public DateTime Usrdata { get; set; }
        public string Usrhora { get; set; } = null!;
        public bool Marcada { get; set; }
        public bool Inactivo { get; set; }
        public string Id { get; set; } = null!;
    }
}
