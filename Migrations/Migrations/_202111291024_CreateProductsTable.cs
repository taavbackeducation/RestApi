using FluentMigrator;

namespace Migrations.Migrations
{
    [Migration(202111291024)]
    public class _202111291024_CreateProductsTable : Migration
    {
        public override void Up()
        {
            Create.Table("Products")
                  .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                  .WithColumn("Price").AsDouble().NotNullable()
                  .WithColumn("Title").AsString(50).NotNullable()
                  .WithColumn("Stock").AsInt32().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("Products");
        }
    }
}
