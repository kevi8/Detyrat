using System.ComponentModel.DataAnnotations;

namespace SessionWorkshop.Models;

public class Data
{

    [Required(ErrorMessage = "Please fill the username before proceeding")]
    public string? username { get; set; }

    public int? number { get; set; }

}