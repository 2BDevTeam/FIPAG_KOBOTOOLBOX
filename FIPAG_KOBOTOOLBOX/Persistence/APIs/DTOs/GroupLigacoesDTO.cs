using Newtonsoft.Json;

namespace FIPAG_KOBOTOOLBOX.Persistence.APIs.DTOs
{

    public class Group1Ligacoes
    {
        public int no { get; set; }
        public string nome { get; set; }
        public string DataLigacao { get; set; }

        [JsonProperty(PropertyName = "ID_Benef_Kobo")]
        public long IDBenefKobo { get; set; }

        public string EstadoLigacao { get; set; }
    }

}
