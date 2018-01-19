using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmileApp.Model.Models
{
    [Table("SmileAppRoleGroups")]
    public class SmileAppRoleGroup
    {
        [Key]
        [Column(Order = 1)]
        public int GroupId { set; get; }

        [Column(Order = 2)]
        [StringLength(128)]
        [Key]
        public string RoleId { set; get; }

        [ForeignKey("RoleId")]
        public virtual SmileAppRole SmileAppRole { set; get; }

        [ForeignKey("GroupId")]
        public virtual SmileAppGroup SmileAppGroup { set; get; }
    }
}