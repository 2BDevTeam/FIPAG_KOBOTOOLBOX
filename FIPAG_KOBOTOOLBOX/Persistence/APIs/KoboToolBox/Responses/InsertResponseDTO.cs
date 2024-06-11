namespace FIPAG_KOBOTOOLBOX.Persistence.APIs.KoboToolBox.Responses
{
    public class InsertResponseDTO
    {
        public string error { get; set; }
        public string message { get; set; }
        public string submissionDate { get; set; }
        public override string ToString() => Newtonsoft.Json.JsonConvert.SerializeObject(this);
    }

    
}
