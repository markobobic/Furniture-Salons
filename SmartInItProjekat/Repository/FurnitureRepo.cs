using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SmartInItProjekat.Models;

namespace SmartInItProjekat.Repository
{
    public class FurnitureRepo : IFurnitureRepo,IDisposable
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public IEnumerable<Furniture> GetAll()
        {
            return db.Furnitures.Include("FurnitureSalon").Include("Category");
        }
        public SelectList IncludeCategory()
        {
            return  new SelectList(db.Categories, "CategoryId", "Name");
        }
        public  SelectList IncludeFurnitureSalon()
        {
           return  new SelectList(db.FurnitureSalons, "FurnitureSalonId", "Name");
        }
       
        public  Furniture GetById(int? id)
        {
            return  db.Furnitures.Include("FurnitureSalon").Include("Category").SingleOrDefault(f => f.FurnitureId == id);
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }
        public async Task<Furniture> Add(Furniture furniture)
        {
            return  db.Furnitures.Add(furniture);
        }

        public bool  Update(Furniture furniture)
        {
            db.Entry(furniture).State = System.Data.Entity.EntityState.Modified;
            try
            {
                db.SaveChanges();
                return true;
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
        public void Delete(int id)
        {
            var furniture =  GetById(id);
            db.Furnitures.Remove(furniture);
            db.SaveChanges();
            
        }
    }
}