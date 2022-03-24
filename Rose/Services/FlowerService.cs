using Rose.Abstractions;
using Rose.Data;
using Rose.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rose.Services
{
    public class FlowerService : IFlowerService
    {
        private readonly ApplicationDbContext _context;

        public FlowerService(ApplicationDbContext context)
        {
            _context = context;
        }

        //bool Create(string name, decimal price, int categoryId, string picture);
        //bool UpdateFlower(int flowerId, string name, decimal price, int categoryId, string picture);
        //List<Flower> GetFlowers();
        //Flower GetFlowerById(int flowerId);
        //bool RemoveById(int dogId);
        //List<Flower> GetFlowers(string searchstringCategory, string searchStringName);
        public bool Create(string name, decimal price, int categoryId, string picture)
        {
            var flower = new Flower
            {
                Name = name,
                Price = price,
                Category = _context.Categories.Find(categoryId),
                Picture = picture,
            };

            _context.Flowers.Add(flower);

            return _context.SaveChanges() != 0;
        }
    }
}
