using Applications.Service.Modules.Bank.Requests;

namespace Applications.Service.Modules.Bank.Interfaces;

public interface ICurrentAccountService
{
    Task OpenNewCurrentAccount(OpenNewCurrentAccountRequest request);
}