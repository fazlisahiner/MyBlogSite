using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BlogSite.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //HttpRequestValidationException.RequestValidationSource.AddDomains("pathToYourDomain");
            //HttpRequestValidationException.RequestValidationSource.Add(HttpRequestValidationException.RequestValidationSource.List, "/AdminPanel/Articles/MakaleGuncelle");
        }
    }
}
