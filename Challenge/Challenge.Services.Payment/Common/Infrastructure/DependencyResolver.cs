using System;
using AutoMapper;
using Challenge.Services.Payment.Mapper;
using Challenge.Services.Payment.Repository;
using Challenge.Services.Payment.Service.Implementation;
using Challenge.Services.Payment.Service.Protocol;
using LUM.Services.Material.Common.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Raven.Client.Documents;

namespace Challenge.Services.Payment.Common.Infrastructure
{
    public static class DependencyResolver
    {
        public static void Resolve(this IServiceCollection services, Setting setting)
        {

            services.AddSingleton<IDocumentStore>(x =>
                {
                    var store = new DocumentStore()
                    {
                        Urls = new[] { setting.Database.Address },
                        Database = setting.Database.Name,

                        Conventions = { }
                    }.Initialize();
                    (new DatabaseInitializer().EnsureDatabaseExistsAsync(store, setting.Database.Name)).GetAwaiter().GetResult();
                    //IndexCreation.CreateIndexes(typeof(MaterialSearchByNameIndex).Assembly, store);
                    return store;
                }
            );

            services.AddTransient<PaymentRepository>();
            services.AddTransient<IPaymentService, PaymentService>();
            services.AddTransient<IBankService, InMemoryBankService>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo() { Title = "Checkout Payment Microservice", Version = "v1" });
            });

            services.AddAutoMapper(
                configuration => configuration.AddProfile<PaymentProfile>(),
                AppDomain.CurrentDomain.GetAssemblies());


        }
        private static string GetXmlCommentsPath()
        {
            var app = System.AppContext.BaseDirectory;
            return System.IO.Path.Combine(app, "Challenge.Services.Payment.xml");
        }
    }
}

