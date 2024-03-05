namespace QuanLyKhachSan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedb : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rooms", "numberRoom1", c => c.Int(nullable: false));
            AddColumn("dbo.Rooms", "numberRoom2", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rooms", "numberRoom2");
            DropColumn("dbo.Rooms", "numberRoom1");
        }
    }
}
