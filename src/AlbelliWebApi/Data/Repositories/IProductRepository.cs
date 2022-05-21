using AlbelliWebApi.Data.Entities;

namespace AlbelliWebApi.Data.Repositories
{
    public interface IProductRepository
    {
        List<ProductType> GetAll();
    }
}
