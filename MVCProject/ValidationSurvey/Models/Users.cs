
using System.ComponentModel.DataAnnotations;

namespace ValidationSurvey.Models;

public class Users
{
    [Required(ErrorMessage = "Name is required!")]
    [MinLength(3, ErrorMessage = "Name must be at least 3 characters in length.")]

    public string? fname { get; set; }

    public string? location { get; set; }
    public string? fav { get; set; }

    [MinLength(20, ErrorMessage = "Message must be at least 20 characters in length.")]
    public string? comment { get; set; }
}
