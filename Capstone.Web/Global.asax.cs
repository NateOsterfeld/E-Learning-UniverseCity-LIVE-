using System.Configuration;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using Ninject.Web.Common.WebHost;

namespace Capstone.Web
{
    public class MvcApplication : NinjectHttpApplication
    {
        protected override void OnApplicationStarted()
        {
            base.OnApplicationStarted();

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);            
        }

        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            string connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            kernel.Bind<IDatabaseDAL>().To<DatabaseDAL>().WithConstructorArgument("connectionString", connectionString);
            //kernel.Bind<IDatabaseDAL>().To<MockDatabaseDAL>().WithConstructorArgument("connectionString", connectionString);

            return kernel;
        }
    }
}
