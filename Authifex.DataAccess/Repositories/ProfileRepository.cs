using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Authifex.Core.Interfaces;
using Authifex.Core.Models;
using Authifex.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Authifex.DataAccess.Repositories
{
    public interface IProfileRepository : IRepository<Profile>
    {
        Task<Profile?> GetByUsernameAsync(string username, CancellationToken ct = default);
        Task<Profile?> GetByUidAsync(long uid, CancellationToken ct = default);
    }

    public class ProfileRepository : EfRepository<Profile>, IProfileRepository
    {
        private readonly AuthifexDbContext _ctx;
        public ProfileRepository(AuthifexDbContext ctx) : base(ctx) => _ctx = ctx;

        public Task<Profile?> GetByUsernameAsync(string username, CancellationToken ct = default)
            => _ctx.Profiles.FirstOrDefaultAsync(p => p.Username == username, ct);

        public Task<Profile?> GetByUidAsync(long uid, CancellationToken ct = default)
            => _ctx.Profiles.FirstOrDefaultAsync(p => p.Uid == uid, ct);
    }
}
