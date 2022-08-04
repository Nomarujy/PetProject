using Microsoft.EntityFrameworkCore;
using Portfolio.Data.Account.Encryptor;
using Portfolio.Data.Account.Repository;
using Portfolio.Data.Contact.Repository;
using Portfolio.Data.Context;

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
            service.AddSingleton<IPasswordEncryptor, PasswordEncryptor>();
            service.AddScoped<IContactRepository, ContactRepository>();
        }
    }
}
