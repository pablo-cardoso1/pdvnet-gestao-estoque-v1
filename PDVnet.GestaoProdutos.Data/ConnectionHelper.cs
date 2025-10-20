using Microsoft.Data.SqlClient;
using System;   
using System.Configuration;

namespace PDVnet.GestaoProdutos.Data
{
    public static class ConnectionHelper
    {
        public static string GetConnectionString()
        {
            var cs = ConfigurationManager.ConnectionStrings["DefaultConnection"]?.ConnectionString;
            if (string.IsNullOrWhiteSpace(cs))
            {
                throw new InvalidOperationException("Connection string 'DefaultConnection' não encontrada. Verifique o arquivo de configuração (app.config) e se o projeto executável contém a connection string.");
            }
            return cs;
        }

        public static SqlConnection CreateConnection()
        {
            var cs = GetConnectionString();
            return new SqlConnection(cs);
        }
    }
}