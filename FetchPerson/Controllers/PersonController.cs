using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FetchPerson.Models;
using Newtonsoft.Json;

namespace FetchPerson.Controllers
{
    public class PersonController : Controller
    {
        PersonModel _person = new PersonModel();
        List<PersonModel> _persons = new List<PersonModel>();
        HttpClient client = new HttpClient();

        HttpClientHandler clienthandler = new HttpClientHandler();
        public PersonController()
        {
            clienthandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, SslPolicyErrors) => { return true; };
        }
        // GET: Pers
        public ActionResult PersonAction()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetAllEmployees()
        {
            _persons = new List<PersonModel>();
            HttpResponseMessage response = client.GetAsync("https://localhost:44368/api/person/fetch").Result;
            if (response.IsSuccessStatusCode)
            {
                string res = response.Content.ReadAsStringAsync().Result;
                _persons = JsonConvert.DeserializeObject<List<PersonModel>>(res);
            }
            return Json(_persons);
        }

        [HttpPost]
        public  async Task<JsonResult> AddEmployee(PersonModel personModel)
        {

            var content = new StringContent(JsonConvert.SerializeObject(personModel), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:44368/api/person/add", content);

            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }


        [HttpPost]
        public  async Task<JsonResult> UpdateEmp(PersonModel person)
        {
            var content = new StringContent(JsonConvert.SerializeObject(person), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:44368/api/person/edit", content);

            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}






































/*
[HttpPost]
public async Task<PersonModel> AddEmp(PersonModel personModel)
{
    _person = new PersonModel();
    using (var client = new HttpClient())
    {
        StringContent empContent = new StringContent(JsonConvert.SerializeObject(personModel), Encoding.UTF8, "application/json");
        using (var response = await client.PostAsync("https://localhost:44368/api/person/add", empContent))
        {
            string res = await response.Content.ReadAsStringAsync();
            _person = JsonConvert.DeserializeObject<PersonModel>(res);
        }
    }
    return _person;
}
*/



/*[HttpPost]
public async Task<List<Person>> GetAl()
{
    _persons = new List<Person>();
    using (var client = new HttpClient(clienthandler))
    {
        using (var res = await client.GetAsync("https://localhost:44368/api/person/getcustomers"))
        {
            string apiRes = await res.Content.ReadAsStringAsync();
            _persons = JsonConvert.DeserializeObject<List<Person>>(apiRes);
        }
    }
    return _persons;
}*/
/*
[HttpPost]
public ActionResult find()
{
    List<Person> people = new List<Person>();

    HttpClient client = new HttpClient();
    HttpResponseMessage response = client.GetAsync("https://localhost:44368/api/person/fetch").Result;

    if (response.IsSuccessStatusCode)
    {
        string content = response.Content.ReadAsStringAsync().Result;
        people = JsonConvert.DeserializeObject<List<Person>>(content);
    }
    return Json(people);
}*/