using Microsoft.Extensions.Configuration;
using System;
using System.Net;

namespace WatchWord.Service.Infrastructure
{
    public class WatchWordProxy : IWebProxy
    {
        private readonly IConfiguration configuration;
        private readonly string adress;

        public WatchWordProxy(IConfiguration configuration)
        {
            this.configuration = configuration;
            adress = configuration["Proxy:Address"];
        }

        public Uri GetProxy(Uri destination)
        {
            return new Uri(adress);
        }

        public bool IsBypassed(Uri host)
        {
            return false;
        }

        public ICredentials Credentials { get; set; }
    }
}