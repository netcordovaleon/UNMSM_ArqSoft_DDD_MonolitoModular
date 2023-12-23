﻿using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Helpers;
using Gesfarma.Application.SaleOrder.Handler.Sagas.SagaData;
using Gesfarma.Application.Shopping.Message.Commands;
using Gesfarma.Infrastructure.NSB;
using Gesfarma.Infrastructure.Persistence;
using Gesfarma.Infrastructure.Persistence.NHibernate;
using Microsoft.Extensions.Hosting;
using NHibernate.Cfg;
using NServiceBus.NHibernate.Outbox;
using NServiceBus.Persistence;
using NHEnvironment = NHibernate.Cfg.Environment;

namespace Gesfarma.Application.SaleOrder.Handler;

class Program
{
    static async Task Main(string[] args)
    {

        Console.Title = "Gesfarma.Application.SaleOrder.Handler";
        var host = CreateHostBuilder(args).Build();
        await host.StartAsync();
        Console.WriteLine("Press any key to shutdown");
        Console.ReadKey();
        await host.StopAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseMicrosoftLogFactoryLogging()
            .UseNServiceBus(hostBuilderContext =>
            {
                var endpointConfiguration = ConfigureEndpoint("Gesfarma.Saga");
                return endpointConfiguration;
            });

    public static EndpointConfiguration ConfigureEndpoint(string endpointName)
    {
        var endpointConfiguration = new EndpointConfiguration(endpointName);
        endpointConfiguration.EnableInstallers();
        endpointConfiguration.EnableOutbox();
        endpointConfiguration.AuditProcessedMessagesTo("audit");
        var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
        string rabbitmqUrl = "amqps://uvxqnobb:UYewpGtjux7ezOBgdCSJpn9S_iCoDajJ@jackal.rmq.cloudamqp.com/uvxqnobb"; //System.Environment.GetEnvironmentVariable("RABBITMQ_DDD");
        transport.ConnectionString(rabbitmqUrl);
        transport.UseConventionalRoutingTopology(QueueType.Quorum);
        var persistence = endpointConfiguration.UsePersistence<NHibernatePersistence>();
        persistence.UseOutboxRecord<Outbox, OutboxMap>();
        var nHibernateConfig = new Configuration();
        nHibernateConfig.SetProperty(NHEnvironment.ConnectionProvider, typeof(NHibernate.Connection.DriverConnectionProvider).FullName);
        nHibernateConfig.SetProperty(NHEnvironment.ConnectionDriver, typeof(NHibernate.Driver.MicrosoftDataSqlClientDriver).FullName);
        nHibernateConfig.SetProperty(NHEnvironment.Dialect, typeof(NHibernate.Dialect.MsSql2012Dialect).FullName);
        string stringConnection = "Server=localhost\\SQLEXPRESS;Database=Gesfarma;Integrated Security=true;TrustServerCertificate=true;"; //System.Environment.GetEnvironmentVariable("SQL_CONNECTION");
        nHibernateConfig.SetProperty(NHEnvironment.ConnectionString, stringConnection);
        AddFluentMappings(nHibernateConfig, stringConnection);
        persistence.UseConfiguration(nHibernateConfig);
        persistence.DisableSchemaUpdate();
        var routing = transport.Routing();
        routing.RouteToEndpoint(typeof(SalesOrderCar).Assembly, "Gesfarma.Handler");
        return endpointConfiguration;

    }

    private static Configuration AddFluentMappings(Configuration nhConfiguration, string stringConnection)
    {
        return Fluently
            .Configure(nhConfiguration)
            .Database(MsSqlConfiguration.MsSql2012.ConnectionString(stringConnection))
            .Mappings(cfg =>
            {
                cfg.FluentMappings.AddFromAssembly(typeof(ShoppingMap).Assembly);
                cfg.FluentMappings.Conventions.Add(
                    ForeignKey.EndsWith("_id"),
                    ConventionBuilder.Property.When(criteria => criteria.Expect(x => x.Nullable, Is.Not.Set), x => x.Not.Nullable()));
                cfg.FluentMappings.Conventions.Add<OtherConversions>();
            })
            .Mappings(cfg =>
            {
                cfg.FluentMappings.AddFromAssembly(typeof(SaleOrderSagaDataMap).Assembly);
            })
            .BuildConfiguration();
    }
}
