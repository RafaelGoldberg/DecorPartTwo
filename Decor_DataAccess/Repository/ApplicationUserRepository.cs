using Tzedakah_DataAccess.Data;
using Tzedakah_DataAccess.Repository.IRepository;
using Tzedakah_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tzedakah_DataAccess.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly ApplicationDbContext _db;
      

        public ApplicationUserRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }

     
    }
}
