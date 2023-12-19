namespace Applications.Service.Modules.Bank.Model;

public class CustomerWithDetailsModel
{
    public string Name { get; set; } 

    public string Surname { get; set; }
    
    public decimal Balance { get; set; }
    
    public List<TransactionModel> Transactions { get; set; }
}