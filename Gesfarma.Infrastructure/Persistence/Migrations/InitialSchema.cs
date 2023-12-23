using FluentMigrator;

namespace Gesfarma.Infrastructure.Persistence.Migrations;

[Migration(1)]
public class InitialSchema : Migration
{
    public override void Up()
    {
        Execute.EmbeddedScript("InitialScript.sql");
    }

    public override void Down()
    {
    }
}
