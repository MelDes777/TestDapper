



namespace TestDapper.Models;

public class Driver
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50, ErrorMessage = "Name must contain a maximum of 50 characters!")]
    public string Name { get; set; }

    [ForeignKey("Auto")]
    public int AutoId { get; set; }
    public List<Auto> Autos { get; set; } = new List<Auto>();


}
