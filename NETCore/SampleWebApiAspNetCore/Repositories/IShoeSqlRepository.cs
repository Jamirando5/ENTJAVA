using SampleWebApiAspNetCore.Entities;
using SampleWebApiAspNetCore.Helpers;
using SampleWebApiAspNetCore.Models;
using System.Linq.Dynamic.Core;

namespace SampleWebApiAspNetCore.Repositories
{
    public class ShoeSqlRepository : IShoeRepository
    {
        private readonly ShoeDbContext _shoeDbContext;

        public ShoeSqlRepository(ShoeDbContext shoeDbContext)
        {
            _shoeDbContext = shoeDbContext;
        }

        public ShoeEntity GetSingle(int id)
        {
            return _shoeDbContext.ShoeItems.FirstOrDefault(x => x.Id == id);
        }

        public void Add(ShoeEntity item)
        {
            _shoeDbContext.ShoeItems.Add(item);
        }

        public void Delete(int id)
        {
            ShoeEntity shoeItem = GetSingle(id);
            _shoeDbContext.ShoeItems.Remove(shoeItem);
        }

        public ShoeEntity Update(int id, ShoeEntity item)
        {
            _shoeDbContext.ShoeItems.Update(item);
            return item;
        }

        public IQueryable<ShoeEntity> GetAll(QueryParameters queryParameters)
        {
            IQueryable<ShoeEntity> _allItems = _shoeDbContext.ShoeItems.OrderBy(queryParameters.OrderBy,
              queryParameters.IsDescending());

            if (queryParameters.HasQuery())
            {
                _allItems = _allItems
                    .Where(x => x.Price.ToString().Contains(queryParameters.Query.ToLowerInvariant())
                    || x.Name.ToLowerInvariant().Contains(queryParameters.Query.ToLowerInvariant()));
            }

            return _allItems
                .Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                .Take(queryParameters.PageCount);
        }

        public int Count()
        {
            return _shoeDbContext.ShoeItems.Count();
        }

        public bool Save()
        {
            return (_shoeDbContext.SaveChanges() >= 0);
        }

        public ICollection<ShoeEntity> GetRandomMeal()
        {
            List<ShoeEntity> toReturn = new List<ShoeEntity>();

            toReturn.Add(GetRandomItem("Starter"));
            toReturn.Add(GetRandomItem("Main"));
            toReturn.Add(GetRandomItem("Dessert"));

            return toReturn;
        }

        private ShoeEntity GetRandomItem(string type)
        {
            return _shoeDbContext.ShoeItems
                .Where(x => x.Type == type)
                .OrderBy(o => Guid.NewGuid())
                .FirstOrDefault();
        }
    }
}
