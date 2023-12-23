using Carter;
using FluentMigrator.Runner;
using Gesfarma.Application.Clients.GetClients;
using Gesfarma.Application.Shopping.Message.Commands;
using Gesfarma.Infrastructure.Persistence;
using Gesfarma.Infrastructure.Persistence.Migrations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetClients).Assembly));
builder.Services.AddCarter();
builder.Services.AddScoped<SqlConnectionFactory>();

const string CorsPolicyName = "MyCorsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: CorsPolicyName, builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

//Connection Database
string stringConnection = Environment.GetEnvironmentVariable("SQL_CONNECTION");

builder.Services.AddFluentMigratorCore()
    .ConfigureRunner(rb => rb
        .WithGlobalCommandTimeout(new TimeSpan(1, 0, 0))
        .AddSqlServer2016()
        .WithGlobalConnectionString(stringConnection)
        .ScanIn(typeof(InitialSchema).Assembly)
        .For.All()
    )
    .AddLogging(lb => lb.AddFluentMigratorConsole());


//ServicesBus Connection
builder.Host.UseNServiceBus(context =>
{
    var endPointName = "Gesfarma.EndPoint";
    string rabbitmqUrl = Environment.GetEnvironmentVariable("RABBITMQ_DDD");
    var endpointConfiguration = new EndpointConfiguration(endPointName);
    var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
    transport.ConnectionString(rabbitmqUrl);
    transport.UseConventionalRoutingTopology(QueueType.Quorum);
    var routing = transport.Routing();
    routing.RouteToEndpoint(typeof(SalesOrderCar).Assembly, "Gesfarma.Hosts");
    endpointConfiguration.SendOnly();
    return endpointConfiguration;
});


// Add services to the container.
var app = builder.Build();
app.UseCors(CorsPolicyName);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using var serviceScope = app.Services.CreateScope();
var services = serviceScope.ServiceProvider;
var runner = services.GetRequiredService<IMigrationRunner>();
runner.MigrateUp();

app.MapCarter();
app.UseHttpsRedirection();
app.MapGet("", () =>
{
    return "";
})
.WithName("GetRoot"); //.WithOpenApi()

app.Run();