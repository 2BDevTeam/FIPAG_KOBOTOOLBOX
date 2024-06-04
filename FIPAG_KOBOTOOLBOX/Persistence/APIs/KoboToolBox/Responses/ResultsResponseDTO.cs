namespace FIPAG_KOBOTOOLBOX.Persistence.APIs.KoboToolBox.Responses
{
    public class ResultsResponseDTO
    {
        public List<ResultsDTO> results { get; set; }
        public override string ToString() => Newtonsoft.Json.JsonConvert.SerializeObject(this);
    }
}
