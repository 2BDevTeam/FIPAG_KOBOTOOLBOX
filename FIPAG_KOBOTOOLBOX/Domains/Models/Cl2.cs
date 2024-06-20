using System;
using System.Collections.Generic;

namespace FIPAG_KOBOTOOLBOX.Domains.Models
{
    public partial class Cl2
    {
        public string Cl2stamp { get; set; } = null!;
        public string Codpais { get; set; } = null!;
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string UAndar { get; set; } = null!;
        public string UEndereco { get; set; } = null!;
        public string UNrua { get; set; } = null!;
        public string UPorta { get; set; } = null!;
        public string UPref { get; set; } = null!;
        public string URua { get; set; } = null!;
        public string Ousrinis { get; set; } = null!;
        public DateTime Ousrdata { get; set; }
        public string Ousrhora { get; set; } = null!;
        public string Usrinis { get; set; } = null!;
        public DateTime Usrdata { get; set; }
        public string Usrhora { get; set; } = null!;
        public DateTime UInicio { get; set; }
        public DateTime UTermino { get; set; }
        public string UClientid { get; set; } = null!;
        public DateTime UIniciof { get; set; }
        public string Codendereco { get; set; } = null!;
        public string UNcasa { get; set; } = null!;
        public string UDistrit { get; set; } = null!;
        public string UProvince { get; set; } = null!;
  
        public string UOrigem { get; set; } = null!;
        public decimal UKoboid { get; set; }
        public bool UKoboOri { get; set; }
        public bool UKoboSync { get; set; }

    }
}
