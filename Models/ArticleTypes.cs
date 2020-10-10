using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plethora.Models
{
    [Table("Article_Types")]
    public partial class ArticleTypes
    {
        public ArticleTypes()
        {
            Articles = new HashSet<Articles>();
        }

        [Key]
        [Column("Article_Type_ID")]
        public int ArticleTypeId { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [Column("Date_Created", TypeName = "datetime")]
        public DateTime DateCreated { get; set; }

        [InverseProperty("ArticleType")]
        public virtual ICollection<Articles> Articles { get; set; }
    }
}
