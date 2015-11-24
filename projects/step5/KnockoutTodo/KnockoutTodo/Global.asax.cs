using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;
using System.Web.Optimization;
using System.Data.Entity;
using KnockoutTodo.Models;
using KnockoutTodo.Migrations;

namespace KnockoutTodo
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // アプリケーションのスタートアップで実行するコードです
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // css
            BundleTable.Bundles.Add(new StyleBundle("~/bundle/style").Include(
                "~/Content/bootstrap.css",
                "~/Content/bootstrap-theme.css",
                "~/Content/base.css"));
            // js
            BundleTable.Bundles.Add(new ScriptBundle("~/bundle/script").Include(
                "~/Scripts/jquery-1.9.1.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/knockout-3.3.0.js",
                "~/Scripts/app.js"));

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TodoesContext, Configuration>());
        }
    }
}