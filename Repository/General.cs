
using System.Data.SqlClient;

namespace EntregaCoder.Repository
{
    public class General
    {
      public static string connectionString()
        {
            SqlConnectionStringBuilder connectionBuilder = new();
            connectionBuilder.DataSource = "LAPTOP-ANAQNMU4";
            connectionBuilder.InitialCatalog = "SistemaGestion";
            connectionBuilder.IntegratedSecurity = true;
            var cs = connectionBuilder.ConnectionString;
            return cs;
        }
    }
}
