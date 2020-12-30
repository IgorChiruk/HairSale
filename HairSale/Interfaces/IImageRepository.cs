using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HairSale.Models.HairImages;

namespace HairSale.Interfaces
{
    public interface IImageRepository<T> : IDisposable where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Add(T item);
        void Update(T item);
        void Delete(int id);
        void Save();
    }
}
