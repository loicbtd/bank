using Applications.Service.Common.DbContexts;
using Applications.Service.Modules.Bank.Interfaces;
using Applications.Service.Modules.Bank.Services;
using Xunit;
using FluentAssertions;
using Libraries.Tests.Common.Constants;
using Microsoft.EntityFrameworkCore;

namespace Libraries.Tests;

public class CurrentAccountServiceShould
{
    [Fact]
    public async Task BeAbleToOpenAnAccount()
    {
        // Arrange
        DatabaseDbContext databaseDbContext = new DatabaseDbContext();
        databaseDbContext.Customers.AddRange(InitialDatabaseValuesConstant.Customers);
        await databaseDbContext.SaveChangesAsync();
        ITransactionService transactionService = new TransactionService(databaseDbContext);
        ICurrentAccountService currentAccountService = new CurrentAccountService(databaseDbContext, transactionService);

        // Act
        await currentAccountService.OpenNewCurrentAccount(new()
        {
            CustomerId = "withoutAccount1"
        });

        // Assert
        (await databaseDbContext.Customers
                .Include(_ => _.CurrentAccount)
                .Where(_ => _.CustomerId == "withoutAccount1")
                .FirstOrDefaultAsync())
            .CurrentAccount
            .Should()
            .NotBeNull();

        // Clean
        await databaseDbContext.Database.EnsureDeletedAsync();
    }

    [Fact]
    public async Task PerformATransactionAfterCurrentAccountOpeningIfInitialCreditIsGreaterThan0()
    {
        // Arrange
        DatabaseDbContext databaseDbContext = new();
        databaseDbContext.Customers.AddRange(InitialDatabaseValuesConstant.Customers);
        await databaseDbContext.SaveChangesAsync();
        ITransactionService transactionService = new TransactionService(databaseDbContext);
        ICurrentAccountService currentAccountService = new CurrentAccountService(databaseDbContext, transactionService);

        // Act
        await currentAccountService.OpenNewCurrentAccount(new()
        {
            CustomerId = "withoutAccount2",
            InitialCredit = 1000
        });

        // Assert
        (await databaseDbContext.Transactions.FirstOrDefaultAsync()).Amount.Should().Be(1000);

        // Clean
        await databaseDbContext.Database.EnsureDeletedAsync();
    }
}