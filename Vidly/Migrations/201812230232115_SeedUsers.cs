namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'8275d6b1-cae1-4ff6-9391-32d54b9ca1de', N'CanManageMovies')

                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'07d06513-1f2d-4496-90f2-129f1593067b', N'operation@vidly.com', 0, N'AMWxPz0DBQySngVgvqyVvZjc5SHBuIAm5aIhA0/Rqu4NOiGoUZivyuqD8nf+yRq79w==', N'0f0018f3-1c1e-4341-8eef-de98fabeb89d', NULL, 0, 0, NULL, 1, 0, N'operation@vidly.com')
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'e21837ee-7235-432e-bcdd-06c64f61a929', N'admin@vidly.com', 0, N'AHBXGnAMvoiHTyt1dgfxRh2NPe/aGl+OYuQ7z/H/y9KOaPdT+sZhNdnZtceKk40Ikw==', N'85fe41ee-16ab-4a14-8f52-182889f89f64', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')

                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'e21837ee-7235-432e-bcdd-06c64f61a929', N'8275d6b1-cae1-4ff6-9391-32d54b9ca1de')
            ");
        }
        
        public override void Down()
        {
        }
    }
}
