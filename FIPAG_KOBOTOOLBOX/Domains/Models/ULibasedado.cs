using System;
using System.Collections.Generic;

namespace FIPAG_KOBOTOOLBOX.Domains.Models
{
    public partial class ULibasedado
    {
        public string ULibasedadostamp { get; set; } = null!;
        public string Nome { get; set; } = null!;
        public string SubNome { get; set; } = null!;
        public string Formid { get; set; } = null!;
        public string Basedadosstamp { get; set; } = null!;
        public string Ousrinis { get; set; } = null!;
        public DateTime Ousrdata { get; set; }
        public string Ousrhora { get; set; } = null!;
        public string Usrinis { get; set; } = null!;
        public DateTime Usrdata { get; set; }
        public string Usrhora { get; set; } = null!;
        public bool Marcada { get; set; }
    }
}
