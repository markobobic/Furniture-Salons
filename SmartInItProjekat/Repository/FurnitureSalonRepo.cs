using SmartInItProjekat.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SmartInItProjekat.Repository
{
    public class FurnitureSalonRepo : IDisposable,IFurnitureSalonRepo
    {
        ApplicationDbContext db = new ApplicationDbContext();
      

        public IEnumerable<FurnitureSalon> GetAll()
        {
            return db.FurnitureSalons.Include("Furnitures");
        }

        public FurnitureSalon GetById(int? id)
        {
            return db.FurnitureSalons.Include("Furnitures").SingleOrDefault(f => f.FurnitureSalonId == id);
        }
        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }
        public async Task<FurnitureSalon> Add(FurnitureSalon furnitureSalon)
        {
            return db.FurnitureSalons.Add(furnitureSalon);
        }

        public async Task Update(FurnitureSalon furnitureSalon)
        {
            db.Entry(furnitureSalon).State = System.Data.Entity.EntityState.Modified;
            try
            {
               await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public void Delete(int id)
        {
            db.FurnitureSalons.Remove(db.FurnitureSalons.Find(id));
            db.SaveChanges();
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