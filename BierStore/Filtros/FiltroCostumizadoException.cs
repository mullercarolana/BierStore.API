using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace BierStore.Filtros
{
    public class FiltroCostumizadoException : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            string errorMessage = context.Exception.Message;
            string stackTrace = context.Exception.StackTrace;

            var response = context.HttpContext.Response;

            var result = JsonConvert.SerializeObject(
                new
                {
                    Message = errorMessage,
                    StackTrace = stackTrace
                });

            response.WriteAsync(result);
        }
    }
}
