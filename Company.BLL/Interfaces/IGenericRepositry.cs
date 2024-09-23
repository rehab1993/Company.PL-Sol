﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Interfaces
{
    public interface IGenericRepositry<T>
    {
       
        IEnumerable<T> GetAll();
        T GetById(int id);
            
        int Add(T item);
        int Update(T item);
        int Delete(T item);



    }
}
