using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Net.Mime;
using System.Text.Json;

namespace ETıcaretAPI.API.Extensions
{
    static public class ConfigureExceptionHandlerExtension
    {
        public static void ConfiguureExceptionHandler<T>(this WebApplication application, ILogger<T> logger)
        {
            application.UseExceptionHandler(builder =>
            {
                builder.Run(async contex =>
                {
                    contex.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    contex.Response.ContentType = MediaTypeNames.Application.Json;
                    var contextFeature = contex.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        logger.LogError(contextFeature.Error.Message);
                        await contex.Response.WriteAsync(JsonSerializer.Serialize(new
                        {
                            StatusCode = contex.Response.StatusCode,
                            Message = contextFeature.Error.Message,
                            Title = "Hata Alındı"
                        }));
                    }
                });
            });

        }
    }
}
