﻿using System;
using System.Collections.Generic;
using System.Text;
using NHibernate.Mapping.Attributes;

namespace Hd.Model
{
    public abstract class BaseEntity<T> : Feng.BaseAbstractEntity<T>
        where T : IComparable
    {
        #region "Log, MultiOrg, Version, Active"
        /// <summary>
        /// Version
        /// </summary>
        [Version(Column = "Version", Type = "Int32", UnsavedValue = "0")]
        public override int Version
        {
            get;
            set;
        }

        /// <summary>
        /// 创建者
        /// </summary>
        [Property(Name = "CreatedBy", NotNull = true, Length = 20)]
        public override string CreatedBy
        {
            get;
            set;
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Property(Name = "Created", NotNull = true)]
        public override DateTime Created
        {
            get;
            set;
        }

        /// <summary>
        /// 修改者
        /// </summary>
        [Property(Name = "UpdatedBy", NotNull = false, Length = 20)]
        public override string UpdatedBy
        {
            get;
            set;
        }

        /// <summary>
        /// 修改时间
        /// </summary>
        [Property(Name = "Updated", NotNull = false)]
        public override DateTime? Updated
        {
            get;
            set;
        }

        /// <summary>
        /// IsActive
        /// </summary>
        [Property(Name = "IsActive", NotNull = true)]
        public override bool IsActive
        {
            get;
            set;
        }

        ///// <summary>
        ///// 相关客户，同<see cref="ClientId"/>
        ///// </summary>
        //[ManyToOne(Column = "ClientId", Insert = false, Update = false)]
        //public virtual ClientInfo Client
        //{
        //    get;
        //    set;
        //}

        /// <summary>
        /// 此记录属于的客户Id。
        /// 系统支持多客户组织。客户对应为公司，组织对应为部门，记录只能同客户的操作，同一客户内，部门通过不同权限操作记录。
        /// 目前无用，默认为0
        /// </summary>
        [Property(Name = "ClientId", NotNull = true)]
        public override long ClientId
        {
            get;
            set;
        }

        ///// <summary>
        ///// 相关组织，同<see cref="OrgId"/>
        ///// </summary>
        //[ManyToOne(Column = "OrgId", Insert = false, Update = false)]
        //public virtual OrganizationInfo Org
        //{
        //    get;
        //    set;
        //}

        /// <summary>
        /// 此记录属于的组织Id。
        /// 客户组织的关系见<see cref="ClientId"/>
        /// </summary>
        [Property(Name = "OrgId", NotNull = true)]
        public override long OrgId
        {
            get;
            set;
        }
        #endregion
    }

    public abstract class BaseBOEntity : BaseEntity<Guid>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public sealed override Guid Identity
        {
            get { return this.Id; }
        }

        [Id(0, Name = "Id", Column = "Id")]
        [Generator(1, Class = "guid.comb")]
        public virtual Guid Id
        {
            get;
            set;
        }
    }
}
