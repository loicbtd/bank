using Applications.Service.Common.DbContexts;
using Applications.Service.Common.Entities;
using Applications.Service.Modules.Bank.Interfaces;
using Applications.Service.Modules.Bank.Requests;
using Libraries.Web.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Applications.Service.Modules.Bank.Services;

public class CurrentAccountService : ICurrentAccountService
{
    private readonly DatabaseDbContext _databaseDbContext;
    private readonly ITransactionService _transactionService;

    public CurrentAccountService(DatabaseDbContext databaseDbContext, ITransactionService transactionService)
    {
        _databaseDbContext = databaseDbContext;
        _transactionService = transactionService;
    }

    public async Task OpenNewCurrentAccount(OpenNewCurrentAccountRequest request)
    {
        if (string.IsNullOrEmpty(request.CustomerId))
        {
            throw new UserException("the id of the customer is mandatory");
        }

        CustomerEntity customerEntity = await _databaseDbContext.Customers
            .Include(_ => _.CurrentAccount)
            .Where(_ => _.CustomerId == request.CustomerId)
            .FirstOrDefaultAsync();

        if (customerEntity is null)
        {
            throw new UserException("the customer does not exist");
        }

        if (customerEntity.CurrentAccount is not null)
        {
            throw new UserException("the customer already owns a current account");
        }
        
        customerEntity.CurrentAccount = new CurrentAccountEntity();
        
        _databaseDbContext.Customers.Update(customerEntity);

        await _databaseDbContext.SaveChangesAsync();

        if (request.InitialCredit > 0)
        {
            await _transactionService.PerformTransaction(new PerformTransactionRequest
            {
                Amount = request.InitialCredit,
                RecipientCustomerId = request.CustomerId
            });
        }
    }
}