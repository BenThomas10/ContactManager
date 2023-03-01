using ContactManagerDataBusiness.Models;

namespace ContactManagerDataBusiness.Interfaces
{
    public interface IContactsService
    {
        Task<ContactViewModel> GetContacts();
        Task DeleteContact(Guid id);
        Task<EditContactViewModel> GetEditContact(Guid id);
        Task SaveContact(SaveContactViewModel model);
    }
}
