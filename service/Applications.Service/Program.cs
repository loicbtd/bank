using Applications.Service.Common;
using Applications.Service.Modules.Bank;
using Libraries.Web.Common;
using Libraries.Web.Common.Models;

CommonConfigurationModel commonConfiguration = new()
{
    SwaggerEnabled = EnvironmentConstant.SwaggerEnabled,
};

WebApplicationBuilder webApplicationBuilder = WebApplication
    .CreateBuilder()
    .WithCommon(commonConfiguration)
    .WithCommon()
    .WithBank();

WebApplication webApplication = webApplicationBuilder
    .Build()
    .WithCommon(commonConfiguration)
    .WithCommon()
    .WithBank();

webApplication.Run();
