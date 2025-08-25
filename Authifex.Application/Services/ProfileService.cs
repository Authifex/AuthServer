using Authifex.Core.Interfaces;
using Authifex.Core.Models;
using Authifex.DataAccess.Persistence;
using Authifex.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Authifex.Application.Services
{
    /// <summary>
    /// 实现基于 UUIDv7 的 Profile ID 生成器
    /// </summary>
    internal class profileIdGenerator: IProfileIdGenerator
    {
        public Guid NewId()
        {
            return Medo.Uuid7.NewUuid7();
        }
    }

    /// <summary>
    /// 为系统生成唯一的个人资料 UID。
    /// </summary>
    /// <remarks>此类提供为个人资料生成新的唯一标识符（UID）的功能。
    /// 它会先尝试重用未分配的 UID，之后再从预定义的基准值开始生成新的连续 UID。</remarks>
    public class ProfileUidGenerator : IProfileUidGenerator
    {
        private readonly AuthifexDbContext _dbContext;
        private const long StartUid = 10001;

        public ProfileUidGenerator(AuthifexDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<long> NewUidAsync(CancellationToken ct = default)
        {
            // 1. 尝试回收空位
            var emptyUid = await _dbContext.Profiles
                .Where(p => p.Uid == 0)
                .OrderBy(p => p.CreatedAt)
                .Select(p => p.Uid)
                .FirstOrDefaultAsync(ct);

            if (emptyUid != 0)
            {
                return emptyUid;
            }

            // 2. 生成新的连续 UID
            var maxUid = await _dbContext.Profiles.MaxAsync(p => (long?)p.Uid, ct) ?? StartUid - 1;
            return maxUid + 1;
        }
    }

    /// <summary>
    /// 提供用户配置文件的创建和管理功能
    /// </summary>
    /// <remarks>
    /// 此服务负责创建新的用户配置文件，并将其持久化到基础数据存储中。
    /// 它使用<see cref="IProfileRepository"/>进行数据访问，
    /// 并使用<see cref="IProfileIdGenerator"/>生成唯一的配置文件 ID。
    /// </remarks>
    public class ProfileService
    {
        private readonly IProfileRepository _profileRepo;
        private readonly IProfileIdGenerator _idGenerator;

        public ProfileService(IProfileRepository profileRepo, IProfileIdGenerator idGenerator)
        {
            _profileRepo = profileRepo;
            _idGenerator = idGenerator;
        }

        public async Task<Profile> CreateProfileAsync(string username, string nickname)
        {
            var profile = new Profile
            {
                Id = _idGenerator.NewId(),
                Username = username,
                Nickname = nickname,
                Status = 0,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _profileRepo.AddAsync(profile);
            await _profileRepo.SaveChangesAsync();
            return profile;
        }
    }
}
