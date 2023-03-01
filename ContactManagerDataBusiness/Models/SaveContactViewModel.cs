using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManagerDataBusiness.Models
{
    public class SaveContactViewModel
    {
        public Guid ContactId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public List<EmailViewModel> Emails { get; set; } = new List<EmailViewModel>();
        public List<AddressViewModel> Addresses { get; set; } = new List<AddressViewModel>();

    }
}
