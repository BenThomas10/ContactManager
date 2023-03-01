using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactManagerDataBusiness.Data;

namespace ContactManagerDataBusiness.Models
{
    public class EditContactViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public List<EmailAddress> EmailAddresses { get; set; }
        public List<Address> Addresses { get; set; }
    }
}
