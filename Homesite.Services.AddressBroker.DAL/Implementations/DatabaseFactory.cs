using Homesite.Services.AddressBroker.DAL.Contracts;
using Homesite.Services.AddressBroker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homesite.Services.AddressBroker.DAL.Implementations
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private JB_Service_InfoEntities dataContext;

        public JB_Service_InfoEntities Get()
        {
            return dataContext ?? (dataContext = new JB_Service_InfoEntities());
        }

        protected override void DisposeCore()
        {
            if (dataContext != null)
                dataContext.Dispose();
        }
    }
}
