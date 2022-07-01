using Microsoft.EntityFrameworkCore;
using ContactsExampleApp.Repositories.Abstract;
using ContactsExampleApp.Model.Database;

namespace ContactsExampleApp.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly ContactsDBContext dbContext;

        public ContactRepository(ContactsDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Contact?> CreateContact(Contact contact)
        {
            dbContext.Contacts.Add(contact);
            int result = await dbContext.SaveChangesAsync();

            if(result > 0)
            {
                return contact;
            }

            return null;
        }

        public async Task DeleteContact(int contactId)
        {
            Contact? contact = await dbContext.Contacts.Where(c => c.ContactId == contactId).SingleOrDefaultAsync();
            
            if(contact != null)
            {
                dbContext.Remove(contact);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<IList<Contact>> GetAllContacts()
        {
            return await dbContext.Contacts.OrderBy(c => c.LastName).ToListAsync();
        }

        public async Task<Contact?> GetContact(int contactId)
        {
            return await dbContext.Contacts.Where(c => c.ContactId == contactId).SingleOrDefaultAsync();
        }

        public async Task<Contact?> UpdateContact(Contact contact)
        {
            Contact? oldContact = dbContext.Contacts.Where(c => c.ContactId == contact.ContactId).SingleOrDefault();

            if(oldContact != null)
            {
                dbContext.Entry(oldContact).State = EntityState.Detached;
                dbContext.Contacts.Update(contact);
                await dbContext.SaveChangesAsync();
            }

            return null;
        }
    }
}
