using Business.IRepostitory;
using Entities.DAL;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository
{
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        public ClientRepository(TNRContext context) : base(context)
        {

        }

    }
}
