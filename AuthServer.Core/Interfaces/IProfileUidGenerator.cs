using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Authifex.Core.Interfaces
{
    public interface IProfileUidGenerator
    {
        Task<long> NewUidAsync(CancellationToken ct = default);
    }
}
