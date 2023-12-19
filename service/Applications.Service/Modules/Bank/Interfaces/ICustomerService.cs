using Applications.Service.Common.Entities;
using Applications.Service.Modules.Bank.Model;
using Applications.Service.Modules.Bank.Requests;

namespace Applications.Service.Modules.Bank.Interfaces;

public interface ICustomerService
{
    Task<IList<CustomerEntity>> FindAll();

    Task<CustomerWithDetailsModel> FindOneWithDetails(FindOneByCustomerIdRequest request);
}