using Business.IRepostitory;
using Entities.DAL;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;


namespace Business.Repository
{
    public class RoleMemberRepository : Repository<RoleMember>, IRoleMemberRepository
    {
        public RoleMemberRepository(TNRContext context) : base(context)
        {

        }

    }
}
