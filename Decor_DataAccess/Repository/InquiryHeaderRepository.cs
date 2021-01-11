using Decor_DataAccess.Data;
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
    public class InquiryHeaderRepository : Repository<InquiryHeader>, IInquiryHeaderRepository
    {
        private readonly ApplicationDbContext _db;
      

        public InquiryHeaderRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }

     

        public void Update(InquiryHeader obj)
        {
            _db.InquiryHeader.Update(obj);
        }
    }
}
