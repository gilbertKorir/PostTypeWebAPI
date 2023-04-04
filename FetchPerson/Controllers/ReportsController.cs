using FetchPerson.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        [HttpPost]
        public ActionResult GetCashStatement(int id, DateTime startDate, DateTime endDate)
        {
            //string msg = "";
            List<Transactions> transactions = new List<Transactions>();
            HttpResponseMessage response = client.GetAsync($"https://localhost:44368/api/person/getstatement/{id}/{startDate}/{endDate}").Result;

            if (response.IsSuccessStatusCode)
            {
                string content = response.Content.ReadAsStringAsync().Result;
                 transactions = JsonConvert.DeserializeObject<List<Transactions>>(content);
            }

           
            // Deserialize the response content to your cash statement model
           

            return Json(transactions);
        }
    }
}