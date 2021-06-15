using DataAccess.Interface;
using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Repository
{
    public class MasterConfigurationRepository : RepoSQLDBRepository<MasterConfiguration>
    {
        public MasterConfigurationRepository(IUnitofWork uow) : base(uow)
        { }       

    }
}
