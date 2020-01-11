using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartInItProjekat.Models;

namespace SmartInItProjekat.Repository
{
    public class FurnitureRepo : IFurnitureRepo,IDisposable
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public void Add(Furniture furniture,HttpPostedFileBase img)
        {
            if (img != null)
            {
                furniture.PhotoType = img.ContentType;
                furniture.ProductImage = new byte[img.ContentLength];
                img.InputStream.Read(furniture.ProductImage, 0, img.ContentLength);
            }
            db.Furnitures.Add(furniture);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            db.Furnitures.Remove(db.Furnitures.Find(id));
            db.SaveChanges();
        }

        public IEnumerable<Furniture> GetAll()
        {
            return db.Furnitures.Include("FurnitureSalon").Include("Category");
        }
        public SelectList IncludeCategory()
        {
            return  new SelectList(db.Categories, "CategoryId", "Name");
        }
        public SelectList IncludeFurnitureSalon()
        {
           return  new SelectList(db.FurnitureSalons, "FurnitureSalonId", "Name");
        }
        public Furniture GetById(int? id)
        {
            return db.Furnitures.Include("FurnitureSalon").Include("Category").SingleOrDefault(f => f.FurnitureId == id);
        }

        public void Update(Furniture furniture, HttpPostedFileBase img)
        {
            try
            {
                if (img == null)
                {
                    db.Entry(furniture).State = EntityState.Modified;
                    db.Entry(furniture).Property(x => x.ProductImage).IsModified = false;
                }
                else
                {
                    furniture.PhotoType = img.ContentType;
                    furniture.ProductImage = new byte[img.ContentLength];
                    img.InputStream.Read(furniture.ProductImage, 0, img.ContentLength);
                    db.Entry(furniture).State = EntityState.Modified;
                }
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {

                throw;
            }
           
        }
        
        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}