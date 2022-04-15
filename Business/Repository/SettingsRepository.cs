﻿using Business.IRepostitory;
using Entities.DAL;
using Entities.Entities;

namespace Business.Repository
{
    public class SettingsRepository : Repository<Settings>, ISettingsRepository
    {
        public SettingsRepository(TNRContext context) : base(context)
        {

        }
    }
}
