using Portfolio.Models.StartPage;

namespace Portfolio.Services.Repository
{
    public interface IMessageRepository
    {
        Task Add(MessageModel message);
    }
}