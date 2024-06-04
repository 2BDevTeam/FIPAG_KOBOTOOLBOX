using Newtonsoft.Json;

namespace FIPAG_KOBOTOOLBOX.Persistence.APIs.DTOs
{
    public class LiftContainerHoldDTO
    {
        public string reference_number { get; set; }
        public DateTime lift_until { get; set; }
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
