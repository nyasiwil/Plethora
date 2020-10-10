using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plethora.Models
{
    public partial class Highlights
    {
        [Key]
        [Column("Highlight_ID")]
        public int HighlightId { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [Required]
        [StringLength(150)]
        public string Description { get; set; }
        [Column("New_Tab")]
        public bool NewTab { get; set; }
        [Column("Status_ID")]
        public int StatusId { get; set; }
        [Column("Date_Updated", TypeName = "datetime")]
        public DateTime DateUpdated { get; set; }
        [Column("Date_Created", TypeName = "datetime")]
        public DateTime DateCreated { get; set; }

        [ForeignKey(nameof(StatusId))]
        [InverseProperty(nameof(Statuses.Highlights))]
        public virtual Statuses Status { get; set; }
    }
}
