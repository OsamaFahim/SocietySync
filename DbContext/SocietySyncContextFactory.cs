using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace SocietySync.DBcontext
{
    public class SocietySyncContextFactory : IDesignTimeDbContextFactory<SocietySyncContext>
    {
        private SocietySyncContext context;

        public SocietySyncContextFactory()
        {
            context = CreateDbContext(Array.Empty<string>());
        }
        public SocietySyncContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<SocietySyncContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            context = new SocietySyncContext(optionsBuilder.Options);
            return context;
        }
        public SocietySyncContext GetContext()
        {
            return context;
        }
    }
}