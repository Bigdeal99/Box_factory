using System.ComponentModel.DataAnnotations;
using api.CustomDataAnnotations;

namespace api.TransferModels;

public class CreateBoxRequestDto
{
    [MinLength(5)]
    [Required]
    public string BoxName { get; set; }
    
    [Required]
    public double BoxWeight { get; set; }
}