using ContactsExampleApp.Model.Database;

namespace ContactsExampleApp.Repositories.Abstract
{
    public interface IContactRepository
    {
        Task<IList<Contact>> GetAllContacts();
        Task<Contact?> GetContact(int contactId);
        Task<Contact?> CreateContact(Contact contact);
        Task<Contact?> UpdateContact(Contact contact);
        Task DeleteContact(int contactId);
    }
}
