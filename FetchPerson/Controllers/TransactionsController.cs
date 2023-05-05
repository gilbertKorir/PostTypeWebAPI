using FetchPerson.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
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
             string myAPI = "https://localhost:44368/api/person/addtransaction";
             string strPayLoad = JsonConvert.SerializeObject(transactions);
             string response = "";

             HttpWebRequest client = (HttpWebRequest)WebRequest.Create(myAPI);
             client.Method = "POST";
             client.ContentType = "application/json";
             client.ContentLength = strPayLoad.Length;
             client.Timeout = 600000;
             using (Stream webStream = client.GetRequestStream())
             using (StreamWriter requestWriter = new StreamWriter(webStream, Encoding.ASCII))
             {
                 requestWriter.Write(strPayLoad);
             }

             try
             {
                 WebResponse webResponse = client.GetResponse();
                 using (Stream webStream = webResponse.GetResponseStream())
                 {
                     if (webStream != null)
                     {
                         using (StreamReader responseReader = new StreamReader(webStream))
                         {
                             response = responseReader.ReadToEnd();
                         }
                     }
                 } 

                 return Json(response); 
             }catch{
                 return null;
             }
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

        //getCurrent balance
        [HttpPost]
        public ActionResult CurrentBalance(int id)
        {
            string msg = "";
            using (HttpClient client = new HttpClient())
            {
                string url = $"https://localhost:44368/api/person/balance/{id}";
                HttpResponseMessage response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseData = response.Content.ReadAsStringAsync().Result;
                    return Json(responseData);
                }
            }
            return Json(msg);

        }
    }
}


