using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;



namespace DataBase
{
    public class TemporaryDbContextFactory : IDesignTimeDbContextFactory<DatabaseContext>

    {

        DatabaseContext IDesignTimeDbContextFactory<DatabaseContext>.CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<DatabaseContext>();
            builder.UseNpgsql(DatabaseSettings.getConnectionString());
            return new DatabaseContext(builder.Options);
        }
    }
}
