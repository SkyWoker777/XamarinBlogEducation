using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinBlogEducation.ViewModels.Models.Blog
{
    public class AddCommentBlogViewModel
    {
        public string Content { get; set; }
        public int PostId { get; set; }
        public string UserId { get; set; }

    }
}
