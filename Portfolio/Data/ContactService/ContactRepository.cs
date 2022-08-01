using Portfolio.Data.MainContext;
using Portfolio.Models;

namespace Portfolio.Data.ContactService
{
    public class ContactRepository : IContactRepository
    {
        private readonly DatabaseContext _databaseContext;

        public ContactRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public void Add(Contact contact)
        {
            _databaseContext.Contact.Add(contact);
            _databaseContext.SaveChanges();
        }

        public Contact[] GetFirst(int Page, int Count)
        {
            return _databaseContext.Contact.OrderBy(c => c.Id).Skip(Page * Count).Take(10).ToArray();
        }

        public Contact[] GetLast(int Page, int Count)
        {
            return _databaseContext.Contact.OrderByDescending(c => c.Id).Skip(Page * Count).Take(10).ToArray();
        }
    }
}
