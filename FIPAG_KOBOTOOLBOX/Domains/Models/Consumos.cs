using Newtonsoft.Json;

namespace FIPAG_KOBOTOOLBOX.Domains.Models
{
    public partial class Consumos
    {
        public string Ftstamp{ get; set; }
        public string Clstamp { get; set; }
        public int No{ get; set; }
        public string Nome{ get; set; }
        public int IDBenefKobo { get; set; }
        public string Nmdoc { get; set; }
        public int Fno { get; set; }
        public decimal TotalMeticais { get; set; }
        public DateTime Fdata { get; set; }
        public string? TipoFatura { get; set; }
        public string Periodo { get; set; }
        public decimal LeituraAnterior { get; set; }
        public decimal LeituraActual { get; set; }
        public decimal ConsumoFaturado { get; set; }
        public decimal ConsumoReal { get; set; }
        public string? Anomalia { get; set; }
    }
}
