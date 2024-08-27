using Newtonsoft.Json;

namespace FIPAG_KOBOTOOLBOX.Persistence.APIs.DTOs
{

    public class Group1Consumos
    {
        public long no{ get; set; }
        public string nome{ get; set; }
        [JsonProperty(PropertyName = "ID_Benef_Kobo")]
        public long IDBenefKobo { get; set; }
    }

    public class Group2Consumos
    {
        [JsonProperty(PropertyName = "Nome_do_Documento")]
        public string nmdoc { get; set; }

        [JsonProperty(PropertyName = "fno")]
        public int fno { get; set; }

        [JsonProperty(PropertyName = "Total_em_Meticais")]
        public decimal totalMeticais { get; set; }

        [JsonProperty(PropertyName = "Data_da_Fatura")]
        public string fdata { get; set; }

        [JsonProperty(PropertyName = "Anulada")]
        public string anulado { get; set; }
    }

    public class Group3Consumos
    {
        [JsonProperty(PropertyName = "Tipo_Fatura")]
        public string tipoFatura { get; set; }

        [JsonProperty(PropertyName = "Periodo")]
        public string periodo { get; set; }

        [JsonProperty(PropertyName = "Leitura_Anterior")]
        public decimal leituraAnterior { get; set; }

        [JsonProperty(PropertyName = "Leitura_Actual")]
        public decimal leituraActual { get; set; }

        [JsonProperty(PropertyName = "Consumo_Faturado")]
        public decimal consumoFaturado { get; set; }

        [JsonProperty(PropertyName = "Consumo_Real")]
        public decimal consumoReal { get; set; }

        [JsonProperty(PropertyName = "Anomalia")]
        public string anomalia { get; set; }
    }
}
