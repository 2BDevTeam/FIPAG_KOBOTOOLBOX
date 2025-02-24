using System.Text.Json.Serialization;
using Newtonsoft.Json;
using JsonIgnoreAttribute = Newtonsoft.Json.JsonIgnoreAttribute;

namespace FIPAG_KOBOTOOLBOX.DTOs
{

    public class RespostaDTO
    {

        public RespostaDTO(decimal responseId, string codigo,  string descricao)
        {
            Codigo = codigo;
            Descricao = descricao;
            Id = responseId;
        }
        
        public RespostaDTO()
        {
        }


        [JsonIgnore]
        public decimal Id { get; set; }
        
        [JsonPropertyName ("code")]
        public string Codigo { get; set; }
        
        [JsonPropertyName ("description")]
        public string Descricao { get; set; }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
