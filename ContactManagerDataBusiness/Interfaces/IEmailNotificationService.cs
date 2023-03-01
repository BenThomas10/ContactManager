
namespace ContactManagerDataBusiness.Interfaces
{
    public interface IEmailNotificationService
    {
        void SendEmailNotification(Guid contactId);
    }
}
