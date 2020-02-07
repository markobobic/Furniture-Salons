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
        Task<FurnitureSalon> Add(FurnitureSalon furnitureSalon);
        Task Update(FurnitureSalon furnitureSalon);
        void Delete(int id);
         Task SaveAsync();
        bool DoesAccountNumberExist(string accountNumber);
        bool DoesPIBExist(string pibNumber);
        bool DoesEmailExist(string email);
    }
}
