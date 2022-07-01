using ContactsExampleApp.Model.View;
using ContactsExampleApp.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactsExampleApp.Controllers
{
    [Route("api/contacts")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService contactService;

        public ContactController(IContactService contactService)
        {
            this.contactService = contactService;
        }

        [HttpGet]
        public async Task<IList<ContactDisplayData>> GetAllContacts()
        {
            return await contactService.GetAllContacts();
        }

        [HttpGet("{contactId}")]
        public async Task<ContactDisplayData?> GetContact(int contactId)
        {
            return await contactService.GetContact(contactId);
        }

        [HttpPost]
        public async Task<ContactDisplayData?> CreateContact(ContactDisplayData contact)
        {
            return await contactService.CreateContact(contact);
        }

        [HttpPut]
        public async Task<ContactDisplayData?> UpdateContact(ContactDisplayData contact)
        {
            return await contactService.UpdateContact(contact);
        }

        [HttpDelete("{contactId}")]
        public async Task DeleteContact(int contactId)
        {
            await contactService.DeleteContact(contactId);
        }
    }
}
