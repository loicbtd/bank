using Applications.Service.Common.DbContexts;
using Applications.Service.Common.Entities;
using Applications.Service.Modules.Bank.Interfaces;
using Applications.Service.Modules.Bank.Model;
using Applications.Service.Modules.Bank.Requests;
using Libraries.Web.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Applications.Service.Modules.Bank.Services;

public class CustomerService : ICustomerService
{
    private readonly DatabaseDbContext _databaseDbContext;

    public CustomerService(DatabaseDbContext databaseDbContext)
    {
        _databaseDbContext = databaseDbContext;
    }

    public async Task<IList<CustomerEntity>> FindAll()
    {
        return await _databaseDbContext.Customers
            .Include(_ => _.CurrentAccount)
            .Where(_ => true).ToListAsync();
    }

    public async Task<CustomerWithDetailsModel> FindOneWithDetails(FindOneByCustomerIdRequest request)
    {
        CustomerEntity customer = await _databaseDbContext.Customers
            .Include(_ => _.CurrentAccount)
            .Where(_ => _.CustomerId == request.CustomerId)
            .FirstOrDefaultAsync();

        if (customer is null)
        {
            throw new UserException("the customer does not exist");
        }

        List<TransactionEntity> transactionEntities = await _databaseDbContext.Transactions
            .Include(_ => _.SenderCustomer)
            .Include(_ => _.RecipientCustomer)
            .Where(_ =>
                (_.SenderCustomer != null && _.SenderCustomer.CustomerId == request.CustomerId) ||
                _.RecipientCustomer.CustomerId == request.CustomerId)
            .ToListAsync();


        List<TransactionModel> transactionModels = transactionEntities.Select(_ => new TransactionModel()
        {
            RecipientCustomerId = _.RecipientCustomer.CustomerId,
            SenderCustomerId = _.SenderCustomer?.CustomerId,
            Amount = _.Amount
        }).ToList();

        CustomerWithDetailsModel customerWithDetailsModel = new CustomerWithDetailsModel()
        {
            Balance = customer.CurrentAccount?.Balance ?? 0,
            Name = customer.Name ?? "",
            Surname = customer.Surname ?? "",
            Transactions = transactionModels
        };

        return customerWithDetailsModel;
    }
}