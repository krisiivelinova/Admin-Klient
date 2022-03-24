using Rose.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rose.Abstractions
{
    public interface IFlowerService
    {
        bool Create(string name, decimal price, int categoryId, string picture);
        bool UpdateFlower(int flowerId, string name, decimal price, int categoryId, string picture);
        List<Flower> GetFlowers();
        Flower GetFlowerById(int flowerId);
        bool RemoveById(int dogId);
        List<Flower> GetFlowers(string searchstringCategory, string searchStringName);
    }
}
