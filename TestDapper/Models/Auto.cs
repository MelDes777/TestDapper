

namespace TestDapper.Models;

public class Auto
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50, ErrorMessage = "Brand must contain a maximum of 50 characters!")]
    public string Brand { get; set; }

    [Required]
    [MaxLength(50, ErrorMessage = "Model must contain a maximum of 50 characters!")]
    public string Model { get; set; }


}
