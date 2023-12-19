using Applications.Service.Common.DbContexts;
using Applications.Service.Common.Entities;
using Applications.Service.Modules.Bank.Interfaces;
using Applications.Service.Modules.Bank.Model;
using Applications.Service.Modules.Bank.Requests;
using Libraries.Web.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Applications.Service.Modules.Bank.Services;

public class TransactionService : ITransactionService
{
    private readonly DatabaseDbContext _databaseDbContext;

    public TransactionService(DatabaseDbContext databaseDbContext)
    {
        _databaseDbContext = databaseDbContext;
    }

    public async Task PerformTransaction(PerformTransactionRequest request)
    {
        if (request.Amount <= 0)
        {
            await _databaseDbContext.Database.RollbackTransactionAsync();
            throw new UserException("the amount of the transaction must be higher than 0");
        }
        
        CustomerEntity sender = null;

        if (!string.IsNullOrEmpty(request.SenderCustomerId))
        {
            sender = await _databaseDbContext.Customers
                .Include(_ => _.CurrentAccount)
                .Where(_ => _.CustomerId == request.SenderCustomerId)
                .FirstOrDefaultAsync();

            if (sender is null)
            {
                throw new UserException("unable to find the sender customer");
            }

            if (sender.CurrentAccount is null)
            {
                throw new UserException("the sender customer has no current account");
            }

            if (sender.CurrentAccount.Balance < request.Amount)
            {
                throw new UserException("the transaction amount must be greater than 0");
            }

            sender.CurrentAccount.Balance -= request.Amount;
            _databaseDbContext.Update(sender);
        }

        CustomerEntity recipient = await _databaseDbContext.Customers
            .Include(_ => _.CurrentAccount)
            .Where(_ => _.CustomerId == request.RecipientCustomerId)
            .FirstOrDefaultAsync();

        if (recipient is null)
        {
            throw new UserException("unable to find the recipient customer");
        }

        if (recipient.CurrentAccount is null)
        {
            throw new UserException("the recipient customer has no current account");
        }

        recipient.CurrentAccount.Balance += request.Amount;
        _databaseDbContext.Update(recipient);

        _databaseDbContext.Add(new TransactionEntity()
        {
            SenderCustomer = sender,
            Amount = request.Amount,
            RecipientCustomer = recipient
        });

        await _databaseDbContext.SaveChangesAsync();
    }
}