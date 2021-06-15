using Business.Interface;
using DataAccess.Interface;
using DataAccess.Model;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.BussinessLogic.AIML
{
    public class AIMLConfiguration : BaseBusinessLogic, IAIMLConfiguration
    {
        private IUnitofWork _uow { get; set; }
        public AIMLConfiguration(IUnitofWork uow)
        {
            _uow = uow;
        }

        public List<MasterConfiguration> GetAppSettings()
        {
            _uow.OpenConnection(base.SQLDBConn);
            var ConfigurationRepository = new MasterConfigurationRepository(_uow);

            try
            {
                IList<MasterConfiguration> list = ConfigurationRepository.ReadByLambda(x => x.IsActive == true);
                return (List<MasterConfiguration>)list;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                _uow.Dispose();

            }
        }

    }
}
