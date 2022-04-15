using Business.IRepostitory;
using Entities.DAL;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;


namespace Business.Repository
{
    public class ProjectDetailRepository : Repository<ProjectDetail>, IProjectDetailRepository
    {
        public ProjectDetailRepository(TNRContext context) : base(context)
        {

        }

    }
}
