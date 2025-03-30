using Microsoft.EntityFrameworkCore;

namespace merchant_api.Data.Data;

public class PostgresContext(DbContextOptions<PostgresContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PostgresContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}