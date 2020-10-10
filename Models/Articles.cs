using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plethora.Models
{
    public partial class Articles
    {
        [Key]
        [Column("Article_ID")]
        public int ArticleId { get; set; }
        [Column("Article_Type_ID")]
        public int ArticleTypeId { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [Required]
        [StringLength(200)]
        public string Intro { get; set; }
        [Required]
        [Column("By_Line")]
        [StringLength(100)]
        public string ByLine { get; set; }
        [Required]
        [Column("Body_Copy")]
        public string BodyCopy { get; set; }
        [Column("Status_ID")]
        public int StatusId { get; set; }
        [Column("Date_Updated", TypeName = "datetime")]
        public DateTime DateUpdated { get; set; }
        [Column("Date_Created", TypeName = "datetime")]
        public DateTime DateCreated { get; set; }

        [ForeignKey(nameof(ArticleTypeId))]
        [InverseProperty(nameof(ArticleTypes.Articles))]
        public virtual ArticleTypes ArticleType { get; set; }
        [ForeignKey(nameof(StatusId))]
        [InverseProperty(nameof(Statuses.Articles))]
        public virtual Statuses Status { get; set; }
    }
}
