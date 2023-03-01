using FetchPerson.Models;
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
    public class AccountsController : Controller
    {

        HttpClient client = new HttpClient();

        // GET: Accounts
        public ActionResult AccountsAction()
        {
            return View();
        }

        [HttpPost]
        public async Task<List<AccountsModel>> GetNames()
        {
            client = new HttpClient();
            var response = await client.GetAsync("https://localhost:44368/api/person/getnames");
            var names = await response.Content.ReadAsStringAsync();
            var apiRes = JsonConvert.DeserializeObject<List<AccountsModel>>(names);

            return apiRes;

        }

        [HttpPost]
        public ActionResult GetAllNames()
        {
            List<AccountsModel>_accounts = new List<AccountsModel>();
            HttpResponseMessage response = client.GetAsync("https://localhost:44368/api/person/getnames").Result;
            if (response.IsSuccessStatusCode)
            {
                string res = response.Content.ReadAsStringAsync().Result;
                _accounts = JsonConvert.DeserializeObject<List<AccountsModel>>(res);
            }
            return Json(_accounts);
        }

    }
}