namespace SeoManager.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstInit01 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BackLinkAndWord",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LinkId = c.Int(nullable: false),
                        LinkToId = c.Int(nullable: false),
                        WordId = c.Int(nullable: false),
                        NgayCapNhat = c.DateTime(),
                        TrangThai = c.Boolean(),
                        Link_Id = c.Int(),
                        Link_DomainId = c.Int(),
                        LinkTo_Id = c.Int(),
                        LinkTo_DomainId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Link", t => new { t.Link_Id, t.Link_DomainId })
                .ForeignKey("dbo.Link", t => new { t.LinkTo_Id, t.LinkTo_DomainId })
                .ForeignKey("dbo.Word", t => t.WordId, cascadeDelete: true)
                .Index(t => t.WordId)
                .Index(t => new { t.Link_Id, t.Link_DomainId })
                .Index(t => new { t.LinkTo_Id, t.LinkTo_DomainId });
            
            CreateTable(
                "dbo.Link",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DomainId = c.Int(nullable: false),
                        TieuDe = c.String(),
                        MoTa = c.String(),
                        NgayTao = c.DateTime(),
                        TrangThai = c.Boolean(),
                    })
                .PrimaryKey(t => new { t.Id, t.DomainId })
                .ForeignKey("dbo.Domain", t => t.DomainId, cascadeDelete: true)
                .Index(t => t.DomainId);
            
            CreateTable(
                "dbo.Domain",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ten = c.String(),
                        URL = c.String(),
                        MieuTa = c.String(),
                        NguoiDungId = c.Int(),
                        NgayKichHoat = c.DateTime(),
                        TrangThai = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NguoiDung", t => t.NguoiDungId)
                .Index(t => t.NguoiDungId);
            
            CreateTable(
                "dbo.NguoiDung",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ten = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        Phone = c.String(),
                        ImageUrl = c.String(),
                        FeedBackMessage = c.String(),
                        VaiTroId = c.Int(nullable: false),
                        NgayKichHoat = c.DateTime(),
                        TrangThai = c.Boolean(),
                        PercentOfProfileInfo = c.Int(nullable: false),
                        MoneyInAccount = c.Single(nullable: false),
                        DomainAssign = c.Int(nullable: false),
                        PercentOfDocumentViewer = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.VaiTro", t => t.VaiTroId, cascadeDelete: true)
                .Index(t => t.VaiTroId);
            
            CreateTable(
                "dbo.VaiTro",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Ten = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Word",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        NgayTao = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.History",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NguoiDungId = c.Int(nullable: false),
                        MieuTa = c.String(),
                        ThoiGian = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NguoiDung", t => t.NguoiDungId, cascadeDelete: true)
                .Index(t => t.NguoiDungId);
            
            CreateTable(
                "dbo.LinkAndWord",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LinkId = c.Int(nullable: false),
                        WordId = c.Int(nullable: false),
                        XepHang = c.Int(nullable: false),
                        NgayCapNhat = c.DateTime(nullable: false),
                        TrangThai = c.Boolean(nullable: false),
                        Link_Id = c.Int(),
                        Link_DomainId = c.Int(),
                    })
                .PrimaryKey(t => new { t.Id, t.LinkId, t.WordId })
                .ForeignKey("dbo.Link", t => new { t.Link_Id, t.Link_DomainId })
                .ForeignKey("dbo.Word", t => t.WordId, cascadeDelete: true)
                .Index(t => t.WordId)
                .Index(t => new { t.Link_Id, t.Link_DomainId });
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LinkAndWord", "WordId", "dbo.Word");
            DropForeignKey("dbo.LinkAndWord", new[] { "Link_Id", "Link_DomainId" }, "dbo.Link");
            DropForeignKey("dbo.History", "NguoiDungId", "dbo.NguoiDung");
            DropForeignKey("dbo.BackLinkAndWord", "WordId", "dbo.Word");
            DropForeignKey("dbo.BackLinkAndWord", new[] { "LinkTo_Id", "LinkTo_DomainId" }, "dbo.Link");
            DropForeignKey("dbo.BackLinkAndWord", new[] { "Link_Id", "Link_DomainId" }, "dbo.Link");
            DropForeignKey("dbo.Link", "DomainId", "dbo.Domain");
            DropForeignKey("dbo.Domain", "NguoiDungId", "dbo.NguoiDung");
            DropForeignKey("dbo.NguoiDung", "VaiTroId", "dbo.VaiTro");
            DropIndex("dbo.LinkAndWord", new[] { "Link_Id", "Link_DomainId" });
            DropIndex("dbo.LinkAndWord", new[] { "WordId" });
            DropIndex("dbo.History", new[] { "NguoiDungId" });
            DropIndex("dbo.NguoiDung", new[] { "VaiTroId" });
            DropIndex("dbo.Domain", new[] { "NguoiDungId" });
            DropIndex("dbo.Link", new[] { "DomainId" });
            DropIndex("dbo.BackLinkAndWord", new[] { "LinkTo_Id", "LinkTo_DomainId" });
            DropIndex("dbo.BackLinkAndWord", new[] { "Link_Id", "Link_DomainId" });
            DropIndex("dbo.BackLinkAndWord", new[] { "WordId" });
            DropTable("dbo.LinkAndWord");
            DropTable("dbo.History");
            DropTable("dbo.Word");
            DropTable("dbo.VaiTro");
            DropTable("dbo.NguoiDung");
            DropTable("dbo.Domain");
            DropTable("dbo.Link");
            DropTable("dbo.BackLinkAndWord");
        }
    }
}
