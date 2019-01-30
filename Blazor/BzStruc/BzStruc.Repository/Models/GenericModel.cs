using BzStruc.Repository.Enums;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BzStruc.Repository.Models
{
   

    public partial class GenericUser : IdentityUser<int>
    {
        public GenericUser()
        {
            Participants = new HashSet<Participants>();
            Messages = new HashSet<Messages>();
            MessageReadBy = new HashSet<MessageReadBy>();
        }
        [Required]
        [StringLength(200)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(200)]
        public string LastName { get; set; }

        public string RefreshToken { get; set; }
        public UserOnlineStatus OnlineStatus { get; set; }

        public virtual ICollection<Participants> Participants { get; set; }
        public virtual ICollection<Messages> Messages { get; set; }
        public virtual ICollection<MessageReadBy> MessageReadBy { get; set; }

    }

    public class GenericUserLogin : IdentityUserLogin<int>
    {
    }

    public class GenericUserRole : IdentityUserRole<int>
    {
    }

    public class GenericUserClaim : IdentityUserClaim<int>
    {
    }

    public class GenericRoleClaim : IdentityRoleClaim<int>
    {
    }

    public class GenericUserToken : IdentityUserToken<int>
    {
    }

    public class GenericRole : IdentityRole<int>
    {

        public bool IsActive { get; set; }
    }
}
