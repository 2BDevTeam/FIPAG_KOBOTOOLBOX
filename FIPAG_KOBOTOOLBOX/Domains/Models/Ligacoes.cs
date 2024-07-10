using Newtonsoft.Json;

namespace FIPAG_KOBOTOOLBOX.Domains.Models
{
    public partial class Ligacoes
    {
        public string Clstamp { get; set; }
        public long No{ get; set; }
        public string Nome{ get; set; }
        public long IDBenefKobo { get; set; }
        public DateTime dataLigacao { get; set; }
        public DateTime dataTermino { get; set; }
    }
}
