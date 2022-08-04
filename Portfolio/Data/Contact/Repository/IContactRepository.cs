using Portfolio.Models.Contact;

namespace Portfolio.Data.Contact.Repository
{
    public interface IContactRepository
    {
        ContactModel[] GetFirst(int Page, int Count);
        ContactModel[] GetLast(int Page, int Count);

        void Add(ContactModel contact);
    }
}