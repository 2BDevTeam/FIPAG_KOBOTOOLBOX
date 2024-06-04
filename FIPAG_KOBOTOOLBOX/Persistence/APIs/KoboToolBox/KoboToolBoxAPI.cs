
using AutoMapper.Execution;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using FIPAG_KOBOTOOLBOX.DTOs;

using FIPAG_KOBOTOOLBOX.Helper;
using FIPAG_KOBOTOOLBOX.Persistence.APIs.DTOs;

using FIPAG_KOBOTOOLBOX.Persistence.APIs.KoboToolBox.Responses;
using System.Diagnostics;
using System.Net;
using System.Dynamic;
using System.Text.Json;

namespace FIPAG_KOBOTOOLBOX.Persistence.APIs.KoboToolBox
{
    public class KoboToolBoxAPI
    {

        private readonly HttpHelper httpHelper = new HttpHelper();
        private readonly LogHelper logHelper = new LogHelper();

        public ResultsResponseDTO GetResult(string nome)
        {
            try
            {
                string result = "";

                HttpWebRequest httpWebRequest = httpHelper.getHttpWebRequestByProviderApiKey(200, "assets", $"/aPLahUpVMZK4cjGC5i38gP/data/?format=json&query={{\"grupo1/nome_chefe_af\":\"{nome}\"}}");

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                //Debug.Print($"Test");

                StreamReader streamReader = new System.IO.StreamReader(httpResponse.GetResponseStream());

                result = streamReader.ReadToEnd();

                Debug.Print($"RESULT Results {result}");

                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                };

                ResultsResponseDTO results = JsonConvert.DeserializeObject<ResultsResponseDTO>(result, settings);

                return results;
            }
            catch (WebException ex)
            {

                System.IO.StreamReader reader = new System.IO.StreamReader(ex?.Response?.GetResponseStream());
                String rawresp = reader.ReadToEnd();
                Debug.WriteLine("GET GetResults WEB EXCEPTION  Response::::---::::: " + rawresp);

                var errorDTO = new ErrorDTO { message = ex?.Message, stack = ex?.StackTrace?.ToString(), inner = ex?.InnerException?.ToString() + "  " + rawresp };
                //Debug.Print($"ERROR DTO {errorDTO}");
                var finalResponse = new ResponseDTO(new ResponseCodesDTO("EX-500", "Error", logHelper.generateResponseID()), errorDTO.ToString(), null);
                logHelper.GenerateApiLog(finalResponse, logHelper.generateResponseID().ToString(), "GetResults", rawresp);

                return null;

            }
            catch (Exception ex)
            {
                var errorDTO = new ErrorDTO { message = ex?.Message, stack = ex?.StackTrace?.ToString(), inner = ex?.InnerException?.ToString() + "  " };
                Debug.Print($"GetResults ERROR DTO {errorDTO}");
                var finalResponse = new ResponseDTO(new ResponseCodesDTO("0007", "Error", logHelper.generateResponseID()), errorDTO.ToString(), null);
                logHelper.GenerateApiLog(finalResponse, logHelper.generateResponseID().ToString(), "GetResults", errorDTO?.ToString());
                return null;

            }
        }

        public BillOfLadingResponseDTO GetBillOfLadingByNumber(GetBillingOfLadingRequestDTO getBillingOfLadingDTO)
        {
            string encodedUrl = getBillingOfLadingDTO?.billOfLadingNumber.Replace("/", "%2F");
            try
            {
                string result = "";

                HttpWebRequest httpWebRequest = httpHelper.getHttpWebRequestByProvider(200, "voyages", $"/voyages/{getBillingOfLadingDTO?.voyageNumber}/bill_of_ladings/{encodedUrl}.json");

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                StreamReader streamReader = new System.IO.StreamReader(httpResponse.GetResponseStream());

                result = streamReader.ReadToEnd();

                Debug.Print($"RESULT GetBillOfLadingByNumber {result}");
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                };

                BillOfLadingResponseDTO billOfLading = JsonConvert.DeserializeObject<BillOfLadingResponseDTO>(result, settings);



                return billOfLading;
            }
            catch (WebException ex)
            {

                System.IO.StreamReader reader = new System.IO.StreamReader(ex?.Response?.GetResponseStream());
                String rawresp = reader.ReadToEnd();
                Debug.WriteLine("GET GetBillOfLadingByNumber WEB EXCEPTION  Response::::---::::: " + rawresp);

                var errorDTO = new ErrorDTO { message = ex?.Message, stack = ex?.StackTrace?.ToString(), inner = ex?.InnerException?.ToString() + "  " + rawresp };
                //Debug.Print($"ERROR DTO {errorDTO}");
                var finalResponse = new ResponseDTO(new ResponseCodesDTO("EX-500", "Error", logHelper.generateResponseID()), errorDTO.ToString(), null);
                logHelper.GenerateApiLog(finalResponse, logHelper.generateResponseID().ToString(), "GetBillOfLadingByNumber", rawresp);

                return null;

            }
            catch (Exception ex)
            {
                var errorDTO = new ErrorDTO { message = ex?.Message, stack = ex?.StackTrace?.ToString(), inner = ex?.InnerException?.ToString() + "  " };
                Debug.Print($"GetBillOfLadingByNumber ERROR DTO {errorDTO}");
                var finalResponse = new ResponseDTO(new ResponseCodesDTO("0007", "Error", logHelper.generateResponseID()), errorDTO.ToString(), null);
                logHelper.GenerateApiLog(finalResponse, logHelper.generateResponseID().ToString(), "GetBillOfLadingByNumber", errorDTO?.ToString());
                return null;

            }
        }

    }
}

//OCTOPI API