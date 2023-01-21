using System.ComponentModel.DataAnnotations;

namespace TestDapper.Models
{
    public class Driver
    {

        public int DriverId { get; set; }

        public int AutoId { get; set; }

        public string Name { get; set; }
    }
}
