using System.ComponentModel.DataAnnotations;
using api.CustomDataAnnotations;

namespace api.TransferModels;

public class CreateBoxRequestDto
{
    [MinLength(5)]
    [ValueIsOneOf(new string[] {"box_1", "box_2"}, "Must be box one or tow ...")]
    [Required]
    public string BoxName { get; set; }
    
    [Required]
    public double BoxWeight { get; set; }
}