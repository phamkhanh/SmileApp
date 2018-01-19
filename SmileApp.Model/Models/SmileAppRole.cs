using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SmileApp.Model.Models
{
    public class SmileAppRole : IdentityRole
    {
        public SmileAppRole() : base()
        {
        }

        [StringLength(250)]
        public string Description { set; get; }
    }
}