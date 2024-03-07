using Microsoft.EntityFrameworkCore;

namespace MovieClub.Tests.Tools.Infrastructure.DatabaseConfig
{
    public static class DbContextHelper
    {
        public static void Save<TDbContext, TEntity>(
      this TDbContext dbContext,
      TEntity entity)
      where TDbContext : DbContext
      where TEntity : class
        {
            dbContext.Add(entity);
            dbContext.SaveChanges();
        }
    }
}
