﻿using SmartInItProjekat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartInItProjekat.Repository
{
    public interface ICategoriesRepo
    {
        IEnumerable<Category> GetAll();
        Category GetById(int? id);
        void Add(Category category);
        void Update(Category category);
        void Delete(int id);
    }
}
