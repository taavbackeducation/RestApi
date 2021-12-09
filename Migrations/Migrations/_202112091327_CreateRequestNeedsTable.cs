using FluentMigrator;
using System.Data;

namespace Migrations.Migrations
{
    [Migration(202112091327)]
    public class _202112091327_CreateRequestNeedsTable : Migration
    {
        public override void Up()
        {
            Create.Table("RequestNeeds")
                  .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                  .WithColumn("Count").AsInt32().NotNullable()
                  .WithColumn("Section").AsString().NotNullable()
                  .WithColumn("ProductId").AsInt32().NotNullable()
                    .ForeignKey(foreignKeyName: "FK_RequestNeeds_Products",
                                primaryTableName: "Products",
                                primaryColumnName: "Id").OnDelete(Rule.None);
        }

        public override void Down()
        {
            Delete.ForeignKey("FK_RequestNeeds_Products").OnTable("RequestNeeds");
            Delete.Table("RequestNeeds");
        }
    }
}
