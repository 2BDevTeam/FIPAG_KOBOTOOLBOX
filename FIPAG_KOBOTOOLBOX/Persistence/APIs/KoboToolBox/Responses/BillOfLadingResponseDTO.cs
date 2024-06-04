namespace FIPAG_KOBOTOOLBOX.Persistence.APIs.KoboToolBox.Responses
{
    public class BillOfLadingResponseDTO
    {
        public BillOfLadingDTO bill_of_lading { get; set; }
        public override string ToString() => Newtonsoft.Json.JsonConvert.SerializeObject(this);
    }
}
