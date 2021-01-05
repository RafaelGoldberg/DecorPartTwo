﻿using Decor_Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Decor_DataAccess.Repository.IRepository
{
    public interface IApplicationTypeRepository : IRepository<ApplicationType>
    {
        void Update(ApplicationType obj);
    }
}
