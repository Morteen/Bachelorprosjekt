namespace APMKommune.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Applications",
                c => new
                    {
                        ApplicationId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        NumberOfUsers = c.Int(nullable: false),
                        OperatedBy = c.String(),
                        ContractInformation = c.String(),
                        InfoLink = c.String(),
                        Status = c.String(),
                        Administrator = c.String(),
                        SuperUsers = c.String(),
                        Type = c.String(),
                        LastUpgraded = c.DateTime(nullable: false),
                        ExternalUsers = c.String(),
                        CostYearly = c.Single(nullable: false),
                        CostInitial = c.Single(nullable: false),
                        UsesSharedComponents = c.Boolean(nullable: false),
                        Strengths = c.String(),
                        Weaknesses = c.String(),
                        ContractResignation = c.String(),
                        BusinessValueScore = c.Int(nullable: false),
                        ArchitectureFitsScore = c.Int(nullable: false),
                        ApplicationRiskScore = c.Int(nullable: false),
                        ApplicationSpeedScore = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                        SegmentId = c.Int(nullable: false),
                        ServiceId = c.Int(nullable: false),
                        SupplierId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ApplicationId)
                .ForeignKey("dbo.Companies", t => t.CompanyId)
                .ForeignKey("dbo.Segments", t => t.SegmentId)
                .ForeignKey("dbo.Services", t => t.ServiceId)
                .ForeignKey("dbo.Suppliers", t => t.SupplierId)
                .Index(t => t.CompanyId)
                .Index(t => t.SegmentId)
                .Index(t => t.ServiceId)
                .Index(t => t.SupplierId);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        CompanyId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        CompanyNr = c.String(),
                    })
                .PrimaryKey(t => t.CompanyId);
            
            CreateTable(
                "dbo.Datasets",
                c => new
                    {
                        DatasetId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        isMasterData = c.Boolean(nullable: false),
                        isAccessible = c.Boolean(nullable: false),
                        ApplicationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DatasetId)
                .ForeignKey("dbo.Applications", t => t.ApplicationId)
                .Index(t => t.ApplicationId);
            
            CreateTable(
                "dbo.Integrations",
                c => new
                    {
                        IntegrationId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Type = c.String(),
                        Description = c.String(),
                        TargetApplicationId = c.Int(nullable: false),
                        ApplicationId = c.Int(nullable: false),
                        Application_ApplicationId = c.Int(),
                    })
                .PrimaryKey(t => t.IntegrationId)
                .ForeignKey("dbo.Applications", t => t.ApplicationId)
                .ForeignKey("dbo.Applications", t => t.TargetApplicationId)
                .ForeignKey("dbo.Applications", t => t.Application_ApplicationId)
                .Index(t => t.TargetApplicationId)
                .Index(t => t.ApplicationId)
                .Index(t => t.Application_ApplicationId);
            
            CreateTable(
                "dbo.Segments",
                c => new
                    {
                        SegmentId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.SegmentId);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        ServiceId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        SegmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ServiceId)
                .ForeignKey("dbo.Segments", t => t.SegmentId)
                .Index(t => t.SegmentId);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        SupplierId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.SupplierId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Applications", "SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.Applications", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.Applications", "SegmentId", "dbo.Segments");
            DropForeignKey("dbo.Services", "SegmentId", "dbo.Segments");
            DropForeignKey("dbo.Integrations", "Application_ApplicationId", "dbo.Applications");
            DropForeignKey("dbo.Integrations", "TargetApplicationId", "dbo.Applications");
            DropForeignKey("dbo.Integrations", "ApplicationId", "dbo.Applications");
            DropForeignKey("dbo.Datasets", "ApplicationId", "dbo.Applications");
            DropForeignKey("dbo.Applications", "CompanyId", "dbo.Companies");
            DropIndex("dbo.Services", new[] { "SegmentId" });
            DropIndex("dbo.Integrations", new[] { "Application_ApplicationId" });
            DropIndex("dbo.Integrations", new[] { "ApplicationId" });
            DropIndex("dbo.Integrations", new[] { "TargetApplicationId" });
            DropIndex("dbo.Datasets", new[] { "ApplicationId" });
            DropIndex("dbo.Applications", new[] { "SupplierId" });
            DropIndex("dbo.Applications", new[] { "ServiceId" });
            DropIndex("dbo.Applications", new[] { "SegmentId" });
            DropIndex("dbo.Applications", new[] { "CompanyId" });
            DropTable("dbo.Suppliers");
            DropTable("dbo.Services");
            DropTable("dbo.Segments");
            DropTable("dbo.Integrations");
            DropTable("dbo.Datasets");
            DropTable("dbo.Companies");
            DropTable("dbo.Applications");
        }
    }
}
