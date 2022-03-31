using Rose.Abstractions;
using Rose.Data;
using Rose.Entities;
using Rose.Models.Flower;
using System;
using System.Collections.Generic;
using System.IO;
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

        public bool Create(string name, decimal price, string description, int categoryId, string picture)
        {
            var flower = new Flower
            {
                Name = name,
                Price = price,
                Description = description,
                Category = _context.Categories.Find(categoryId),
                Picture = picture,
            };
            _context.Flowers.Add(flower);
            return _context.SaveChanges() != 0;

        }

        public Flower GetFlowerById(int flowerId)
        {
            throw new NotImplementedException();
        }

        public List<Flower> GetFlowers()
        {
            throw new NotImplementedException();
        }

        public List<Flower> GetFlowers(string searchstringCategory, string searchStringName)
        {
            throw new NotImplementedException();
        }

        public bool RemoveById(int dogId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateFlower(int flowerId, string name, decimal price, int categoryId, string picture)
        {
            throw new NotImplementedException();
        }
        
    }
}
