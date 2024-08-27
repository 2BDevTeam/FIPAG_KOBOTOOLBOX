using Newtonsoft.Json;

namespace FIPAG_KOBOTOOLBOX.Persistence.APIs.DTOs
{
    public class UpdateFormDTO
    {
        public PayloadFormDTO payload { get; set; }
    }

    public class PayloadFormDTO
    {
        public List<long> submission_ids { get; set; }

        public DataFormDTO data { get; set; }
    }
    public class DataFormDTO
    {

        [JsonProperty(PropertyName = "grupo1/nome_chefe_af")]
        public string nome_chefe_af { get; set; }

        [JsonProperty(PropertyName = "grupo1/Data_de_nascimento")]
        public DateTime DataDeNascimento { get; set; }

        [JsonProperty(PropertyName = "grupo1/N_do_Bilhete_de_Identidade")]
        public string NrBi { get; set; }

        [JsonProperty(PropertyName = "grupo1/Data_emissao_BI")]
        public DateTime DataEmissaoBI { get; set; }

        [JsonProperty(PropertyName = "grupo1/Local_de_emiss_o_do_ilhete_de_Identidade")]
        public string LocalEmissaoBI { get; set; }

        [JsonProperty(PropertyName = "grupo1/Telefone")]
        public string telefone { get; set; }

        [JsonProperty(PropertyName = "grupo1/NUIT")]
        public string nuit { get; set; }

        [JsonProperty(PropertyName = "grupo1/n_casa")]
        public string ncasa { get; set; }

        [JsonProperty(PropertyName = "group4/adicionado_PHC")]
        public string adicionado_PHC { get; set; }

        [JsonProperty(PropertyName = "group4/isCliente")]
        public string isCliente { get; set; }


        [JsonProperty(PropertyName = "Group2Consumos/Anulada")]
        public string anulada { get; set; }

        public string EstadoLigacao { get; set; }
    }

}
