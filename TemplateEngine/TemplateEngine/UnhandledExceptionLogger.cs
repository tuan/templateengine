using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using Common.Logging;

namespace TemplateEngine
{
    public class UnhandledExceptionLogger : ExceptionLogger
    {
        private readonly ILogger log;
        public UnhandledExceptionLogger(ILogger logger)
        {
            this.log = logger;
        }

        public override void Log(ExceptionLoggerContext context)
        {
            log.Error(context.Exception, 
                "Unhandled exception processing {0} for {1}",
                context.Request.Method,
                context.Request.RequestUri);
        }
    }
}
