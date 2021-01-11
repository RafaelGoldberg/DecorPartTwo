using Decor_DataAccess.Data;
using Decor_DataAccess.Repository.IRepository;
using Decor_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Decor_DataAccess.Repository
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
