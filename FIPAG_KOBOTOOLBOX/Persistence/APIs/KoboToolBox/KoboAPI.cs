
using AutoMapper.Execution;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using FIPAG_KOBOTOOLBOX.DTOs;

using FIPAG_KOBOTOOLBOX.Helper;
using FIPAG_KOBOTOOLBOX.Persistence.APIs.DTOs;
using FIPAG_KOBOTOOLBOX.Persistence.APIs;

using FIPAG_KOBOTOOLBOX.Persistence.APIs.KoboToolBox.Responses;
using System.Diagnostics;
using System.Net;
using System.Dynamic;
using System.Text.Json;

namespace FIPAG_KOBOTOOLBOX.Persistence.APIs.KoboToolBox
{
    public class KoboAPI
    {

        private readonly HttpHelper httpHelper = new HttpHelper();
        private readonly LogHelper logHelper = new LogHelper();
        private KoboHelper _koboToolBoxHelper = new KoboHelper();

        public ResultsResponseDTO GetResult(string nome)
        {
            string formID = "aj3EiDTv5DmsrxsPEGwi28";
            try
            {
                string result = "";

                HttpWebRequest httpWebRequest = httpHelper.getHttpWebRequestByProviderApiKey(200, "assets", $"/{formID}/data/?format=json&query={{\"grupo1/nome_chefe_af\":\"{nome}\"}}");

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

        public ResultsResponseDTO GetResultByIdd(int idd, string formID)
        {

            try
            {
                string result = "";

                HttpWebRequest httpWebRequest = httpHelper.getHttpWebRequestByProviderApiKey(200, "assets", $"/{formID}/data/?format=json&query={{\"_idd\":\"{idd}\"}}");

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
                Debug.WriteLine("GET GetResultByIdd WEB EXCEPTION  Response::::---::::: " + rawresp);

                var errorDTO = new ErrorDTO { message = ex?.Message, stack = ex?.StackTrace?.ToString(), inner = ex?.InnerException?.ToString() + "  " + rawresp };
                //Debug.Print($"ERROR DTO {errorDTO}");
                var finalResponse = new ResponseDTO(new ResponseCodesDTO("EX-500", "Error", logHelper.generateResponseID()), errorDTO.ToString(), null);
                logHelper.GenerateApiLog(finalResponse, logHelper.generateResponseID().ToString(), "GetResultByIdd", rawresp);

                return null;

            }
            catch (Exception ex)
            {
                var errorDTO = new ErrorDTO { message = ex?.Message, stack = ex?.StackTrace?.ToString(), inner = ex?.InnerException?.ToString() + "  " };
                Debug.Print($"GetResultByIdd ERROR DTO {errorDTO}");
                var finalResponse = new ResponseDTO(new ResponseCodesDTO("0007", "Error", logHelper.generateResponseID()), errorDTO.ToString(), null);
                logHelper.GenerateApiLog(finalResponse, logHelper.generateResponseID().ToString(), "GetResultByIdd", errorDTO?.ToString());
                return null;

            }
        }


        public UpdateResponseDTO UpdNaoAdicionadosPHC(int id, string formID)
        {
            //string formID = "aj3EiDTv5DmsrxsPEGwi28";
            try
            {
                string result = "";

                HttpWebRequest httpWebRequest = httpHelper.getHttpWebRequestByProviderApiKey(200, "assets", $"/{formID}/data/bulk/?format=json");
                httpWebRequest.Method = "PATCH";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    UpdateFormDTO body = new UpdateFormDTO
                    {
                        payload = new PayloadFormDTO
                        {
                            submission_ids = new List<int> { id },
                            data = new DataFormDTO
                            {
                                adicionado_PHC = "true"
                            }
                        },
                    };

                    string json = JsonConvert.SerializeObject(body);

                    JObject jsonObject = JObject.Parse(json);
                    RemoveNullProperties(jsonObject);

                    string cleanedJson = jsonObject.ToString(Formatting.None);
                    Debug.Print(cleanedJson);

                    streamWriter.Write(cleanedJson);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                StreamReader streamReader = new System.IO.StreamReader(httpResponse.GetResponseStream());

                result = streamReader.ReadToEnd();

                Debug.Print($"RESULT Results {result}");

                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                };

                UpdateResponseDTO results = JsonConvert.DeserializeObject<UpdateResponseDTO>(result, settings);

                return results;
            }
            catch (WebException ex)
            {

                System.IO.StreamReader reader = new System.IO.StreamReader(ex?.Response?.GetResponseStream());
                String rawresp = reader.ReadToEnd();
                Debug.WriteLine("UpdNaoAdicionadosPHC WEB EXCEPTION  Response::::---::::: " + rawresp);

                var errorDTO = new ErrorDTO { message = ex?.Message, stack = ex?.StackTrace?.ToString(), inner = ex?.InnerException?.ToString() + "  " + rawresp };
                //Debug.Print($"ERROR DTO {errorDTO}");
                var finalResponse = new ResponseDTO(new ResponseCodesDTO("EX-500", "Error", logHelper.generateResponseID()), errorDTO.ToString(), null);
                logHelper.GenerateApiLog(finalResponse, logHelper.generateResponseID().ToString(), "GetResults", rawresp);

                return null;

            }
            catch (Exception ex)
            {
                var errorDTO = new ErrorDTO { message = ex?.Message, stack = ex?.StackTrace?.ToString(), inner = ex?.InnerException?.ToString() + "  " };
                Debug.Print($"UpdNaoAdicionadosPHC ERROR DTO {errorDTO}");
                var finalResponse = new ResponseDTO(new ResponseCodesDTO("0007", "Error", logHelper.generateResponseID()), errorDTO.ToString(), null);
                logHelper.GenerateApiLog(finalResponse, logHelper.generateResponseID().ToString(), "GetResults", errorDTO?.ToString());
                return null;

            }
        }

        public UpdateResponseDTO UpdIsClientePHC(int id, string formID)
        {
            try
            {
                string result = "";

                HttpWebRequest httpWebRequest = httpHelper.getHttpWebRequestByProviderApiKey(200, "assets", $"/{formID}/data/bulk/?format=json");
                httpWebRequest.Method = "PATCH";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    UpdateFormDTO body = new UpdateFormDTO
                    {
                        payload = new PayloadFormDTO
                        {
                            submission_ids = new List<int> { id },
                            data = new DataFormDTO
                            {
                                isCliente = "true"
                            }
                        },
                    };

                    string json = JsonConvert.SerializeObject(body);

                    JObject jsonObject = JObject.Parse(json);
                    RemoveNullProperties(jsonObject);

                    string cleanedJson = jsonObject.ToString(Formatting.None);
                    Debug.Print(cleanedJson);

                    streamWriter.Write(cleanedJson);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                StreamReader streamReader = new System.IO.StreamReader(httpResponse.GetResponseStream());

                result = streamReader.ReadToEnd();

                Debug.Print($"RESULT Results {result}");

                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                };

                UpdateResponseDTO results = JsonConvert.DeserializeObject<UpdateResponseDTO>(result, settings);

                return results;
            }
            catch (WebException ex)
            {

                System.IO.StreamReader reader = new System.IO.StreamReader(ex?.Response?.GetResponseStream());
                String rawresp = reader.ReadToEnd();
                Debug.WriteLine("UpdNaoAdicionadosPHC WEB EXCEPTION  Response::::---::::: " + rawresp);

                var errorDTO = new ErrorDTO { message = ex?.Message, stack = ex?.StackTrace?.ToString(), inner = ex?.InnerException?.ToString() + "  " + rawresp };
                //Debug.Print($"ERROR DTO {errorDTO}");
                var finalResponse = new ResponseDTO(new ResponseCodesDTO("EX-500", "Error", logHelper.generateResponseID()), errorDTO.ToString(), null);
                logHelper.GenerateApiLog(finalResponse, logHelper.generateResponseID().ToString(), "GetResults", rawresp);

                return null;

            }
            catch (Exception ex)
            {
                var errorDTO = new ErrorDTO { message = ex?.Message, stack = ex?.StackTrace?.ToString(), inner = ex?.InnerException?.ToString() + "  " };
                Debug.Print($"UpdNaoAdicionadosPHC ERROR DTO {errorDTO}");
                var finalResponse = new ResponseDTO(new ResponseCodesDTO("0007", "Error", logHelper.generateResponseID()), errorDTO.ToString(), null);
                logHelper.GenerateApiLog(finalResponse, logHelper.generateResponseID().ToString(), "GetResults", errorDTO?.ToString());
                return null;

            }
        }



        public UpdateResponseDTO UpdEstadoLigacao(int id, string estado, string formID)
        {


            //string formID = "aRGAdMpcPyV8dgjnisTatW";
            try
            {
                string result = "";

                HttpWebRequest httpWebRequest = httpHelper.getHttpWebRequestByProviderApiKey(200, "assets", $"/{formID}/data/bulk/?format=json");
                httpWebRequest.Method = "PATCH";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    UpdateFormDTO body = new UpdateFormDTO
                    {
                        payload = new PayloadFormDTO
                        {
                            submission_ids = new List<int> { id },
                            data = new DataFormDTO
                            {
                                EstadoLigacao = estado
                            }
                        },
                    };

                    string json = JsonConvert.SerializeObject(body);

                    JObject jsonObject = JObject.Parse(json);
                    RemoveNullProperties(jsonObject);

                    string cleanedJson = jsonObject.ToString(Formatting.None);
                    Debug.Print(cleanedJson);

                    streamWriter.Write(cleanedJson);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                StreamReader streamReader = new System.IO.StreamReader(httpResponse.GetResponseStream());

                result = streamReader.ReadToEnd();

                Debug.Print($"RESULT Results {result}");

                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                };

                UpdateResponseDTO results = JsonConvert.DeserializeObject<UpdateResponseDTO>(result, settings);

                return results;
            }
            catch (WebException ex)
            {

                System.IO.StreamReader reader = new System.IO.StreamReader(ex?.Response?.GetResponseStream());
                String rawresp = reader.ReadToEnd();
                Debug.WriteLine("UpdNaoAdicionadosPHC WEB EXCEPTION  Response::::---::::: " + rawresp);

                var errorDTO = new ErrorDTO { message = ex?.Message, stack = ex?.StackTrace?.ToString(), inner = ex?.InnerException?.ToString() + "  " + rawresp };
                //Debug.Print($"ERROR DTO {errorDTO}");
                var finalResponse = new ResponseDTO(new ResponseCodesDTO("EX-500", "Error", logHelper.generateResponseID()), errorDTO.ToString(), null);
                logHelper.GenerateApiLog(finalResponse, logHelper.generateResponseID().ToString(), "GetResults", rawresp);

                return null;

            }
            catch (Exception ex)
            {
                var errorDTO = new ErrorDTO { message = ex?.Message, stack = ex?.StackTrace?.ToString(), inner = ex?.InnerException?.ToString() + "  " };
                Debug.Print($"UpdNaoAdicionadosPHC ERROR DTO {errorDTO}");
                var finalResponse = new ResponseDTO(new ResponseCodesDTO("0007", "Error", logHelper.generateResponseID()), errorDTO.ToString(), null);
                logHelper.GenerateApiLog(finalResponse, logHelper.generateResponseID().ToString(), "GetResults", errorDTO?.ToString());
                return null;

            }
        }


        public ResultsResponseDTO GetFormNaoAdicionadosPHC2()
        {
            string formID = "aj3EiDTv5DmsrxsPEGwi28";
            try
            {
                string result = "";

                HttpWebRequest httpWebRequest = httpHelper.getHttpWebRequestByProviderApiKey(200, "assets", $"/{formID}/data/?format=json&query={{\"group4/adicionado_PHC\":\"false\"}}");

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
                var responseStream = ex?.Response?.GetResponseStream();
                if (responseStream == null)
                {
                    return null;
                }
                System.IO.StreamReader reader = new System.IO.StreamReader(responseStream);

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



        /*
         https://kf.kobotoolbox.org/api/v2/assets/aRvGf6VwkPF9878G3KTenn/data/?format=json&query={"$and":[{"_validation_status.uid":"validation_status_approved"},{"_id":"252735225"}]}
         */

        public ResultsResponseDTO GetFormNaoAdicionadosPHC(string formID, string cidade)
        {

            int start = 0;
            int limit = 1500;

            try
            {
                string result = "";

                HttpWebRequest httpWebRequest = httpHelper.getHttpWebRequestByProviderApiKey(
                    200,
                    "assets",
                    //$"/{formID}/data/?format=json&query={{\"group4/adicionado_PHC\":\"false\"}}&start={start}&limit={limit}"
                    //$"/{formID}/data/?format=json&query={{\"$and\":[{{\"_validation_status.uid\":\"validation_status_approved\"}},{{\"group4/adicionado_PHC\":\"false\"}}]}}&start={start}&limit={limit}"
                    //$"/{formID}/data/?format=json&query={{\"_validation_status.uid\":\"validation_status_approved\"}}&start={start}&limit={limit}"

                    $"/{formID}/data/?format=json&query={{\"$and\":[{{\"_validation_status.uid\":\"validation_status_approved\"}},{{\"group4/adicionado_PHC\":\"false\"}},{{\"grupo1/cidade\":\"{cidade}\"}}]}}&start={start}&limit={limit}\r\n"
                );

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }

                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                };

                ResultsResponseDTO results = JsonConvert.DeserializeObject<ResultsResponseDTO>(result, settings);

                return results;
            }
            catch (WebException ex)
            {
                var responseStream = ex?.Response?.GetResponseStream();
                if (responseStream == null)
                {
                    return null;
                }
                using (StreamReader reader = new StreamReader(responseStream))
                {
                    String rawresp = reader.ReadToEnd();
                    Debug.WriteLine("GET GetResults WEB EXCEPTION Response::::---::::: " + rawresp);
                    var errorDTO = new ErrorDTO { message = ex?.Message, stack = ex?.StackTrace?.ToString(), inner = ex?.InnerException?.ToString() + " " + rawresp };
                    var finalResponse = new ResponseDTO(new ResponseCodesDTO("EX-500", "Error", logHelper.generateResponseID()), errorDTO.ToString(), null);
                    logHelper.GenerateApiLog(finalResponse, logHelper.generateResponseID().ToString(), "GetResults", rawresp);
                    return null;
                }
            }
            catch (Exception ex)
            {
                var errorDTO = new ErrorDTO { message = ex?.Message, stack = ex?.StackTrace?.ToString(), inner = ex?.InnerException?.ToString() + " " };
                Debug.Print($"GetResults ERROR DTO {errorDTO}");
                var finalResponse = new ResponseDTO(new ResponseCodesDTO("0007", "Error", logHelper.generateResponseID()), errorDTO.ToString(), null);
                logHelper.GenerateApiLog(finalResponse, logHelper.generateResponseID().ToString(), "GetResults", errorDTO?.ToString());
                return null;
            }
        }



        public InsertResponseDTO AddDataToKobo(InsertFormDTO body, string formID)
        {

            try
            {
                string result = "";

                HttpWebRequest httpWebRequest = httpHelper.getHttpWebRequestByProviderApiKey(200, "submissions", $"/submissions");
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    body.id = formID;

                    string json = JsonConvert.SerializeObject(body);

                    JObject jsonObject = JObject.Parse(json);
                    RemoveNullProperties(jsonObject);

                    string cleanedJson = jsonObject.ToString(Formatting.None);
                    Debug.Print(cleanedJson);

                    streamWriter.Write(cleanedJson);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                StreamReader streamReader = new System.IO.StreamReader(httpResponse.GetResponseStream());

                result = streamReader.ReadToEnd();

                Debug.Print($"RESULT Results {result}");

                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                };

                InsertResponseDTO results = JsonConvert.DeserializeObject<InsertResponseDTO>(result, settings);

                return results;
            }
            catch (WebException ex)
            {

                System.IO.StreamReader reader = new System.IO.StreamReader(ex?.Response?.GetResponseStream());
                String rawresp = reader.ReadToEnd();
                Debug.WriteLine("UpdNaoAdicionadosPHC WEB EXCEPTION  Response::::---::::: " + rawresp);

                var errorDTO = new ErrorDTO { message = ex?.Message, stack = ex?.StackTrace?.ToString(), inner = ex?.InnerException?.ToString() + "  " + rawresp };
                //Debug.Print($"ERROR DTO {errorDTO}");
                var finalResponse = new ResponseDTO(new ResponseCodesDTO("EX-500", "Error", logHelper.generateResponseID()), errorDTO.ToString(), null);
                logHelper.GenerateApiLog(finalResponse, logHelper.generateResponseID().ToString(), "GetResults", rawresp);

                return null;

            }
            catch (Exception ex)
            {
                var errorDTO = new ErrorDTO { message = ex?.Message, stack = ex?.StackTrace?.ToString(), inner = ex?.InnerException?.ToString() + "  " };
                Debug.Print($"UpdNaoAdicionadosPHC ERROR DTO {errorDTO}");
                var finalResponse = new ResponseDTO(new ResponseCodesDTO("0007", "Error", logHelper.generateResponseID()), errorDTO.ToString(), null);
                logHelper.GenerateApiLog(finalResponse, logHelper.generateResponseID().ToString(), "GetResults", errorDTO?.ToString());
                return null;

            }
        }


        public void RemoveNullProperties(JToken token)
        {
            if (token.Type == JTokenType.Object)
            {
                var propertiesToRemove = new List<JProperty>();
                foreach (var property in token.Children<JProperty>())
                {
                    if (property.Value.Type == JTokenType.Null)
                    {
                        propertiesToRemove.Add(property);
                    }
                    else
                    {
                        RemoveNullProperties(property.Value);
                    }
                }

                foreach (var property in propertiesToRemove)
                {
                    property.Remove();
                }
            }
            else if (token.Type == JTokenType.Array)
            {
                foreach (var item in token.Children())
                {
                    RemoveNullProperties(item);
                }
            }
        }


    }
}

//KOBO API