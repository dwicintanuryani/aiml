using DataAccess.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Model
{
    public class MasterConfiguration : BaseModel
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
