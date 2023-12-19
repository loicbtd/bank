using System.ComponentModel.DataAnnotations;

namespace Applications.Service.Modules.Bank.Requests;

public class OpenNewCurrentAccountRequest
{
    [Required]
    public string CustomerId { get; set; }

    [Required] 
    public decimal InitialCredit { get; set; } = 0;
}