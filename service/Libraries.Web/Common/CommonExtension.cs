using System.Text;
using Libraries.Web.Common.ControllerModelConventions;
using Libraries.Web.Common.Exceptions;
using Libraries.Web.Common.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.OpenApi.Models;

namespace Libraries.Web.Common;

public static class CommonExtension
{
    public static WebApplicationBuilder WithCommon(this WebApplicationBuilder webApplicationBuilder, CommonConfigurationModel configuration)
    {
        webApplicationBuilder.Services.AddEndpointsApiExplorer();

        webApplicationBuilder.Services.AddRouting();
        
        webApplicationBuilder.Services.AddControllers(options =>
        {
            options.Conventions.Add(new GlobalControllerModelConvention());
        });
        
        if (configuration.SwaggerEnabled)
        {
            webApplicationBuilder.Services.AddSwaggerGen(options => options.SwaggerDoc("default",
                new OpenApiInfo() { Title = "Api", Version = "default"}));
        }

        webApplicationBuilder.Services.AddHttpContextAccessor();

        webApplicationBuilder.Services.AddCors(options =>
        {
            options.AddPolicy("default", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
        });
        
        return webApplicationBuilder;
    }

    public static WebApplication WithCommon(this WebApplication webApplication, CommonConfigurationModel configuration)
    {
        if (configuration.SwaggerEnabled)
        {
            webApplication.UseSwagger(options => { options.RouteTemplate = "{documentName}/swagger.json"; });
            webApplication.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint($"default/swagger.json", "default");
                options.RoutePrefix = string.Empty;
            });
        }

        webApplication.UseExceptionHandler(exceptionHandlerApp =>
        {
            exceptionHandlerApp.Run(context =>
            {
                IExceptionHandlerPathFeature? exceptionHandlerPathFeature =
                    context.Features.Get<IExceptionHandlerPathFeature>();

                if (exceptionHandlerPathFeature is not null)
                {
                    context.Response.StatusCode = exceptionHandlerPathFeature?.Error switch
                    {
                        UserException => StatusCodes.Status400BadRequest,
                        System.ApplicationException => StatusCodes.Status500InternalServerError,
                        _ => StatusCodes.Status500InternalServerError
                    };
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                }

                if (exceptionHandlerPathFeature.Error is UserException userException)
                {
                    context.Response.ContentType = "text/plain";
                    byte[] messageBytes = Encoding.UTF8.GetBytes(userException.Message);
                    context.Response.Body.WriteAsync(messageBytes, 0, messageBytes.Length);
                }

                return Task.CompletedTask;
            });
        });

        webApplication.MapControllers();

        webApplication.UseCors("default");
        
        return webApplication;
    }
}