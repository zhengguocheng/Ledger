using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public interface iEntityBase
    {
        string UpdatedByName { get; set; }
        string CreatedByName { get; set; }
    }
}
