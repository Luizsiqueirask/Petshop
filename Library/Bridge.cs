using System.Configuration;
using System.Web.Configuration;

namespace Library
{
    public class Bridge
    {
        public string Connect()
        {
            try
            {
                var PathConnection = WebConfigurationManager.ConnectionStrings["Petshop"].ConnectionString;
                return PathConnection;
            }
            catch
            {
                var PathConnection = ConfigurationManager.ConnectionStrings["Petshop"].ConnectionString;
                return PathConnection;
            }
        }
    }
}
