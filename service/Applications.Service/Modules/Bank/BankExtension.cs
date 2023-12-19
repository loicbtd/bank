using Applications.Service.Common.Constants;
using Applications.Service.Common.DbContexts;
using Applications.Service.Common.Entities;
using Applications.Service.Modules.Bank.Interfaces;
using Applications.Service.Modules.Bank.Services;

namespace Applications.Service.Modules.Bank;

public static class BankExtension
{
    public static WebApplicationBuilder WithBank(this WebApplicationBuilder webApplicationBuilder)
    {
        webApplicationBuilder.Services.AddScoped<ICurrentAccountService, CurrentAccountService>();
        webApplicationBuilder.Services.AddScoped<ICustomerService, CustomerService>();
        webApplicationBuilder.Services.AddScoped<ITransactionService, TransactionService>();

        return webApplicationBuilder;
    }

    public static WebApplication WithBank(this WebApplication webApplication)
    {
        DatabaseDbContext? databaseDbContext = webApplication.Services.GetService<DatabaseDbContext>();

        databaseDbContext?.Customers.AddRange(InitialDatabaseValuesConstant.Customers);

        databaseDbContext?.SaveChanges();
        
        return webApplication;
    }
}