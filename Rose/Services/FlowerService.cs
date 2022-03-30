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
        public async Task Create(FlowerCreateVM model, string imagePath)
        {
            var extension = Path.GetExtension(model.Image.FileName).TrimStart('.');

            var flower = new Flower
            {
                Name = model.Name,
                Price = model.Price,
                CategoryId = model.CategoryId,
            };
            var dbImage = new Image()
            {
                Flower = flower,
                Extension = extension
            };
            flower.ImageId = dbImage.Id;
            Directory.CreateDirectory($"{imagePath}/images/");
            var physicalPath = $"{imagePath}/images/{dbImage.Id}.{extension}";
            using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
            await model.Image.CopyToAsync(fileStream);
            await this._context.Images.AddAsync(dbImage);
            await this._context.Flowers.AddAsync(flower);
            await this._context.SaveChangesAsync();

        }
    }
}
