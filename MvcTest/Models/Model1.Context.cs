﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace MvcTest.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ToolsDBEntities : DbContext
    {
        public ToolsDBEntities()
            : base("name=ToolsDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<C_Class> C_Class { get; set; }
        public virtual DbSet<EmailSendHistory> EmailSendHistories { get; set; }
        public virtual DbSet<ErrorLog> ErrorLogs { get; set; }
        public virtual DbSet<LoginLog> LoginLogs { get; set; }
        public virtual DbSet<Manager> Managers { get; set; }
        public virtual DbSet<MenuInfo> MenuInfoes { get; set; }
        public virtual DbSet<PagePowerSign> PagePowerSigns { get; set; }
        public virtual DbSet<PagePowerSignPublic> PagePowerSignPublics { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RoleManager> RoleManagers { get; set; }
        public virtual DbSet<RoleMenuPage> RoleMenuPages { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<UseLog> UseLogs { get; set; }
        public virtual DbSet<WebConfig> WebConfigs { get; set; }
        public virtual DbSet<MasterData> MasterDatas { get; set; }
        public virtual DbSet<C_News> C_News { get; set; }
        public virtual DbSet<C_Channel> C_Channel { get; set; }
        public virtual DbSet<ThirdUser> ThirdUsers { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}