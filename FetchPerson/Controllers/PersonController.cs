using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
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
                //return Json(new { success = true });
                return Json(response);
            }
            //return Json(new { success = false });
            return Json(response);
        }

        public ActionResult GetEmployeeReport()
        {
            // Call the API to get the data and deserialize it to a list of PersonModel objects
            List<PersonModel> persons = new List<PersonModel>();
            HttpResponseMessage response = client.GetAsync("https://localhost:44368/api/person/fetch").Result;
            if (response.IsSuccessStatusCode)
            {
                string res = response.Content.ReadAsStringAsync().Result;
                persons = JsonConvert.DeserializeObject<List<PersonModel>>(res);
            }

            // Create a new DataTable instance and add columns
            DataTable dataTable = new DataTable("EmployeeTable");
            dataTable.Columns.Add("Id", typeof(int));
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("Age", typeof(int));
            dataTable.Columns.Add("Active", typeof(int));

            // Add data to the DataTable
            foreach (PersonModel person in persons)
            {
                DataRow row = dataTable.NewRow();
                row["Id"] = person.Id;
                row["Name"] = person.Name;
                row["Age"] = person.Age;
                row["Active"] = person.Active;
                dataTable.Rows.Add(row);
            }

            // Create a new report document instance
            ReportDocument rd = new ReportDocument();

            // Set the report source to the DataTable
            rd.SetDataSource(dataTable);

            // Export the report to a stream and return it as a PDF file
            Stream stream = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            return File(stream, "application/pdf", "Employeereport.pdf");

        }

        public ActionResult GenerateReport()
        {
                // Retrieve data from the Web API
                HttpResponseMessage response = client.GetAsync("https://localhost:44368/api/person/fetch").Result;
                if (response.IsSuccessStatusCode)
                {
                    string res = response.Content.ReadAsStringAsync().Result;
                    List<PersonModel> persons = JsonConvert.DeserializeObject<List<PersonModel>>(res);

                    // Create an instance of the Crystal Report
                    ReportDocument report = new ReportDocument();
                    report.Load(Server.MapPath("D:/C-SHARP-projects/ApiPerson/FetchPerson/Reporting/EmployeeReport.rpt"));
                   // report.Load(Server.MapPath("~/Reporting/EmployeeReport.rpt"));

                    // Set the report data source
                    report.SetDataSource(persons);

                    // Set export options
                    ExportOptions options = new ExportOptions();
                    options.ExportFormatType = ExportFormatType.PortableDocFormat;
                    options.ExportDestinationType = ExportDestinationType.DiskFile;
                    options.DestinationOptions = new DiskFileDestinationOptions
                    {
                        DiskFileName = Server.MapPath("~/Reporting/EmployeeReport.pdf")
                    };

                //ReportDocumentOptions reportOptions = report.ReportOptions;
                //reportOptions.ExportEncodingType = ExportEncodingType.NoEncoding;
                // Export the report to PDF
                report.Export(options);

                    // Dispose the report object
                    report.Close();
                    report.Dispose();
                }

                return RedirectToAction("PersonAction"); // or any other appropriate action
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