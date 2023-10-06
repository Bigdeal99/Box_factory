using System.ComponentModel.DataAnnotations;
using api.CustomDataAnnotations;

namespace api.TransferModels;

public class CreateBoxRequestDto
{
    [MinLength(5)]
    public string BoxName { get; set; }
    
    [ValueIsOneOf(new string[] {"box_1", "box_2"}, "Must be one one ...")]

    public double BoxWeight { get; set; }
}