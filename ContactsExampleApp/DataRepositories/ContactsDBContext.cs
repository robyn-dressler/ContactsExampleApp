using ContactsExampleApp.Model.Database;
using Microsoft.EntityFrameworkCore;

namespace ContactsExampleApp.Repositories
{
    public class ContactsDBContext : DbContext
    {
        public ContactsDBContext(DbContextOptions options) : base(options) { }

        public DbSet<Contact> Contacts { get; set; }
    }
}
