using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DataAccess {
    public class BaseDAL {
        public StockDataProvider dataProvider { get; set; } = null;
        public SqlConnection connection = null;

        public BaseDAL() {
            var connectionstring = GetConnectionString();
            dataProvider = new StockDataProvider(connectionstring);
        }

        public string GetConnectionString() {
            IConfiguration config = new ConfigurationBuilder()
                                        .SetBasePath(Directory.GetCurrentDirectory())
                                        .AddJsonFile("appsettings.json", true, true)
                                        .Build();
            var connectionString = config["ConnectionString:MemberManagementDB"];
            return connectionString;
        }

        public void CloseConnection() => dataProvider.CloseConnection(connection);
    }
}
