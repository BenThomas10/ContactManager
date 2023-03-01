using ContactManagerDataBusiness.Models;
using Microsoft.AspNetCore.Mvc;
using ContactManagerDataBusiness.Services;
using ContactManagerDataBusiness.Interfaces;

namespace ContactManagerStarter.Controllers
{
    public class ContactsController : Controller
    {
        private readonly ILogger? _logger;
        private readonly IEmailNotificationService? _emailNotificationService;
        private readonly ContactsService _contactService;

        public ContactsController(ContactsService contactService, ILogger<ContactsController>? logger = null, IEmailNotificationService? emailNotificationService = null)
        {
            _contactService = contactService;
            _logger = logger;
            _emailNotificationService = emailNotificationService;
        }

        public async Task<IActionResult> DeleteContact(Guid id)
        {
            try
            {
                await _contactService.DeleteContact(id);
                return Ok();
            }
            catch(Exception ex)
            {
                _logger.LogError($"An error occured while deleting a contact in {nameof(ContactsController)}.{nameof(DeleteContact)}, Message: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> EditContact(Guid id)
        {
            try
            {
                var viewModel = await _contactService.GetEditContact(id);
                return PartialView("_EditContact", viewModel);
            }
            catch  (Exception ex)
            {
                _logger.LogError($"An error occured while editing a contact in {nameof(ContactsController)}.{nameof(EditContact)}, Message: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> GetContacts()
        {
            
            try
            {
                var contactList = await _contactService.GetContacts();
                return PartialView("_ContactTable", contactList);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occured while getting contacts in {nameof(ContactsController)}.{nameof(GetContacts)}, Message: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NewContact()
        {
            return PartialView("_EditContact", new EditContactViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> SaveContact([FromBody]SaveContactViewModel model)
        {
            try
            {
                await _contactService.SaveContact(model);
                return Ok();
            }
            catch(Exception ex)
            {
                _logger.LogError($"An error occured while saving a contact in {nameof(ContactsController)}.{nameof(SaveContact)}, Message: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        private void SendEmailNotification(Guid contactId)
        {
            try
            {
                _emailNotificationService.SendEmailNotification(contactId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error sending email notification in {nameof(ContactsController)}.{nameof(SendEmailNotification)}, Message: {ex.Message}");
            }
        }
    }
}