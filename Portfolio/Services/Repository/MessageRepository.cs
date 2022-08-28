using Portfolio.Models;
using Portfolio.Models.StartPage;

namespace Portfolio.Services.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ApplicationDbContext _database;

        public MessageRepository(ApplicationDbContext database)
        {
            _database = database;
        }

        public async Task Add(MessageModel message)
        {
            _database.Messages.Add(message);
            await _database.SaveChangesAsync();
        }
    }
}
