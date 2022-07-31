using Portfolio.Models;

namespace Portfolio.Data.ContactService
{
    public interface IContactRepository
    {
        ContactWithMe[] GetFirst(int Count);
        ContactWithMe[] GetLast(int Count);

        void Add(ContactWithMe contact);
    }
}