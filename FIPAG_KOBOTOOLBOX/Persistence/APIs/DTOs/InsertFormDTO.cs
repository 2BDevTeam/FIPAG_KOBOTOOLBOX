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

}
