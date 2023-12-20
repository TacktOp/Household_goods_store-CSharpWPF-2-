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
    public string Name { get; set; }

    [Required]
    public int Price { get; set; }  
}

public class Shop
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Addres { get; set; }
}