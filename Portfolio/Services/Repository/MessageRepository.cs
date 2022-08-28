using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<MessageModel>> Get(int page = 0, int count = 10)
        {
            return await _database.Messages.AsNoTracking()
                .Take(count).Skip(page * count).ToListAsync();
        }
    }
}
