using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
namespace DL
{
    //CONEXION A BASE DE DATOS UTILIZANDO NET CORE Y LAS APPSETTINGS
    //CAPA DL
    public class Conexion
    {
        public static string GetConnectionString(string connectionString)
        {
            IConfigurationRoot Configuration;
            var builder = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false);
            try
            {
                Configuration = builder.Build();
                return Configuration.GetSection(connectionString).Value;
            }
            finally
            {
                builder = null;
                Configuration = null;
            }
        }
    }
}
