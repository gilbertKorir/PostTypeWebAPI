using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

namespace ApiPerson.Models
{
    public class Transactions
    {
        public int TransId { get; set; }
        public int AccountNo { get; set; }
        public string Type { get; set; }
        public string TransDate { get; set; }
        public double Amount { get; set; }
    }
}