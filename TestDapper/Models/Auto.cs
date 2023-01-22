

namespace TestDapper.Models;

public class Auto
{
    public int id { get; set; }

    //[MinLength(2, ErrorMessage = "Brand must contain at least two characters!")]
    //[MaxLength(50, ErrorMessage = "Brand must contain a maximum of 50 characters!")]
    public string brand { get; set; }

    //[MinLength(1, ErrorMessage = "Model must contain at least one character!")]
    //[MaxLength(50, ErrorMessage = "Model must contain a maximum of 50 characters!")]
    public string model { get; set; }


}
