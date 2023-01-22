

namespace TestDapper.Models;

public class Auto
{
    public int Id { get; set; }

    [MinLength(2, ErrorMessage = "Brand must contain at least two characters!")]
    [MaxLength(50, ErrorMessage = "Brand must contain a maximum of 50 characters!")]
    public string Brand { get; set; }

    [MinLength(1, ErrorMessage = "Model must contain at least one character!")]
    [MaxLength(50, ErrorMessage = "Model must contain a maximum of 50 characters!")]
    public string Model { get; set; }


}
