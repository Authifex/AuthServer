using System;

namespace Authifex.Core.Models
{
    /// <summary>
    /// 外部账号绑定（领域实体）
    /// </summary>
    public class Account
    {
        /// <summary>主键ID (UUID Guid)</summary>
        public Guid Id { get; set; }

        /// <summary>关联的 Profile Id</summary>
        public Guid ProfileId { get; set; }

        /// <summary>认证提供方，如 email/phone/wechat/google</summary>
        public string Provider { get; set; } = null;

        /// <summary>提供方内的唯一ID，如邮箱或 UnionID</summary>
        public string ProviderId { get; set; } = null;

        /// <summary>是否已验证</summary>
        public bool IsVerified { get; set; }

        /// <summary>绑定时间</summary>
        public DateTime BoundAt { get; set; }

        // 导航属性
        public Profile Profile { get; set; } = null;
    }
}
