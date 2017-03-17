using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Brandviser.Data.Models
{
    public class Status
    {
        private ICollection<Domain> domains;

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public Status()
        {
            this.domains = new HashSet<Domain>();
        }

        public Status(string name)
            : this()
        {
            this.Name = name;
        }

        public virtual ICollection<Domain> Domains
        {
            get
            {
                return this.domains;
            }

            set
            {
                this.domains = value;
            }
        }
    }
}