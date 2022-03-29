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
                var PathConnection = WebConfigurationManager.ConnectionStrings["Petship"].ConnectionString;
                return PathConnection;
            }
            catch
            {
                var PathConnection = ConfigurationManager.ConnectionStrings["Petship"].ConnectionString;
                return PathConnection;
            }
        }
    }
}
