using Applications.Service.Common.Entities;
using Applications.Service.Modules.Bank.Interfaces;
using Applications.Service.Modules.Bank.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Applications.Service.Modules.Account.Controllers;

[Route("[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;
        
    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpPost("[action]")]
    public async Task<ActionResult<IList<CustomerEntity>>> FindAll()
    {
        return Ok(await _customerService.FindAll()); 
    }
    
    [HttpPost("[action]")]
    public async Task<ActionResult<CustomerEntity>> FindOneWithDetails([FromBody] FindOneByCustomerIdRequest request)
    {
        return Ok(await _customerService.FindOneWithDetails(request)); 
    }
}