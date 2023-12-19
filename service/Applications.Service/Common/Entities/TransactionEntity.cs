using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Applications.Service.Common.Entities;

[Table("transaction")]
public class TransactionEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("sender_customer_id")]
    public CustomerEntity? SenderCustomer { get; set; }
    
    [Column("recipient_customer_id")]
    public CustomerEntity RecipientCustomer { get; set; }
    
    [Column("recipient_customer_id")]
    public decimal Amount { get; set; }
}