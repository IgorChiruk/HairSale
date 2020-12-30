using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using HairSale.Interfaces;

namespace HairSale.Models.HairImages
{
    public class ImageManager : IImageRepository<ImageEntity>
    {
        private readonly AppContext db;
        private bool disposed = false;
        public ImageManager()
        {
            this.db = new AppContext();
        }

        public void Add(ImageEntity image)
        {
            db.Images.Add(image);

        }

        public ImageEntity Get(int id)
        {
            return db.Images.Find(id);
        }

        public IEnumerable<ImageEntity> GetAll()
        {
            return db.Images.ToList();
        }

        public void Delete(int id)
        {
            ImageEntity image = db.Images.Find(id);
            if (image != null)
            { db.Images.Remove(image); }
        }

        public void Update(ImageEntity image)
        {
            db.Entry(image).State= EntityState.Modified;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}