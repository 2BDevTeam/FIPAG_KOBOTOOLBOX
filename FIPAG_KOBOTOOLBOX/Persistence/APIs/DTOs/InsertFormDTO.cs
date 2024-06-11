using Newtonsoft.Json;

namespace FIPAG_KOBOTOOLBOX.Persistence.APIs.DTOs
{
    public class InsertFormDTO
    {
        public string id { get; set; }

        public SubmissionDataDTO submission { get; set; }

    }

    public class SubmissionDataDTO
    {
        public MetaDataDTO meta { get; set; }

        //Consumos
        public Group1Consumos Group1Consumos { get; set; }
        public Group2Consumos Group2Consumos { get; set; }
        public Group3Consumos Group3Consumos { get; set; }

        //Ligações
        public Group1Ligacoes Group1Ligacoes { get; set; }

    }

    public class MetaDataDTO
    {
        public string instanceID { get; set; }
    }

    public class Group1Ligacoes
    {
        public decimal no { get; set; }
        public string nome { get; set; }
        public string DataLigacao { get; set; }

        [JsonProperty(PropertyName = "ID_Benef_Kobo")]
        public int IDBenefKobo { get; set; }
    }

    public class Group1Consumos
    {
        public int no{ get; set; }
        public string nome{ get; set; }
        [JsonProperty(PropertyName = "ID_Benef_Kobo")]
        public int IDBenefKobo { get; set; }
    }

    public class Group2Consumos
    {
        [JsonProperty(PropertyName = "Nome_do_Documento")]
        public string nmdoc { get; set; }

        [JsonProperty(PropertyName = "fno")]
        public int fno { get; set; }

        [JsonProperty(PropertyName = "Consumo_Mensal")]
        public decimal consumoMensal { get; set; }

        [JsonProperty(PropertyName = "Total_em_Meticais")]
        public decimal totalMeticais { get; set; }

        [JsonProperty(PropertyName = "Data_da_Fatura")]
        public string fdata { get; set; }
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
