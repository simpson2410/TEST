using Business.IRepostitory;
using Entities.DAL;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Repository
{
    public class CommmentRepository : Repository<Comment>, ICommentRepository
    {
        public CommmentRepository(TNRContext context) : base(context)
        {

        }

    }
}
