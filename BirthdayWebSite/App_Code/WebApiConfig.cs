﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

/// <summary>
/// Summary description for WebApiConfig
/// </summary>
public static class WebApiConfig
{
    public static void Register(HttpConfiguration config)
    {
        // Web API configuration and services
        // Configure Web API to use only bearer token authentication.
        //config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
        //config.SuppressDefaultHostAuthentication();
        //config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

        // Web API routes
        config.MapHttpAttributeRoutes();

//        config.Routes.MapHttpRoute(
//          name: "ActionApi",
//          routeTemplate: "api/{controller}/{action}/{id}",
//          defaults: new { id = RouteParameter.Optional }
//);
        config.Routes.MapHttpRoute(
            name: "DefaultApi",
            routeTemplate: "api/{controller}/{id}",
            defaults: new { id = RouteParameter.Optional }
        );
        
    }
}