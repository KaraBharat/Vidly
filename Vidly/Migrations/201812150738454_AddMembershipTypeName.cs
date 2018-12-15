namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMembershipTypeName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MembershipTypes", "Type", c => c.String(maxLength: 255));

            Sql("UPDATE MembershipTypes SET Type = 'Pay as You go' WHERE Id = 1");
            Sql("UPDATE MembershipTypes SET Type = 'Monthly' WHERE Id = 2");
            Sql("UPDATE MembershipTypes SET Type = 'Quarterly' WHERE Id = 3");
            Sql("UPDATE MembershipTypes SET Type = 'Yearly' WHERE Id = 4");
        }
        
        public override void Down()
        {
            DropColumn("dbo.MembershipTypes", "Type");
        }
    }
}
