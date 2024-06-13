using Newtonsoft.Json;

namespace FIPAG_KOBOTOOLBOX.Persistence.APIs.DTOs
{
    public class UpdateFormDTO
    {
        public PayloadFormDTO payload { get; set; }
    }

    public class PayloadFormDTO
    {
        public List<int> submission_ids { get; set; }

        public DataFormDTO data { get; set; }
    }
    public class DataFormDTO
    {
        [JsonProperty(PropertyName = "group4/adicionado_PHC")]
        public string adicionado_PHC { get; set; }

        [JsonProperty(PropertyName = "group4/isCliente")]
        public string isCliente { get; set; }
    }

}
