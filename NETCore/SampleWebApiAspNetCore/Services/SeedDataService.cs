using SampleWebApiAspNetCore.Entities;
using SampleWebApiAspNetCore.Repositories;

namespace SampleWebApiAspNetCore.Services
{
    public class SeedDataService : ISeedDataService
    {
        public void Initialize(ShoeDbContext context)
        {
            context.ShoeItems.Add(new ShoeEntity() { Price = 7500, Type = "Nike", Name = "Freak4", Created = DateTime.Now });
            context.ShoeItems.Add(new ShoeEntity() { Price = 6895, Type = "Jordan", Name = "Tatum1", Created = DateTime.Now });
            context.ShoeItems.Add(new ShoeEntity() { Price = 7500, Type = "Adidas", Name = "Harden Vol. 7", Created = DateTime.Now });
            context.ShoeItems.Add(new ShoeEntity() { Price = 10895, Type = "Li-Ning", Name = "Way of Wade 10", Created = DateTime.Now });

            context.SaveChanges();
        }
    }
}
