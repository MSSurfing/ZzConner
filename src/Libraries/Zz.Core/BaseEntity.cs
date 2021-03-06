﻿using System;

namespace Zz.Core
{
    public abstract class BaseEntity
    {
        /// <summary>
        /// 实体主键（Id）
        /// </summary>
        public virtual Guid Id { get; set; }
    }
}
