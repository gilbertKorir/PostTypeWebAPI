using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiPerson.Models
{
    public class StatementModel
    {
        public int RowId { get; set; }
        public string Type { get; set; }
        public string TransDate { get; set; }
        public Decimal Credit { get; set; }
        public Decimal Amount { get; set; }
        public Decimal Debit { get; set; }
        public Decimal Balance { get; set; }


    }
}