using System.ComponentModel.DataAnnotations;
using api.CustomDataAnnotations;

namespace api.TransferModels;

public class UpdateBoxRequestDto
{
        [Length(4,6)]
        [Required]
        public string BoxName { get; set; }
    
        [Required]
        public double BoxWeight { get; set; }
    
}