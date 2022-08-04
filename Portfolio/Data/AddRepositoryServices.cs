using Microsoft.EntityFrameworkCore;
using Portfolio.Data.Context;
using Portfolio.Data.Account.Repository;
using Portfolio.Data.Contact.Repository;

namespace Portfolio.Data
{
    public static class AddRepositoryServices
    {
        public static void AddDatabase(this IServiceCollection service, string ConnectionString)
        {
            service.AddDbContext<DatabaseContext>(opt => opt.UseSqlServer(ConnectionString));
        }

        public static void AddRepository(this IServiceCollection service)
        {
            service.AddScoped<IAccountRepository, AccountRepository>();
            service.AddScoped<IContactRepository, ContactRepository>();
        }
    }
}
