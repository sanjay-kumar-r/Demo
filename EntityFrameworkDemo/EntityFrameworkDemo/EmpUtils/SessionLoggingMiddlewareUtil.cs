using Abp.Dependency;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Diagnostics;
using Utils.Logger;

namespace EntityFrameworkDemo.EmpUtils
{
    public static class SessionLoggingMiddlewareUtil
    {
        public static void UseSessionLogging(this IApplicationBuilder app)
        {
            app.UseMiddleware<SessionLoggingMiddleware>();
        }
    }

    public class SessionLoggingMiddleware : IMiddleware, ITransientDependency
    {
        private readonly IAbpSession _session;

        public SessionLoggingMiddleware(IAbpSession session)
        {
            _session = session;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            string traceId = Activity.Current?.Id ?? context?.TraceIdentifier;
            string operationName = context?.Request?.Path;
            string userId = context?.Request?.Headers["UserId"];
            string tenantId = context?.Request?.Headers["tenantId"];
            LoggerUtils.SetLogicalThreadContext(null, traceId, operationName, userId, tenantId);
            var x = _session.UserId;
            var y = _session.TenantId;
            await next(context);
        }
    }
}
