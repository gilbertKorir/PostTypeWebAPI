using FetchPerson.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
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
        public async Task<JsonResult> AddAccount(AccountsModel accountsModel)
        {
            client = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(accountsModel), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:44368/api/person/addaccount", content);

            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }

        [HttpPost]
        public ActionResult GetAllIds()
        {
            List<NamesModel> _accounts = new List<NamesModel>();
            HttpResponseMessage response = client.GetAsync("https://localhost:44368/api/person/getids").Result;
            if (response.IsSuccessStatusCode)
            {
                string res = response.Content.ReadAsStringAsync().Result;
                _accounts = JsonConvert.DeserializeObject<List<NamesModel>>(res);
            }
            return Json(_accounts);
        }
        
        [HttpPost]
        public ActionResult GetAllAccounts()
        {
            List<AccountsModel> _accounts = new List<AccountsModel>();
            HttpResponseMessage response = client.GetAsync("https://localhost:44368/api/person/fetchaccount").Result;
            if (response.IsSuccessStatusCode)
            {
                string res = response.Content.ReadAsStringAsync().Result;
                _accounts = JsonConvert.DeserializeObject<List<AccountsModel>>(res);
            }
            return Json(_accounts);
        }

        [HttpPost]
        public async Task<JsonResult> UpdateAccounts(AccountsModel accountsModel)
        {
            client = new HttpClient();
            var content = new StringContent(JsonConvert.SerializeObject(accountsModel), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:44368/api/person/updateaccount", content);

            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }

        [HttpPost]
        public async Task<JsonResult> DeleteAccount()
        {
            client = new HttpClient();
            //StringContent content = new StringContent(JsonConvert.SerializeObject(accountsModel), Encoding.UTF8, "application/json");
            var response = await client.DeleteAsync("https://localhost:44368/api/person/deleteaccount");

            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }
    }
}