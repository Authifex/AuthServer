using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Authifex.Core.Interfaces;
using Authifex.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Authifex.DataAccess.Repositories
{
    public class EfRepository<T> : IRepository<T> where T : class
    {
        private readonly AuthifexDbContext _ctx;
        public EfRepository(AuthifexDbContext ctx) => _ctx = ctx;

        public async Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default)
            => await _ctx.Set<T>().FindAsync([id], ct);

        public async Task AddAsync(T entity, CancellationToken ct = default)
            => await _ctx.Set<T>().AddAsync(entity, ct);

        public void Remove(T entity) => _ctx.Set<T>().Remove(entity);

        public async Task<int> SaveChangesAsync(CancellationToken ct = default)
            => await _ctx.SaveChangesAsync(ct);
    }
}
