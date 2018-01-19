namespace SmileApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initdb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Errors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        StackTrace = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SmileAppRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Description = c.String(maxLength: 250),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SmileAppUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                        SmileAppUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.SmileAppRoles", t => t.IdentityRole_Id)
                .ForeignKey("dbo.SmileAppUsers", t => t.SmileAppUser_Id)
                .Index(t => t.IdentityRole_Id)
                .Index(t => t.SmileAppUser_Id);
            
            CreateTable(
                "dbo.SmileAppGroup",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 250),
                        Description = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SmileAppRoleGroups",
                c => new
                    {
                        GroupId = c.Int(nullable: false),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.GroupId, t.RoleId })
                .ForeignKey("dbo.SmileAppGroup", t => t.GroupId, cascadeDelete: true)
                .ForeignKey("dbo.SmileAppRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.GroupId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.SmileAppUserGroups",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        GroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.GroupId })
                .ForeignKey("dbo.SmileAppGroup", t => t.GroupId, cascadeDelete: true)
                .ForeignKey("dbo.SmileAppUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.SmileAppUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FullName = c.String(maxLength: 256),
                        Address = c.String(maxLength: 256),
                        BirthDay = c.DateTime(),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SmileAppUserClaims",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        Id = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        SmileAppUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.SmileAppUsers", t => t.SmileAppUser_Id)
                .Index(t => t.SmileAppUser_Id);
            
            CreateTable(
                "dbo.SmileAppUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        SmileAppUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.SmileAppUsers", t => t.SmileAppUser_Id)
                .Index(t => t.SmileAppUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SmileAppUserGroups", "UserId", "dbo.SmileAppUsers");
            DropForeignKey("dbo.SmileAppUserRoles", "SmileAppUser_Id", "dbo.SmileAppUsers");
            DropForeignKey("dbo.SmileAppUserLogins", "SmileAppUser_Id", "dbo.SmileAppUsers");
            DropForeignKey("dbo.SmileAppUserClaims", "SmileAppUser_Id", "dbo.SmileAppUsers");
            DropForeignKey("dbo.SmileAppUserGroups", "GroupId", "dbo.SmileAppGroup");
            DropForeignKey("dbo.SmileAppRoleGroups", "RoleId", "dbo.SmileAppRoles");
            DropForeignKey("dbo.SmileAppRoleGroups", "GroupId", "dbo.SmileAppGroup");
            DropForeignKey("dbo.SmileAppUserRoles", "IdentityRole_Id", "dbo.SmileAppRoles");
            DropIndex("dbo.SmileAppUserLogins", new[] { "SmileAppUser_Id" });
            DropIndex("dbo.SmileAppUserClaims", new[] { "SmileAppUser_Id" });
            DropIndex("dbo.SmileAppUserGroups", new[] { "GroupId" });
            DropIndex("dbo.SmileAppUserGroups", new[] { "UserId" });
            DropIndex("dbo.SmileAppRoleGroups", new[] { "RoleId" });
            DropIndex("dbo.SmileAppRoleGroups", new[] { "GroupId" });
            DropIndex("dbo.SmileAppUserRoles", new[] { "SmileAppUser_Id" });
            DropIndex("dbo.SmileAppUserRoles", new[] { "IdentityRole_Id" });
            DropTable("dbo.SmileAppUserLogins");
            DropTable("dbo.SmileAppUserClaims");
            DropTable("dbo.SmileAppUsers");
            DropTable("dbo.SmileAppUserGroups");
            DropTable("dbo.SmileAppRoleGroups");
            DropTable("dbo.SmileAppGroup");
            DropTable("dbo.SmileAppUserRoles");
            DropTable("dbo.SmileAppRoles");
            DropTable("dbo.Errors");
        }
    }
}
