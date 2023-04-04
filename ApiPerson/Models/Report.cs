using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiPerson.Models
{
    public class Report
    {
        public int AccountNo { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
    }
}