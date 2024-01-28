#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;


namespace MarketiIm.Models;

public class Purchase
{
    [Key]
    public int PurchaseId { get; set; }

    public int? ProductId { get; set; }

    public int? KlientId { get; set; }
    [Required(ErrorMessage ="Quantity is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Value must be greater than or equal to 1")]
    public int Quantity { get; set; }

    public int? Total { get; set; }

    public Product? Product { get; set; }
    public Klient? Klient { get; set; }

    
}






