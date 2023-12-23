using FluentMigrator;

namespace Gesfarma.Infrastructure.Persistence.Migrations;

[Migration(3)]
public class InsertProducts : Migration
{
    public override void Down()
    {
        throw new NotImplementedException();
    }

    public override void Up()
    {
        Execute.EmbeddedScript("InsertProducts.sql");
    }
}
