using System;

namespace Authifex.Core.Models
{
    /// <summary>
    /// 认证密钥（密码哈希存储）
    /// </summary>
    public class AuthSecret
    {
        /// <summary>主键ID (UUID Guid)</summary>
        public Guid Id { get; set; }

        /// <summary>关联的 Profile Id</summary>
        public Guid ProfileId { get; set; }

        /// <summary>哈希后的密码 (bcrypt等)</summary>
        public string PasswordHash { get; set; } = null;

        /// <summary>使用的哈希算法 (默认 bcrypt)</summary>
        public string Algorithm { get; set; } = "bcrypt";

        /// <summary>最后修改时间</summary>
        public DateTime LastChangedAt { get; set; }

        // 导航属性
        public Profile Profile { get; set; } = null;
    }
}
