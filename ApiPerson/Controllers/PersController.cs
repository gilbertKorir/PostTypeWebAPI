using ApiPerson.Models;
using Newtonsoft.Json;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Lifetime;
using System.Security.Principal;
using System.Text;
using System.Web.DynamicData;
using System.Web.Http;
using System.Xml.Linq;

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
            if (obj_person != null)
            {
                SqlCommand cmd = new SqlCommand("spAddEmployee", _connection);
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
            SqlDataAdapter da = new SqlDataAdapter("spGetAllEmployees", _connection);
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
                SqlCommand cmd = new SqlCommand("spUpdateEmployee", _connection);
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
            SqlCommand cmd = new SqlCommand("spDeleteEmployee", _connection);
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
        public IHttpActionResult FetchIds()
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
                    emp.Name = dt.Rows[i]["Name"].ToString();
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

        //getting kycid
        [Route("GetKycIds")]
        [HttpGet]
        public IHttpActionResult FetchKycid()
        {
            // string msg = "no data";
            SqlDataAdapter da = new SqlDataAdapter("spGetKycid", _connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            da.Fill(dt);

            List<AccountsModel> lstPerson = new List<AccountsModel>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    AccountsModel account = new AccountsModel();
                    account.KycId = Convert.ToInt32(dt.Rows[i]["KycId"].ToString());
                    lstPerson.Add(account);
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
        public IHttpActionResult AddAccount(AccountsModel accountsModel)
        {
            string msg = "";
            if (accountsModel != null)
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
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    AccountsModel acc = new AccountsModel();
                    acc.AccountName = dt.Rows[i]["AccountName"].ToString();
                    acc.KycId = Convert.ToInt32(dt.Rows[i]["KycId"].ToString());
                    acc.Id = Convert.ToInt32(dt.Rows[i]["Id"].ToString());
                    acc.Active = Convert.ToInt32(dt.Rows[i]["Active"].ToString());
                    lstAccounts.Add(acc);
                }

                if (lstAccounts.Count > 0)
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
            if (accountsModel != null)
            {
                SqlCommand cmd = new SqlCommand("spUpdateAccount", _connection);
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
            SqlCommand cmd = new SqlCommand("spDeleteAccount", _connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", accountsModel.Id);

            _connection.Open();
            int i = cmd.ExecuteNonQuery();
            if (i > 0)
            {
                msg = "Account Has been deleted";
            }
            else
            {
                msg = "Error deleting the Account";
            }

            return Ok(msg);
        }



        [Route("FetchAccNames")]
        [HttpGet]
        public IHttpActionResult FetchAccName(AccountsModel accounts)
        {

            using (SqlConnection connection = new SqlConnection(_conn))
            {
                SqlCommand command = new SqlCommand("spSelectAccounts", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@KycId", accounts.KycId);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                List<AccountsModel> items = new List<AccountsModel>();


                while (reader.Read())
                {
                    AccountsModel item = new AccountsModel();
                    // item.KycId = Convert.ToInt32(reader["KycId"].ToString());
                    item.Id = Convert.ToInt32(reader["Id"].ToString());
                    item.AccountName = reader["AccountName"].ToString();
                    items.Add(item);
                }

                return Json(items);
            }
        }

        [Route("Accsd/{id}")]
        [HttpGet]
        public IHttpActionResult GetAccounts(int id)
        {
            List<AccountsModel> accounts = new List<AccountsModel>();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                using (SqlCommand cmd = new SqlCommand("spSelectAccounts", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@KycId", id);

                    con.Open();

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var account = new AccountsModel
                            {
                                Id = rdr.GetInt32(0),
                                AccountName = rdr.GetString(1)
                            };
                            accounts.Add(account);
                        }
                    }
                }
            }
            return Json(accounts);
        }


        //TRANSACTIONS
        [HttpPost]
        [Route("AddTransaction")]
        public IHttpActionResult AddTransation(Transactions transactions)
        {
            string msg = "";
                if (transactions != null)
                {
                //    SqlCommand cmd = new SqlCommand("spAddtransactions", _connection);
                //    cmd.CommandType = CommandType.StoredProcedure;
                //   // cmd.Parameters.AddWithValue("@TransId", transactions.TransId);
                //    cmd.Parameters.AddWithValue("@AccountNo", transactions.AccountNo);
                //    cmd.Parameters.AddWithValue("@Type", transactions.Type);
                //    cmd.Parameters.AddWithValue("@TransDate", transactions.TransDate);
                //    cmd.Parameters.AddWithValue("@Amount", transactions.Amount);

                //_connection.Open();
                //int i = cmd.ExecuteNonQuery();
                //if (i > 0)
                //{
                //    msg = "Transaction Done successfully";
                //}
                //else
                //{
                //    msg = "Cannot perform the transaction";
                //}

                  DataTable dt = new DataTable();
                  using (SqlDataAdapter da = new SqlDataAdapter())
                  {
                      da.SelectCommand = new SqlCommand("spAddtransactions", _connection);
                      da.SelectCommand.CommandType = CommandType.StoredProcedure;
                      da.SelectCommand.Parameters.AddWithValue("Amount", transactions.Amount);
                      da.SelectCommand.Parameters.AddWithValue("AccountNo", transactions.AccountNo);
                      da.SelectCommand.Parameters.AddWithValue("Type", transactions.Type);
                      da.SelectCommand.Parameters.AddWithValue("TransDate", transactions.TransDate);

                    da.Fill(dt);
                  }

                return Json(dt);
                 //if (dt.Rows.Count == 0)
                 // {
                 //     // There was an error with the stored procedure
                 //     return InternalServerError(new Exception("Error message goes here."));
                 // }
                 // else
                 // {
                 //     return Ok(dt);
                 // }
            }
                return Ok(msg);
         
        }


        //fetch transactions
        [Route("FetchTransactions")]
        [HttpGet]
        public IHttpActionResult FetchTransactions()
        {
            string msg = "";
            SqlDataAdapter da = new SqlDataAdapter("spSelectTransactions", _connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            da.Fill(dt);

            List<Transactions> transactions = new List<Transactions>();
            if (dt.Rows.Count > 0)
            {
                // dt.Columns["TransDate"].DataType = typeof(DateTime);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Transactions transaction = new Transactions();
                   // transaction.TransId = Convert.ToInt32(dt.Rows[i]["TransId"].ToString());
                    transaction.AccountNo = Convert.ToInt32(dt.Rows[i]["AccountNo"].ToString());
                    transaction.Type = dt.Rows[i]["Type"].ToString();

                    DateTime dateValue = Convert.ToDateTime(dt.Rows[i]["TransDate"]);
                    transaction.TransDate = dateValue.ToString("yyyy-MM-dd");

                    transaction.Amount = Convert.ToDouble(dt.Rows[i]["Amount"].ToString());
                    transactions.Add(transaction);
                }
                if (transactions.Count > 0)
                {
                    msg = "successfully fetched the transactions";
                    return Ok(transactions);
                }
                else
                {
                    msg = "Failed to fetch any transaction done";
                }
            }

            return Ok(msg);
        }
    }

}













































































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