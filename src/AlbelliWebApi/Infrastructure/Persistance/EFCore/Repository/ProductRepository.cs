using AlbelliWebApi.Data.Entities;
using AlbelliWebApi.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AlbelliWebApi.Infrastructure.Persistance.EFCore.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AlbelliDataContext _dbContext;
        public ProductRepository(AlbelliDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<ProductType> GetAll()
        {
            var options = new DbContextOptionsBuilder<AlbelliDataContext>()
                           .UseInMemoryDatabase(databaseName: "Test")
                           .Options;

            using var context = new AlbelliDataContext(options);
            return _dbContext.ProductTypes.ToList();
        }
    }
}
