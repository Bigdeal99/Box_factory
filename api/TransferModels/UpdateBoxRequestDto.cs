using System.ComponentModel.DataAnnotations;

namespace api.TransferModels;

public class UpdateBoxRequestDto
{

        public int BoxId { get; set; }
        [MinLength(4)]
        public string BoxName { get; set; }
        public double BoxWeight { get; set; } 
    
}