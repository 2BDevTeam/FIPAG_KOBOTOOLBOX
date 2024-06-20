using System;
using System.Collections.Generic;

namespace FIPAG_KOBOTOOLBOX.Domains.Models
{
    public partial class Bo
    {
        public string Bostamp { get; set; } = null!;
        public string Nmdos { get; set; } = null!;
        public decimal Obrano { get; set; }
        public DateTime Dataobra { get; set; }
        public string Nome { get; set; } = null!;
        public decimal No { get; set; }
        public decimal Boano { get; set; }
        public string Maquina { get; set; } = null!;
        public string Marca { get; set; } = null!;
        public string Serie { get; set; } = null!;
        public string Zona { get; set; } = null!;
        public string Obs { get; set; } = null!;
        public decimal Ndos { get; set; }
        public string Moeda { get; set; } = null!;
        public string Morada { get; set; } = null!;
        public string Local { get; set; } = null!;
        public string Codpost { get; set; } = null!;
        public DateTime Ultfact { get; set; }
        public decimal Period { get; set; }
        public string Ncont { get; set; } = null!;
        public string Segmento { get; set; } = null!;
        public bool Impresso { get; set; }
        public string Userimpresso { get; set; } = null!;
        public string Cobranca { get; set; } = null!;
        public decimal Ecusto { get; set; }
        public string Trab1 { get; set; } = null!;
        public string Trab2 { get; set; } = null!;
        public string Trab3 { get; set; } = null!;
        public string Trab4 { get; set; } = null!;
        public string Trab5 { get; set; } = null!;
        public decimal Custo { get; set; }
        public string Tabela1 { get; set; } = null!;
        public string Fref { get; set; } = null!;
        public string Ccusto { get; set; } = null!;
        public string Ncusto { get; set; } = null!;
        public bool Infref { get; set; }
        public bool Lifref { get; set; }
        public string Memissao { get; set; } = null!;
        public string Nome2 { get; set; } = null!;
        public string Ousrinis { get; set; } = null!;
        public DateTime Ousrdata { get; set; }
        public string Ousrhora { get; set; } = null!;
        public string Usrinis { get; set; } = null!;
        public DateTime Usrdata { get; set; }
        public string Usrhora { get; set; } = null!;
    }
}
