using ContactsExampleApp.Model.View;

namespace ContactsExampleApp.Services.Abstract
{
    public interface IContactService
    {
        Task<IList<ContactDisplayData>> GetAllContacts();
        Task<ContactDisplayData?> GetContact(int contactId);
        Task<ContactDisplayData?> CreateContact(ContactDisplayData contact);
        Task<ContactDisplayData?> UpdateContact(ContactDisplayData contact);
        Task DeleteContact(int contactId);
    }
}
