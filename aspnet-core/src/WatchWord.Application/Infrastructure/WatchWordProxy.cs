using System;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace WatchWord.Infrastructure
{
    public class WatchWordProxy : IWebProxy
    {
        private readonly string _address;
        public ICredentials Credentials { get; set; }

        public WatchWordProxy(IConfiguration configuration)
        {
            _address = configuration["Proxy:Address"];
        }

        public Uri GetProxy(Uri destination)
        {
            return new Uri(_address);
        }

        public bool IsBypassed(Uri host)
        {
            return false;
        }
    }
}
