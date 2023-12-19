using Applications.Service.Modules.Bank.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Applications.Service.Modules.Bank.Controllers;

[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly ITransactionService _transactionService;

    public AccountController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }
}