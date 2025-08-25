using System;

namespace Authifex.Core.Interfaces
{
    public interface IProfileIdGenerator
    {
        Guid NewId();
    }
}
