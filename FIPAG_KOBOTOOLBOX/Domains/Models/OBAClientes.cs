using System;
using System.Collections.Generic;

namespace FIPAG_KOBOTOOLBOX.Domains.Models
{
    public partial class Obaclientes
    {
        public double KoboId { get; set; }
        public string? KoboNome { get; set; }
        public double? PhcId { get; set; }
        public string? PhcNome { get; set; }
        public bool Sync { get; set; }
        public string Error { get; set; } = null!;
    }
}
