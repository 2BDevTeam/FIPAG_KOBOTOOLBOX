using Newtonsoft.Json;

namespace FIPAG_KOBOTOOLBOX.Persistence.APIs.DTOs
{
    public class LiftContainerHoldRequest
    {
        public LiftContainerHoldDTO hold { get; set; }
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
