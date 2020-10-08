using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.Filters {
    public class LogFilter : IActionFilter, IOrderedFilter {
        private readonly ILogger<LogFilter> logger;

        public int Order => -10;
        public LogFilter(ILogger<LogFilter> logger) {
            this.logger = logger;
        }
        public void OnActionExecuted(ActionExecutedContext context) {
            string actionName = context.ActionDescriptor.DisplayName;
            logger.LogDebug(string.Format("Executed {0} at {1}", actionName, DateTime.UtcNow));
        }

        public void OnActionExecuting(ActionExecutingContext context) {
            string actionName = context.ActionDescriptor.DisplayName;
            logger.LogDebug(string.Format("Executing {0} at {1}", actionName, DateTime.UtcNow));
        }
    }
}
