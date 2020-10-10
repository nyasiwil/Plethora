using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plethora.Models
{
    public partial class Statuses
    {
        public Statuses()
        {
            Articles = new HashSet<Articles>();
            Highlights = new HashSet<Highlights>();
        }

        [Key]
        [Column("Status_ID")]
        public int StatusId { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [Column("Date_Created", TypeName = "datetime")]
        public DateTime DateCreated { get; set; }

        [InverseProperty("Status")]
        public virtual ICollection<Articles> Articles { get; set; }
        [InverseProperty("Status")]
        public virtual ICollection<Highlights> Highlights { get; set; }
    }
}
