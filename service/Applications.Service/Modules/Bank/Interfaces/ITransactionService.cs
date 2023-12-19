using Applications.Service.Common.Entities;
using Applications.Service.Modules.Bank.Model;
using Applications.Service.Modules.Bank.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Applications.Service.Modules.Bank.Interfaces;

public interface ITransactionService
{
    Task PerformTransaction(PerformTransactionRequest request);
}