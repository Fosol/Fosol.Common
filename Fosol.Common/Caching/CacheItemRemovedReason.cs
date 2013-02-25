using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fosol.Common.Caching
{
    enum CacheItemRemovedReason
    {
        Removed = 1,
        Expired,
        Underused,
        DependancyChanged
    }
}
