using System;
using System.Collections.Generic;

namespace Authifex.Core.Models
{
    /// <summary>
    /// 用户档案信息（领域实体）
    /// </summary>
    public class Profile
    {
        /// <summary>核心ID (UUID Guid)</summary>
        public Guid Id { get; set; }

        /// <summary>用户数字ID（靓号，从高位起始，如10001）</summary>
        public long Uid { get; set; }

        /// <summary>用户名（唯一，用于URL和登录）</summary>
        public string Username { get; set; } = null;

        /// <summary>昵称（显示用，可重复）</summary>
        public string Nickname { get; set; } = null;

        /// <summary>头像URL</summary>
        public string AvatarUrl { get; set; }

        /// <summary>性别: 0-未知, 1-男, 2-女</summary>
        public byte Gender { get; set; }

        /// <summary>个人简介</summary>
        public string Bio { get; set; }

        /// <summary>用户状态: 0-正常, 1-禁用, 2-待审核, 3-已注销</summary>
        public byte Status { get; set; }

        /// <summary>额外扩展数据 (JSON 格式)</summary>
        //public string ExtraData { get; set; }
        // EF 用 ToJson() 存 JSON 会更自然
        public Dictionary<string, object> ExtraData { get; set; }

        /// <summary>创建时间</summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>更新时间</summary>
        public DateTime UpdatedAt { get; set; }

        /// <summary>软删除时间</summary>
        public DateTime? DeletedAt { get; set; }

        // 导航属性（关系）
        public ICollection<Account> Accounts { get; set; } = new List<Account>();
        public AuthSecret AuthSecret { get; set; }
    }
}
