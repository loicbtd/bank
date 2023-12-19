using Applications.Service.Modules.Bank.Interfaces;
using Applications.Service.Modules.Bank.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Applications.Service.Modules.Bank.Controllers;

[Route("[controller]")]
public class CurrentAccountController : ControllerBase
{
    private readonly ICurrentAccountService _currentAccountService;
        
    public CurrentAccountController(ICurrentAccountService currentAccountService)
    {
        _currentAccountService = currentAccountService;
    }

    [HttpPost("[action]")]
    public async Task<ActionResult> OpenNewCurrentAccount([FromBody] OpenNewCurrentAccountRequest? request)
    {
        await _currentAccountService.OpenNewCurrentAccount(request);
        return Ok(); 
    }
}

