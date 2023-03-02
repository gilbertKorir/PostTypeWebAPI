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
using System.Runtime.InteropServices;
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

        [Route("GetIds")]
        [HttpGet]
        public IHttpActionResult FetchName()
        {
            //string msg = "no data";
            SqlDataAdapter da = new SqlDataAdapter("spGetIds", _connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            da.Fill(dt);

            List<Person> lstPerson = new List<Person>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Person emp = new Person();
                    emp.Id = Convert.ToInt32(dt.Rows[i]["Id"].ToString());
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

        [Route("AddAccount")]
        [HttpPost]
        public IHttpActionResult AddAccount(AccountsModel accountsModel) {
            string msg = "";
            if(accountsModel != null)
            {
                SqlCommand cmd = new SqlCommand("spAddAccount", _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AccountName", accountsModel.AccountName);
                cmd.Parameters.AddWithValue("@KycId", accountsModel.KycId);
                cmd.Parameters.AddWithValue("@Active", accountsModel.Active);

                _connection.Open();
                int i = cmd.ExecuteNonQuery();

                if (i > 0)
                {
                    msg = "Account has been added";
                }
                else
                {
                    msg = "Error in adding an account";
                }
            }
            return Ok(msg);
        }

        [Route("FetchAccount")]
        [HttpGet]
       public IHttpActionResult GetAccounts()
        {
            string msg = "";
            SqlDataAdapter da = new SqlDataAdapter("spGetAccounts", _connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            da.Fill(dt);

            List<AccountsModel> lstAccounts = new List<AccountsModel>();
            if(dt.Rows.Count > 0)
            {
                for(int i = 0; i < dt.Rows.Count; i++)
                {
                    AccountsModel acc = new AccountsModel();
                    acc.AccountName = dt.Rows[i]["AccountName"].ToString();
                    acc.KycId = dt.Rows[i]["KycId"].ToString();
                    acc.Id = Convert.ToInt32(dt.Rows[i]["Id"].ToString());
                    acc.Active = Convert.ToInt32(dt.Rows[i]["Active"].ToString());
                    lstAccounts.Add(acc);
                }

                if(lstAccounts.Count> 0)
                {
                    return Ok(lstAccounts);
                }
                else
                {
                    msg = "Accounts not found";
                }
            }
            return Ok(msg);
        }

        [Route("UpdateAccount")]
        [HttpPost]
        public IHttpActionResult UpdateAccounts(AccountsModel accountsModel)
        {
            string msg = "";
            if(accountsModel != null)
            {
                SqlCommand cmd = new SqlCommand("spUpdateUserAccount", _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", accountsModel.Id);
                cmd.Parameters.AddWithValue("@AccountName", accountsModel.AccountName);
                cmd.Parameters.AddWithValue("@KycId", accountsModel.KycId);
                cmd.Parameters.AddWithValue("@Active", accountsModel.Active);

                _connection.Open();
                int i = cmd.ExecuteNonQuery();

                if (i > 0)
                {
                    msg = "Account updated successfully";
                }
                else
                {
                    msg = "Failed to Update the Account";
                }
            }

            return Ok(msg);
        }

        [Route("DeleteAccount")]
        [HttpPost]
        public IHttpActionResult DeleteAccount(AccountsModel accountsModel)
        {
            string msg = "";
            SqlCommand cmd = new SqlCommand("spDeleteUserAccount", _connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", accountsModel.Id);

            _connection.Open();
            int i = cmd.ExecuteNonQuery();
            if(i > 0) {
                msg = "Account Has been deleted";
            }
            else
            {
                msg = "Error deleting the Account";
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