using System;
using System.ComponentModel.DataAnnotations;

public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    public required string Login { get; set; }

    [Required]
    public required string Password { get; set; }
}

public class Product
{
    [Key]
    public int Id { get; set; }

    [Required]
    public required string Name { get; set; }
}

public class Stash
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int ProductId { get; set; }
    public required Product Product { get; set; }

    [Required]
    public DateTime TimeLastCheck { get; set; }

    [Required]
    public decimal BuyPrice { get; set; }
}

public class TransactionHistory
{
    [Key]
    public int Id { get; set; }

    [Required]
    public double SpendMoney { get; set; }
    public double BalanceAfterTransaction { get; set; }
    public DateTime TimeOfTransaction { get; set; }
}
