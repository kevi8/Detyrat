using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
#pragma warning disable CS8618

namespace MarketiIm.Models;

public class Association
{
    [Key]
    public int AssociationId { get; set; }

    public int? ProductId { get; set; }

    public int? CategoryId { get; set; }

    public Product? Product { get; set; }
    public Category? Category { get; set; }
}






