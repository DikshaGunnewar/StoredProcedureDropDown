namespace CrudUsingSpMVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities1",
                c => new
                    {
                        CityId = c.Int(nullable: false, identity: true),
                        CityName = c.String(),
                        StateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CityId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.States1",
                c => new
                    {
                        StateId = c.Int(nullable: false, identity: true),
                        StateName = c.String(),
                    })
                .PrimaryKey(t => t.StateId);
            
            CreateTable(
                "dbo.Address",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        CurrentAddress = c.String(),
                        PermanantAddress = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        State = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        City = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Id)
                .Index(t => t.Id);
            
            CreateStoredProcedure(
                "dbo.CustomerVM_Insert",
                p => new
                    {
                        Name = p.String(),
                        Email = p.String(),
                        CurrentAddress = p.String(),
                        PermanantAddress = p.String(),
                        State = p.Int(),
                        City = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[Customers]([Name], [Email])
                      VALUES (@Name, @Email)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[Customers]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      INSERT [dbo].[Address]([Id], [CurrentAddress], [PermanantAddress])
                      VALUES (@Id, @CurrentAddress, @PermanantAddress)
                      
                      INSERT [dbo].[States]([Id], [State])
                      VALUES (@Id, @State)
                      
                      INSERT [dbo].[Cities]([Id], [City])
                      VALUES (@Id, @City)
                      
                      SELECT t0.[Id]
                      FROM [dbo].[Customers] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.CustomerVM_Update",
                p => new
                    {
                        Id = p.Int(),
                        Name = p.String(),
                        Email = p.String(),
                        CurrentAddress = p.String(),
                        PermanantAddress = p.String(),
                        State = p.Int(),
                        City = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[Customers]
                      SET [Name] = @Name, [Email] = @Email
                      WHERE ([Id] = @Id)
                      
                      UPDATE [dbo].[Address]
                      SET [CurrentAddress] = @CurrentAddress, [PermanantAddress] = @PermanantAddress
                      WHERE ([Id] = @Id)
                      AND @@ROWCOUNT > 0
                      
                      UPDATE [dbo].[States]
                      SET [State] = @State
                      WHERE ([Id] = @Id)
                      AND @@ROWCOUNT > 0
                      
                      UPDATE [dbo].[Cities]
                      SET [City] = @City
                      WHERE ([Id] = @Id)
                      AND @@ROWCOUNT > 0"
            );
            
            CreateStoredProcedure(
                "dbo.CustomerVM_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Address]
                      WHERE ([Id] = @Id)
                      
                      DELETE [dbo].[States]
                      WHERE ([Id] = @Id)
                      AND @@ROWCOUNT > 0
                      
                      DELETE [dbo].[Cities]
                      WHERE ([Id] = @Id)
                      AND @@ROWCOUNT > 0
                      
                      DELETE [dbo].[Customers]
                      WHERE ([Id] = @Id)
                      AND @@ROWCOUNT > 0"
            );
            
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.CustomerVM_Delete");
            DropStoredProcedure("dbo.CustomerVM_Update");
            DropStoredProcedure("dbo.CustomerVM_Insert");
            DropForeignKey("dbo.Cities", "Id", "dbo.Customers");
            DropForeignKey("dbo.States", "Id", "dbo.Customers");
            DropForeignKey("dbo.Address", "Id", "dbo.Customers");
            DropIndex("dbo.Cities", new[] { "Id" });
            DropIndex("dbo.States", new[] { "Id" });
            DropIndex("dbo.Address", new[] { "Id" });
            DropTable("dbo.Cities");
            DropTable("dbo.States");
            DropTable("dbo.Address");
            DropTable("dbo.States1");
            DropTable("dbo.Customers");
            DropTable("dbo.Cities1");
        }
    }
}
