using System;
using System.Collections.Generic;

namespace FIPAG_KOBOTOOLBOX.Domains.Models
{
    public partial class USyncQueue
    {
        public string USyncqueuestamp { get; set; } = null!;
        public string Nometabela { get; set; } = null!;
        public string Stamptabela { get; set; } = null!;
        public string Accao { get; set; } = null!;
        public string Ousrinis { get; set; } = null!;
        public DateTime Ousrdata { get; set; }
        public string Ousrhora { get; set; } = null!;
        public string Usrinis { get; set; } = null!;
        public DateTime Usrdata { get; set; }
        public string Usrhora { get; set; } = null!;
        public bool Marcada { get; set; }
        public string campo { get; set; }
        public string valor { get; set; }
    }
}
