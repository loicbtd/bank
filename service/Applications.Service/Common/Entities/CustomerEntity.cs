using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Applications.Service.Common.Entities;

[Table("customer")]
public class CustomerEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public Guid Id { get; set; }
    
    [Column("customer_id")]
    public string CustomerId { get; set; }
    
    [Column("name")]
    public string? Name { get; set; }
    
    [Column("surname")]
    public string? Surname { get; set; }
    
    [Column("current_account_id")]
    public CurrentAccountEntity? CurrentAccount { get; set; }
}
