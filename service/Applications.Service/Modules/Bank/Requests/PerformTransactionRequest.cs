namespace Applications.Service.Modules.Bank.Requests;

public class PerformTransactionRequest
{
    public string? SenderCustomerId { get; set; }
    
    public string RecipientCustomerId { get; set; }
    
    public decimal Amount { get; set; }
}