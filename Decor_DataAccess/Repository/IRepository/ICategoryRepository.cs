﻿using Tzedakah_Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tzedakah_DataAccess.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category obj);
    }
}
