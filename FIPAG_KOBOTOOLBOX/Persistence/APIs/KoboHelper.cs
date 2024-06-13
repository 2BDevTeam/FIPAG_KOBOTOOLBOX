using FIPAG_KOBOTOOLBOX.Extensions;
using FIPAG_KOBOTOOLBOX.Persistence.APIs.KoboToolBox;
using FIPAG_KOBOTOOLBOX.Persistence.APIs.KoboToolBox.Responses;
using FIPAG_KOBOTOOLBOX.Persistence.Contexts;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace FIPAG_KOBOTOOLBOX.Persistence.APIs
{
    public class KoboHelper
    {

        public string GetKoboFormID(string form)
        {

            switch (form)
            {
                case "Ligações":
                    return "aRGAdMpcPyV8dgjnisTatW";

                case "Consumos":
                    return "appSEXDxxBLkupDPVKi3jp";
                default:
                    break;
            }

            return "";
        }



    }
}
