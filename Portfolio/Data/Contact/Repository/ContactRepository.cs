using Portfolio.Data.Context;
using Portfolio.Models.Contact;

namespace Portfolio.Data.Contact.Repository
{
    public class ContactRepository : IContactRepository
    {
        private readonly DatabaseContext _databaseContext;

        public ContactRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public void Add(ContactModel contact)
        {
            _databaseContext.Contact.Add(contact);
            _databaseContext.SaveChanges();
        }

        public ContactModel[] GetFirst(int Page, int Count)
        {
            return _databaseContext.Contact.OrderBy(c => c.Id).Skip(Page * Count).Take(10).ToArray();
        }

        public ContactModel[] GetLast(int Page, int Count)
        {
            return _databaseContext.Contact.OrderByDescending(c => c.Id).Skip(Page * Count).Take(10).ToArray();
        }
    }
}
