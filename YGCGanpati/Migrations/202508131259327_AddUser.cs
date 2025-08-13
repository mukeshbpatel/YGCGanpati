namespace YGCGanpati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Collections",
                c => new
                    {
                        CollectionID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        FlatNo = c.String(nullable: false),
                        CollectionDate = c.DateTime(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        UserProfile_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.CollectionID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserProfile_Id)
                .Index(t => t.UserProfile_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        FlatNo = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        EventID = c.Int(nullable: false, identity: true),
                        EventDate = c.DateTime(nullable: false),
                        EventName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.EventID);
            
            CreateTable(
                "dbo.Expenses",
                c => new
                    {
                        ExpenseID = c.Int(nullable: false, identity: true),
                        ExpenseDate = c.DateTime(nullable: false),
                        ExpenseAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ExpenseName = c.String(nullable: false),
                        Details = c.String(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        UserProfile_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ExpenseID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserProfile_Id)
                .Index(t => t.UserProfile_Id);
            
            CreateTable(
                "dbo.QuizAnswers",
                c => new
                    {
                        AnswerID = c.Int(nullable: false, identity: true),
                        QuestionID = c.Int(nullable: false),
                        Answer = c.Int(nullable: false),
                        AnswerDate = c.DateTime(nullable: false),
                        Points = c.Int(nullable: false),
                        UserProfile_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.AnswerID)
                .ForeignKey("dbo.QuizQuestions", t => t.QuestionID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserProfile_Id)
                .Index(t => t.QuestionID)
                .Index(t => t.UserProfile_Id);
            
            CreateTable(
                "dbo.QuizQuestions",
                c => new
                    {
                        QuestionID = c.Int(nullable: false, identity: true),
                        Question = c.String(nullable: false),
                        ImgURL = c.String(),
                        Option1 = c.String(nullable: false),
                        Option2 = c.String(nullable: false),
                        Option3 = c.String(nullable: false),
                        Option4 = c.String(nullable: false),
                        Answer = c.Int(nullable: false),
                        DisplayDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.QuestionID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.Sponsers",
                c => new
                    {
                        SponserID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        FlatNo = c.String(nullable: false),
                        SponserDate = c.DateTime(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Details = c.String(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        UserProfile_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.SponserID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserProfile_Id)
                .Index(t => t.UserProfile_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sponsers", "UserProfile_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.QuizAnswers", "UserProfile_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.QuizAnswers", "QuestionID", "dbo.QuizQuestions");
            DropForeignKey("dbo.Expenses", "UserProfile_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Collections", "UserProfile_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Sponsers", new[] { "UserProfile_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.QuizAnswers", new[] { "UserProfile_Id" });
            DropIndex("dbo.QuizAnswers", new[] { "QuestionID" });
            DropIndex("dbo.Expenses", new[] { "UserProfile_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Collections", new[] { "UserProfile_Id" });
            DropTable("dbo.Sponsers");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.QuizQuestions");
            DropTable("dbo.QuizAnswers");
            DropTable("dbo.Expenses");
            DropTable("dbo.Events");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Collections");
        }
    }
}
