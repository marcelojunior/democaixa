using System;
using Dados.Infra;
using Microsoft.Extensions.Configuration;

namespace Api
{
    public class ContextoApi : Contexto
    {

        public ContextoApi()
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
            .Build();

            this.ConnectionString = configuration.GetConnectionString("DefaultConnection");
        }
    }
}
