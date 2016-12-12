using Bidme.MVC.Web.App_Start;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Bidme.MVC.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //iniciar webservice com o web
            GlobalConfiguration.Configure(WebApiConfig.Register);

            //iniciar web
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
