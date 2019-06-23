using Microsoft.EntityFrameworkCore;
using MakingSense.Blogging.WebAPI.DataAccess;
using MakingSense.Blogging.WebAPI.Entities;

namespace MakingSense.Blogging.Test
{
    public static class DbContextMocker
    {
        public static DataContext GetDataContext(string dbName)
        {
            // Create options for DbContext instance
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            // Create instance of DbContext
            var dbContext = new DataContext(options);

            // Add entities in memory
            dbContext.Seed();

            return dbContext;
        }
    }
}