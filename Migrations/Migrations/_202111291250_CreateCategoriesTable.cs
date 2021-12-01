using FluentMigrator;
using System.Data;

namespace Warehouse.Migrations.Migrations
{
    [Migration(202111291250)]
    public class _202111291250_CreateCategoriesTable : Migration
    {
        public override void Up()
        {
            Create.Table("Categories")
                  .WithColumn("Id").AsInt32().PrimaryKey().NotNullable().Identity()
                  .WithColumn("Title").AsString(50).NotNullable();

            Alter.Table("Products")
                 .AddColumn("CategoryId").AsInt32().Nullable()
                    .ForeignKey(foreignKeyName: "FK_Products_Categories",
                                primaryTableName: "Categories",
                                primaryColumnName: "Id").OnDelete(Rule.Cascade);
        }

        public override void Down()
        {
            Delete.ForeignKey("FK_Products_Categories").OnTable("Products");
            Delete.Column("CategoryId").FromTable("Products");
            Delete.Table("Categories");
        }
    }
}
