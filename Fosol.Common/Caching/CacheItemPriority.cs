using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Caching
{
    enum CacheItemPriority
    {
        Low = 1,
        BelowNormal,
        Normal,
        AboveNormal,
        High,
        NotRemovable,
        Default = Normal
    }
}
