﻿namespace FIPAG_KOBOTOOLBOX.Persistence.APIs.KoboToolBox.Responses
{
    public class ResultsResponseDTO
    {
        /*
        public int count { get; set; }
        public int successes { get; set; }
        public int failures { get; set; }
        */

        public List<ResultsDTO> results { get; set; }
        public override string ToString() => Newtonsoft.Json.JsonConvert.SerializeObject(this);
    }
}
