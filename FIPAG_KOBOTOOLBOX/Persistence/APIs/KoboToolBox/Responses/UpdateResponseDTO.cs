namespace FIPAG_KOBOTOOLBOX.Persistence.APIs.KoboToolBox.Responses
{
    public class UpdateResponseDTO
    {
        public int count { get; set; }
        public int successes { get; set; }
        public int failures { get; set; }
        public List<UpdateResultsDTO> results { get; set; }
        public override string ToString() => Newtonsoft.Json.JsonConvert.SerializeObject(this);
    }

    public class UpdateResultsDTO
    {
        public string uuid { get; set; }
        public int status_code { get; set; }
        public string message { get; set; }
    }
}
