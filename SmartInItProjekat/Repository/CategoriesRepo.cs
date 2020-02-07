using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using SmartInItProjekat.Models;

namespace SmartInItProjekat.Repository
{
    public class CategoriesRepo : ICategoriesRepo, IDisposable
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public void Add(Category category)
        {
            db.Categories.Add(category);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            db.Categories.Remove(db.Categories.Find(id));
            db.SaveChanges();
        }

        public IEnumerable<Category> GetAll()
        {
            return db.Categories;
        }

        public Category GetById(int? id)
        {
            return db.Categories.Find(id);
        }

        public void Update(Category category)
        {

            db.Entry(category).State = System.Data.Entity.EntityState.Modified;
            try
            {
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

        public bool DoesNameExist(string name)
        {
          return db.Categories.Any(x => x.Name == name);
        }
    }
}