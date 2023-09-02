using SampleWebApiAspNetCore.Entities;
using SampleWebApiAspNetCore.Models;

namespace SampleWebApiAspNetCore.Repositories
{
    public interface IShoeRepository
    {
        ShoeEntity GetSingle(int id);
        void Add(ShoeEntity item);
        void Delete(int id);
        ShoeEntity Update(int id, ShoeEntity item);
        IQueryable<ShoeEntity> GetAll(QueryParameters queryParameters);
        ICollection<ShoeEntity> GetRandomMeal();
        int Count();
        bool Save();
    }
}
