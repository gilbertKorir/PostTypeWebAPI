using FetchPerson.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FetchPerson.Controllers
{
    public class ReportsController : Controller
    {
        HttpClient client = new HttpClient();

        public ReportsController()
        {
            client = new HttpClient();
        }
        // GET: Reports
        public ActionResult ReportsAction()
        {
            return View();
        }



        public JsonResult GetCashStatement(ReportsModel report)
        {
            try
            {          
                string myAPI = "https://localhost:44368/api/person/getstatement";
                string strPayLoad = JsonConvert.SerializeObject(report);

                HttpWebRequest client = (HttpWebRequest)WebRequest.Create(myAPI);
                client.Method = "POST";
                client.ContentType = "application/json";
                client.Timeout = 60000; 

                using (Stream webStream = client.GetRequestStream())
                using (StreamWriter requestWriter = new StreamWriter(webStream, Encoding.ASCII))
                    {
                       requestWriter.Write(strPayLoad);
                   }

                using (var response = (HttpWebResponse)client.GetResponse())
                {
                    using (var streamReader = new StreamReader(response.GetResponseStream()))
                    {
                        string data = streamReader.ReadToEnd();
                        var jsonSettings = new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore
                        };
                        List<Statement> result = JsonConvert.DeserializeObject<List<Statement>>(data, jsonSettings);

                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (WebException ex)
            {
                // handle the exception here
                string errorMessage = string.Empty;
                if (ex.Response != null)
                {
                    using (var errorResponse = (HttpWebResponse)ex.Response)
                    {
                        using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                        {
                            errorMessage = reader.ReadToEnd();
                        }
                    }
                }
                else
                {
                    errorMessage = ex.Message;
                }
                return Json(new { error = errorMessage }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}