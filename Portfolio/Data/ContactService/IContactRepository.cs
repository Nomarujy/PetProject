using Portfolio.Models;

namespace Portfolio.Data.ContactService
{
    public interface IContactRepository
    {
        Contact[] GetFirst(int Page, int Count);
        Contact[] GetLast(int Page, int Count);

        void Add(Contact contact);
    }
}