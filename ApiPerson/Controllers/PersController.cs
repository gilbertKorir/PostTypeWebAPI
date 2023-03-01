using ApiPerson.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace ApiPerson.Controllers
{
    [RoutePrefix("api/person")]
    public class PersController : ApiController
    {
        SqlConnection _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString);

        public string _conn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;

        [Route("Add")]
        [HttpPost]
        public IHttpActionResult AddPerson(Person obj_person)
        {
            //string json;
            string msg = "";
            if(obj_person != null)
            {
                SqlCommand cmd = new SqlCommand("spAddPerson", _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", obj_person.Name);
                cmd.Parameters.AddWithValue("@Age", obj_person.Age);
                cmd.Parameters.AddWithValue("@Active", obj_person.Active);

                _connection.Open();

                int i = cmd.ExecuteNonQuery();

                if (i > 0)
                {
                    msg = "Data has been inserted successfully";
                }
                else
                {
                    msg = "Something is wring ";
                }
            }
            /*json = JsonConvert.SerializeObject(obj_person);
            var response = this.Request.CreateResponse(HttpStatusCode.Created);
            response.Headers.Location = new Uri(Request.RequestUri + obj_person.Id.ToString());
            response.Content = new StringContent(json, Encoding.UTF8, "application/json");*/
            return Ok(msg);
        } 
        
        [Route("Fetch")]
        [HttpGet]
        public IHttpActionResult FetchPerson()
        {
            //string msg = "no data";
            SqlDataAdapter da = new SqlDataAdapter("spGetAll", _connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            da.Fill(dt);

            List<Person> lstPerson = new List<Person>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Person emp = new Person();
                    emp.Name = dt.Rows[i]["Name"].ToString();
                    emp.Id = Convert.ToInt32(dt.Rows[i]["Id"].ToString());
                    emp.Age = Convert.ToInt32(dt.Rows[i]["Age"].ToString());
                    emp.Active = Convert.ToInt32(dt.Rows[i]["Active"].ToString());
                    lstPerson.Add(emp);
                }
            }
            if (lstPerson.Count > 0)
            {
                return Json(lstPerson);
            }
            else
            {
                return null;
                
            }
            
        }
        
        [Route("Edit")]
        [HttpPost]
        public IHttpActionResult EditPerson(Person person)
        {
            string msg = "";
            if (person != null)
            {
                SqlCommand cmd = new SqlCommand("spUpdate", _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", person.Id);
                cmd.Parameters.AddWithValue("@Name", person.Name);
                cmd.Parameters.AddWithValue("@Age", person.Age);
                cmd.Parameters.AddWithValue("@Active", person.Active);

                _connection.Open();
                int i = cmd.ExecuteNonQuery();

                if (i > 0)
                {
                    msg = "Data has been Updated";
                }
                else
                {
                    msg = "Error";
                }
            }
            return Ok(msg);
        }
         
        
        [Route("Delete")]
        [HttpPost]
        public IHttpActionResult DeletePerson(Person person)
        {
            string msg = "";
            SqlCommand cmd = new SqlCommand("spDeletePerson", _connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", person.Id);

            _connection.Open();
            int i = cmd.ExecuteNonQuery();

            if (i > 0)
            {
                msg = "Data has been Deleted";
            }
            else
            {
                msg = "Error";
            }

            return Ok(msg);
        }

    }
}






























/*[Route("Find")]
[HttpPost]
public IHttpActionResult GetById(Person person)
{
    using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString))
    {
        connection.Open();

        using (var command = new SqlCommand("spFindById", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", person.Id);

            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    // Maping the record to your model class
                    var record = new Person()
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"].ToString(),
                        Age = (int)reader["Age"],
                        Active = (int)reader["Active"]

                    };

                    return Ok(record);
                }
                else
                {
                    return NotFound();
                }
            }
        }
    }
}*/














































/*
        [Route("GetCustomers")]
        [HttpGet]
        public IHttpActionResult GetCustomers()
        {
            using (var connection = new SqlConnection(_conn))
            {
                var command = new SqlCommand("spGetAll", connection);
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();

                var dataReader = command.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(dataReader);

                return Json(dataTable);
            }
        }*/