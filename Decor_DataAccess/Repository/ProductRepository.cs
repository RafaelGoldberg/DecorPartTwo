﻿using Decor_DataAccess.Data;
using Decor_DataAccess.Repository.IRepository;
using Decor_Models;
using Decor_Utility;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Decor_DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;
      

        public ProductRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetAllDropdownList(string obj)
        {
            if (obj == WC.CategoryName)
            {
                return _db.Category.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
            }

            if (obj == WC.ApplicationTypeName)
            {
                return _db.ApplicationType.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
            }
            return null;
        }

        public void Update(Product obj)
        {
            _db.Product.Update(obj);
        }
    }
}
