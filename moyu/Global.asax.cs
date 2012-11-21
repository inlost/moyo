using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Routing;
namespace moyu
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            RegisteRoutes(RouteTable.Routes);
        }

        private void RegisteRoutes(RouteCollection routes)
        {
            //routes.MapPageRoute("", "", "~/Default.aspx");
            //routes.MapPageRoute("list", "Items/{action}", "~/Items/list.aspx", false, new RouteValueDictionary { { "action", "all" } });
            //routes.MapPageRoute("show", "Show/{action}", "~/show.aspx", false, new RouteValueDictionary { { "action", "all" } });
            //routes.MapPageRoute("edit", "Edit/{id}", "~/edit.aspx", false, new RouteValueDictionary { { "id", "1" } }, new RouteValueDictionary { { "id", @"\d+" } });
            routes.MapPageRoute("Channel", "{Channel}/{url}", "~/routes.aspx");
            routes.MapPageRoute("Detail", "{Channel}/{url}/{parameter}", "~/routes.aspx");
            routes.MapPageRoute("Home", "首页_沁辰左邻", "~/default.aspx");
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}