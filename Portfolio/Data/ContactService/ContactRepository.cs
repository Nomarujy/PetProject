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

        public void Add(ContactWithMe contact)
        {
            _databaseContext.contactWithMe.Add(contact);
        }

        public ContactWithMe[] GetFirst(int Count)
        {
            return _databaseContext.contactWithMe.OrderBy(c => c.Id).Take(10).ToArray();
        }

        public ContactWithMe[] GetLast(int Count)
        {
            return _databaseContext.contactWithMe.OrderByDescending(c => c.Id).Take(10).ToArray();
        }
    }
}
