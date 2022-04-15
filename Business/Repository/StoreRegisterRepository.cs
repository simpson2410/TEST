using Business.IRepostitory;
using Common;
using Entities.DAL;
using Entities.Entities;
using Microsoft.Extensions.Caching.Memory;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Repository
{
    public class StoreRegisterRepository : Repository<StoreRegister>, IStoreRegisterRepository
    {
        private IMemoryCache _cache;

        public StoreRegisterRepository(TNRContext context, IMemoryCache cache) : base(context)
        {
            _cache = cache;
        }
    }

}
