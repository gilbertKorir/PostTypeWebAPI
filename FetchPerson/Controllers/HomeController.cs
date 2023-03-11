using FetchPerson.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace FetchPerson.Controllers
{
    public class HomeController : Controller
    {
        HttpClient client = new HttpClient();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        [HttpPost]
        public ActionResult GetAllEmployees()
        {
           
           List<PersonModel> _persons = new List<PersonModel>();
            HttpResponseMessage response = client.GetAsync("https://localhost:44368/api/person/fetch").Result;
            if (response.IsSuccessStatusCode)
            {
                string res = response.Content.ReadAsStringAsync().Result;
                _persons = JsonConvert.DeserializeObject<List<PersonModel>>(res);
            }
            return Json(_persons);
        }

    }
}