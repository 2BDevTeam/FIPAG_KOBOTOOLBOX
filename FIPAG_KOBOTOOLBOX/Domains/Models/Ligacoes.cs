using Newtonsoft.Json;

namespace FIPAG_KOBOTOOLBOX.Domains.Models
{
    public partial class Ligacoes
    {
        public string Clstamp { get; set; }
        public int No{ get; set; }
        public string Nome{ get; set; }
        public int IDBenefKobo { get; set; }
        public DateTime dataLigacao { get; set; }
    }
}
