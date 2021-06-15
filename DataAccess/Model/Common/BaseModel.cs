using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Model.Common
{
    public abstract class BaseModel
    {
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

    }
}
