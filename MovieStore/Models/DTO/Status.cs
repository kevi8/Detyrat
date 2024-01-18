#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Extensions.Configuration.UserSecrets;
namespace MovieStore.Models;
{
    public class Status
    {
        public int StatusCode {get; set; }
        public string? Message { get; set; }
    }
}