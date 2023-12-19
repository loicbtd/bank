using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Applications.Service.Common.Entities;

[Table("current_account")]

public class CurrentAccountEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public Guid Id { get; set; }
    
    
    [Column("balance")]
    public decimal Balance { get; set; } = 0
;}