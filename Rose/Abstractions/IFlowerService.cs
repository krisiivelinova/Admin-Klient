using Rose.Entities;
using Rose.Models.Flower;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Rose.Abstractions
{
    public interface IFlowerService
    { 
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
            await this._context.Image.AddAsync(dbImage);
            await this._context.Flowers.AddAsync(flower);
            await this._context.SaveChangesAsync();

        }
    
        bool Create(string name, decimal price, int categoryId, string picture);
        bool UpdateFlower(int flowerId, string name, decimal price, int categoryId, string picture);
        List<Flower> GetFlowers();
        Flower GetFlowerById(int flowerId);
        bool RemoveById(int dogId);
        List<Flower> GetFlowers(string searchstringCategory, string searchStringName);
    }
}
