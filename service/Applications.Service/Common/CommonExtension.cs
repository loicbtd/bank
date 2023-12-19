using Applications.Service.Common.DbContexts;

namespace Applications.Service.Common;

public static class CommonExtension
{
    public static WebApplicationBuilder WithCommon(this WebApplicationBuilder webApplicationBuilder)
    {
        webApplicationBuilder.Services.AddDbContext<DatabaseDbContext>();

        return webApplicationBuilder;
    }

    public static WebApplication WithCommon(this WebApplication webApplication)
    {
        return webApplication;
    }
}