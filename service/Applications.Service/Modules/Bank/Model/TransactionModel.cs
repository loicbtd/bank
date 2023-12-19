namespace Applications.Service.Modules.Bank.Model;

public class TransactionModel
{
    public string? SenderCustomerId { get; set; } 
        
    public string RecipientCustomerId { get; set; }
    
    public decimal Amount { get; set; }
}