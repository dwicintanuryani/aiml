using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Interface
{
    interface IAIMLConfiguration
    {
        List<MasterConfiguration> GetAppSettings();
    }
}
