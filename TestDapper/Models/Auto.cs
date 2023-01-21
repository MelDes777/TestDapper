using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace TestDapper.Models
{
    public class Auto
    {
        public int AutoId { get; set; }

        [Microsoft.Build.Framework.Required]
        [MinLength(2, ErrorMessage = "Brand must contain at least two characters!")]
        [MaxLength(50, ErrorMessage = "Brand must contain a maximum of 50 characters!")]
        public string Brand { get; set; }

        [Microsoft.Build.Framework.Required]
        [MinLength(1, ErrorMessage = "Model must contain at least one character!")]
        [MaxLength(50, ErrorMessage = "Model must contain a maximum of 50 characters!")]
        public string Model { get; set; }


    }
}
