using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Brandviser.Data.Models
{
    public class Status
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public Status()
        {

        }

        public Status(string name)
            : this()
        {
            this.Name = name;
        }
    }
}