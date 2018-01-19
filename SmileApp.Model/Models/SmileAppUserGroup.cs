using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmileApp.Model.Models
{
    public class SmileAppUserGroup
    {
        [StringLength(128)]
        [Key]
        [Column(Order = 1)]
        public string UserId { set; get; }

        [Key]
        [Column(Order = 2)]
        public int GroupId { set; get; }

        [ForeignKey("UserId")]
        public virtual SmileAppUser SmileAppUser { set; get; }

        [ForeignKey("GroupId")]
        public virtual SmileAppGroup SmileAppGroup { set; get; }
    }
}