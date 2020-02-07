using SmartInItProjekat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartInItProjekat.Repository
{
    public class BillRepo : IBillRepo
    {
        ApplicationDbContext db = new ApplicationDbContext(); 

        public void Add(Bill bill)
        {
            db.Bills.Add(bill);
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