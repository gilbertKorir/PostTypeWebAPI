using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FetchPerson.Models
{
    public class AccountsModel
    {
        public int Id { get; set; }
        public string AccountName { get; set; }
        public int KycId { get; set; }
        public int Active { get; set; }
    }
}