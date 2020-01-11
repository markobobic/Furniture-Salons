using SmartInItProjekat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartInItProjekat.Repository
{
  public interface IFurnitureSalonRepo
    {
        IEnumerable<FurnitureSalon> GetAll();
        FurnitureSalon GetById(int? id);
        void Add(FurnitureSalon furnitureSalon);
        void Update(FurnitureSalon furnitureSalon);
        void Delete(int id);
    }
}
