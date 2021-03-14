using Tzedakah_DataAccess.Data;
using Tzedakah_DataAccess.Repository.IRepository;
using Tzedakah_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tzedakah_DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
      

        public CategoryRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }

        public void Update(Category obj)
        {
            var objFromDb = base.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = obj.Name;
                objFromDb.DisplayOrder = obj.DisplayOrder;
            }
        }
    }
}
