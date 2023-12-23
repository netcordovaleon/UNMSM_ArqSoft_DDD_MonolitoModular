using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gesfarma.Infrastructure.Persistence.Migrations;

[Migration(4)]
public class OutboxTable : Migration
{
    public override void Down()
    {
        throw new NotImplementedException();
    }

    public override void Up()
    {
        Execute.EmbeddedScript("OutboxTable.sql");
    }
}
