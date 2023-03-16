using FetchPerson.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FetchPerson.Controllers
{
    public class TransactionsController : Controller
    {
        HttpClient client = new HttpClient();

        public TransactionsController()
        {
            client = new HttpClient();
        }
        // GET: Transactions
        public ActionResult TransactionsAction()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetAllKycid()
        {
            List<NamesModel> _names = new List<NamesModel>();
            HttpResponseMessage response = client.GetAsync("https://localhost:44368/api/person/getids").Result;
            if (response.IsSuccessStatusCode)
            {
                string res = response.Content.ReadAsStringAsync().Result;
                _names = JsonConvert.DeserializeObject<List<NamesModel>>(res);
            }
            return Json(_names);
        }

        [HttpPost]
        public ActionResult GetAccountsForId(int id)
        {
            List<AccountsModel> accountNames = new List<AccountsModel>();

                using (HttpClient client = new HttpClient())
                {
                    string url = $"https://localhost:44368/api/person/accsd/{id}";
                    HttpResponseMessage response = client.GetAsync(url).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string responseData = response.Content.ReadAsStringAsync().Result;
                        accountNames = JsonConvert.DeserializeObject<List<AccountsModel>>(responseData);
                    }
                }
            return Json(accountNames);
        }

        //add a transaction
        [HttpPost]
        public ActionResult AddTransaction(Transactions transactions)
        {
            var content = new StringContent(JsonConvert.SerializeObject(transactions), Encoding.UTF8, "application/json");
            var response = client.PostAsync("https://localhost:44368/api/person/addtransaction", content);

            return Json(transactions);
        }
          
        
        //fetch all transactions
        [HttpPost]
        public ActionResult GetTransaction()
        {
            List<Transactions> _transactions = new List<Transactions>();
            HttpResponseMessage response = client.GetAsync("https://localhost:44368/api/person/fetchtransactions").Result;

            if (response.IsSuccessStatusCode)
            {
                string responseData = response.Content.ReadAsStringAsync().Result;
                _transactions = JsonConvert.DeserializeObject<List<Transactions>>(responseData);
            }
            return Json(_transactions);
        }

    }
}


