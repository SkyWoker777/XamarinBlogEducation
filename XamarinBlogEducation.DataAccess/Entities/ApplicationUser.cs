using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace XamarinBlogEducation.DataAccess.Entities
{
    public class ApplicationUser: IdentityUser
    {
        public ApplicationUser()
        {
            this.Posts = new HashSet<Post>();
        }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public byte[] UserImage { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
