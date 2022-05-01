using System.Configuration;
using System.Web.Configuration;

namespace Library
{
    public class Bridge
    {
        protected string PathConnection = null;

        public string Connect()
        {
            try
            {
                PathConnection = WebConfigurationManager.ConnectionStrings["Petshop"].ConnectionString;
                return PathConnection;
            }
            catch
            {
                PathConnection = ConfigurationManager.ConnectionStrings["Petshop"].ConnectionString;
                return PathConnection;
            }
        }
    }
}
