using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace WatchWord.Infrastructure
{
    public class Angular2Middleware
    {
        private readonly RequestDelegate next;

        public Angular2Middleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.HasValue && !context.Request.Path.Value.StartsWith("/api/")
                && !context.Request.Path.Value.Equals("/")
                && !context.Request.Path.Value.Equals("/index.html")
                && !context.Request.Path.Value.Contains("."))
            {
                context.Request.Path = new PathString("/index.html");
            }
            await next.Invoke(context);
        }
    }
}