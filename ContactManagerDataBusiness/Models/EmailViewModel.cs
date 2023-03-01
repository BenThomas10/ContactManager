using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactManagerDataBusiness.Data;

namespace ContactManagerDataBusiness.Models
{
    public class EmailViewModel
    {
        public EmailType Type { get; set; }
        public string Email { get; set; }
        public bool IsPrimary { get; set; }
    }
}
