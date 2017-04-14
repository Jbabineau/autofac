using Homesite.Services.AddressBroker.DAL.Contracts;
using Homesite.Services.AddressBroker.DAL.Implementations;
using Homesite.Services.AddressBroker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homesite.Services.AddressBroker.Repository
{

    public class MapFormatRepository : Repository<JB_MapFormat>, IMapFormatRepository
    {
        private JB_Service_InfoEntities dataContext;

        new protected IDatabaseFactory DatabaseFactory
        {
            get;
            private set;
        }

        public MapFormatRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
            DatabaseFactory = databaseFactory;
        }

        new protected JB_Service_InfoEntities DataContext
        {
            get { return dataContext ?? (dataContext = DatabaseFactory.Get()); }
        }
    }

    public interface IMapFormatRepository : IRepository<JB_MapFormat>
    {
    }
}
