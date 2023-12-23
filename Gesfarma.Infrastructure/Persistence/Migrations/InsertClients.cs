using FluentMigrator;
namespace Gesfarma.Infrastructure.Persistence.Migrations;

[Migration(2)]
public class InsertClients : Migration
{
    public override void Down()
    {
        throw new NotImplementedException();
    }

    public override void Up()
    {
        Execute.EmbeddedScript("InsertClients.sql");
    }
}
