using FluentMigrator;


namespace CardApplication.DBMigrate.Migrations
{
    [Migration(1)]
    public class M0001_CreateCardTable : Migration
    {
        public override void Down()
        {
            Delete.Table("Card");
        }

        public override void Up()
        {
            Create.Table("Card")
                .WithColumn("CardNumber").AsInt64().PrimaryKey().NotNullable()
                .WithColumn("CardGuid").AsGuid().NotNullable()
                .WithColumn("Name").AsString(50).NotNullable()
                .WithColumn("Expiry").AsDateTime().NotNullable();
        }
    }
}
