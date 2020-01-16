using SmartInItProjekat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SmartInItProjekat.Repository
{
  public  interface IFurnitureRepo
    {
        IEnumerable<Furniture> GetAll();
        Furniture GetById(int? id);
        Task<Furniture> Add(Furniture furniture);
        bool Update(Furniture furniture);
        void Delete(int id);
        SelectList IncludeCategory();
        SelectList IncludeFurnitureSalon();
        Task SaveAsync();


    }
}
