using ContactManagerDataBusiness.Data;
using ContactManagerDataBusiness.Hubs;
using ContactManagerDataBusiness.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ContactManagerDataBusiness.Interfaces;

namespace ContactManagerDataBusiness.Services
{
    public class ContactsService : IContactsService
    {
        private readonly ApplicationContext _context;
        private readonly IHubContext<ContactHub> _hubContext;
        private readonly ILogger _logger;
        private readonly IEmailNotificationService _emailNotificationService;

        public ContactsService(ApplicationContext context, IHubContext<ContactHub> hubContext,
            ILogger<ContactsService> logger, IEmailNotificationService emailNotificationService)
        {
            _context = context;
            _hubContext = hubContext;
            _logger = logger;
            _emailNotificationService = emailNotificationService;
        }

        public async Task<ContactViewModel> GetContacts()
        {
            var contactList = await _context.Contacts
                .OrderBy(x => x.FirstName)
                .ToListAsync();

            return new ContactViewModel { Contacts = contactList };
        }

        public async Task DeleteContact(Guid id)
        {
            var contactToDelete = await _context.Contacts
                .Include(x => x.EmailAddresses)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (contactToDelete == null)
            {
                throw new ArgumentException($"Contact with ID {id} not found");
            }

            _context.EmailAddresses.RemoveRange(contactToDelete.EmailAddresses);
            _context.Contacts.Remove(contactToDelete);

            await _context.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("Update");
        }

        public async Task<EditContactViewModel> GetEditContact(Guid id)
        {
            var contact = await _context.Contacts
                .Include(x => x.EmailAddresses)
                .Include(x => x.Addresses)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (contact == null)
            {
                throw new ArgumentException($"Contact with ID {id} not found");
            }

            return new EditContactViewModel
            {
                Id = contact.Id,
                Title = contact.Title,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                DOB = contact.DOB,
                EmailAddresses = contact.EmailAddresses,
                Addresses = contact.Addresses
            };
        }

        public async Task SaveContact(SaveContactViewModel model)
        {
            var contact = model.ContactId == Guid.Empty
                ? new Contact { Title = model.Title, FirstName = model.FirstName, LastName = model.LastName, DOB = model.DOB }
                : await _context.Contacts.Include(x => x.EmailAddresses).Include(x => x.Addresses).FirstOrDefaultAsync(x => x.Id == model.ContactId);

            if (contact == null)
            {
                throw new ArgumentException($"Contact with ID {model.ContactId} not found");
            }

            _context.EmailAddresses.RemoveRange(contact.EmailAddresses);
            _context.Addresses.RemoveRange(contact.Addresses);

            foreach (var email in model.Emails)
            {
                contact.EmailAddresses.Add(new EmailAddress
                {
                    Type = email.Type,
                    Email = email.Email,
                    IsPrimary = email.IsPrimary,
                    Contact = contact
                });
            }

            foreach (var address in model.Addresses)
            {
                contact.Addresses.Add(new Address
                {
                    Street1 = address.Street1,
                    Street2 = address.Street2,
                    City = address.City,
                    State = address.State,
                    Zip = address.Zip,
                    Type = address.Type
                });
            }

            contact.Title = model.Title;
            contact.FirstName = model.FirstName;
            contact.LastName = model.LastName;
            contact.DOB = model.DOB;

            if (model.ContactId == Guid.Empty)
            {
                await _context.Contacts.AddAsync(contact);
            }
            else
            {
                _context.Contacts.Update(contact);
            }

            await _context.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("Update");

            _emailNotificationService.SendEmailNotification(contact.Id);
        }
    }
}


