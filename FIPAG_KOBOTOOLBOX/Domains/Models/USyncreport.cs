using System;
using System.Collections.Generic;

namespace FIPAG_KOBOTOOLBOX.Domains.Models
{
    public partial class USyncreport
    {
        public string USyncreportstamp { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string Obs { get; set; } = null!;
        public string Ousrinis { get; set; } = null!;
        public DateTime Ousrdata { get; set; }
        public string Ousrhora { get; set; } = null!;
        public string Usrinis { get; set; } = null!;
        public DateTime Usrdata { get; set; }
        public string Usrhora { get; set; } = null!;
        public bool Marcada { get; set; }
        public string Tabstamp { get; set; } = null!;
        public decimal Tabno { get; set; }
        public decimal Koboid { get; set; }
    }
}
