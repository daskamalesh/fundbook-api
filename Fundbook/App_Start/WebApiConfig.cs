using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Fundbook
{
    public static class WebApiConfig
    {
        //public static void Register(HttpConfiguration config)
        //{
        //    var cors = new EnableCorsAttribute("*", "*", "*");
        //    config.EnableCors(cors);

        //    config.MapHttpAttributeRoutes();

        //    config.Routes.MapHttpRoute(
        //        name: "DefaultApi",
        //        routeTemplate: "loginapi/{controller}/{action}/{id}",
        //        defaults: new { id = RouteParameter.Optional }
        //    );
        //}
        public static void Register(HttpConfiguration config)
        {
            config.MessageHandlers.Add(new PreflightRequestHandler());

            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "loginapi/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        //public class PreflightRequestHandler : DelegatingHandler
        //{
        //    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        //    {
        //        if (request.Method == HttpMethod.Options)
        //        {
        //            var response = new HttpResponseMessage(HttpStatusCode.OK);
        //            response.Headers.Add("Access-Control-Allow-Origin", "*");
        //            response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
        //            response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Authorization");
        //            return response;
        //        }

        //        return await base.SendAsync(request, cancellationToken);
        //    }
        //}
        public class PreflightRequestHandler : DelegatingHandler
        {
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                if (request.Method == HttpMethod.Options)
                {
                    var response = new HttpResponseMessage(HttpStatusCode.OK);
                    response.Headers.Add("Access-Control-Allow-Origin", "*");
                    response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
                    response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Authorization, Accept");
                    response.Headers.Add("Access-Control-Allow-Credentials", "true");
                    return response;
                }

                return await base.SendAsync(request, cancellationToken);
            }
        }


    }
}
