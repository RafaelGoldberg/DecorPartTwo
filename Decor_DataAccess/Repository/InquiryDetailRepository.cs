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
    public class InquiryDetailRepository : Repository<InquiryDetail>, IInquiryDetailRepository
    {
        private readonly ApplicationDbContext _db;
      

        public InquiryDetailRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }

     

        public void Update(InquiryDetail obj)
        {
            _db.InquiryDetail.Update(obj);
        }
    }
}
