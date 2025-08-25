using Authifex.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Authifex.DataAccess.Persistence
{
    public class AuthifexDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public AuthifexDbContext(DbContextOptions<AuthifexDbContext> options) : base(options) { }

        public DbSet<Profile> Profiles => Set<Profile>();
        public DbSet<Account> Accounts => Set<Account>();
        public DbSet<AuthSecret> AuthSecrets => Set<AuthSecret>();

        protected override void OnModelCreating(ModelBuilder b)
        {
            // 全局过滤软删除
            b.Entity<Profile>().HasQueryFilter(p => p.DeletedAt == null);

            // 表名、主键、列名基本不用动，EF 默认就能映射
            // 只需把 MySQL 特有类型写出来即可
            b.Entity<Profile>(e =>
            {
                e.Property(p => p.Id)
                 .HasColumnType("binary(16)");

                e.Property(p => p.Uid)
                 .HasColumnType("bigint");

                e.Property(p => p.Gender)
                 .HasColumnType("tinyint");

                e.Property(p => p.Status)
                 .HasColumnType("tinyint");

                e.Property(p => p.ExtraData)
                 .HasColumnType("json")
                 .HasConversion(
                     v => JsonSerializer.Serialize(v, default(JsonSerializerOptions)),
                     s => JsonSerializer.Deserialize<Dictionary<string, object>>(s, default(JsonSerializerOptions)!));

                e.HasIndex(p => p.Status)
                 .HasDatabaseName("idx_status");

                e.HasIndex(p => p.DeletedAt)
                 .HasDatabaseName("idx_deleted_at");
            });

            b.Entity<Account>(e =>
            {
                e.Property(p => p.Id)
                 .HasColumnType("binary(16)");

                e.Property(p => p.ProfileId)
                 .HasColumnType("binary(16)");

                e.HasIndex(p => new { p.Provider, p.ProviderId })
                 .IsUnique()
                 .HasDatabaseName("uq_provider_id");

                e.HasIndex(p => p.ProfileId)
                 .HasDatabaseName("idx_profile_id");
            });

            b.Entity<AuthSecret>(e =>
            {
                e.Property(p => p.Id)
                 .HasColumnType("binary(16)");

                e.Property(p => p.ProfileId)
                 .HasColumnType("binary(16)");

                e.HasIndex(p => p.ProfileId)
                 .IsUnique();
            });
        }
    }
}
