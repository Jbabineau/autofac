﻿using Homesite.Services.AddressBroker.DAL.Contracts;
using Homesite.Services.AddressBroker.DAL.Implementations;
using Homesite.Services.AddressBroker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homesite.Services.AddressBroker.Repository
{
    public class MapRepository : Repository<JB_Maps>, IMapRepository
    {
        private JB_Service_InfoEntities dataContext;

        new protected IDatabaseFactory DatabaseFactory
        {
            get;
            private set;
        }

        public MapRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
            DatabaseFactory = databaseFactory;
        }

        new protected JB_Service_InfoEntities DataContext
        {
            get { return dataContext ?? (dataContext = DatabaseFactory.Get());  }
        }
    }

    public interface IMapRepository : IRepository<JB_Maps>
    {
    }
}
