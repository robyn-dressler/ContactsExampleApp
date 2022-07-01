using ContactsExampleApp.Model.Database;
using ContactsExampleApp.Model.View;
using ContactsExampleApp.Repositories.Abstract;
using ContactsExampleApp.Services.Abstract;

namespace ContactsExampleApp.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            this.contactRepository = contactRepository;
        }

        public async Task<ContactDisplayData?> CreateContact(ContactDisplayData contact)
        {
            Contact? resultContact = await contactRepository.CreateContact(ToContact(contact));
            return ToContactDisplay(resultContact);
        }

        public async Task DeleteContact(int contactId)
        {
            await contactRepository.DeleteContact(contactId);
        }

        public async Task<IList<ContactDisplayData>> GetAllContacts()
        {
            var contacts = await contactRepository.GetAllContacts();
            return contacts.Select(c => ToContactDisplay(c)!).ToList();
        }

        public async Task<ContactDisplayData?> GetContact(int contactId)
        {
            return ToContactDisplay(await contactRepository.GetContact(contactId));
        }

        public async Task<ContactDisplayData?> UpdateContact(ContactDisplayData contact)
        {
            Contact? resultContact = await contactRepository.UpdateContact(ToContact(contact));
            return ToContactDisplay(resultContact);
        }

        private ContactDisplayData? ToContactDisplay(Contact? contact)
        {
            if (contact == null)
            {
                return null;
            }

            //Calculate age
            int? age = null;
            if (contact.DateOfBirth.HasValue)
            {
                DateTime birthDate = contact.DateOfBirth.Value;
                DateTime currDate = DateTime.Now;

                age = currDate.Year - birthDate.Year;
                if (currDate.Month < birthDate.Month || (currDate.Month == birthDate.Month && currDate.Day < birthDate.Day))
                {
                    age--;
                }
            }

            return new ContactDisplayData
            {
                ContactId = contact.ContactId,
                DisplayName = $"{contact.LastName}, {contact.FirstName}",
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                EmailAddress = contact.EmailAddress,
                PhoneNumber = contact.PhoneNumber,
                HomeAddress = contact.HomeAddress,
                DateOfBirth = contact.DateOfBirth,
                Age = age,
                Notes = contact.Notes
            };
        }

        private Contact ToContact(ContactDisplayData contactDisplay)
        {
            return new Contact
            {
                ContactId = contactDisplay.ContactId,
                FirstName = contactDisplay.FirstName,
                LastName = contactDisplay.LastName,
                EmailAddress = contactDisplay.EmailAddress,
                PhoneNumber = contactDisplay.PhoneNumber,
                HomeAddress = contactDisplay.HomeAddress,
                DateOfBirth = contactDisplay.DateOfBirth,
                Notes = contactDisplay.Notes
            };
        }
    }
}
