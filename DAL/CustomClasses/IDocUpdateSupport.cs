using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public interface IDocUpdateSupport
    {
        bool UpdateFileContent(long recordID, byte[] data);

    }
}
