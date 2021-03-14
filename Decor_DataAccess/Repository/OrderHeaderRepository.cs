using Tzedakah_DataAccess.Data;
using Tzedakah_DataAccess.Repository.IRepository;
using Tzedakah_Models;
using Tzedakah_Utility;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tzedakah_DataAccess.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private readonly ApplicationDbContext _db;
      

        public OrderHeaderRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }

     

        public void Update(OrderHeader obj)
        {
            _db.OrderHeader.Update(obj);
        }
    }
}
