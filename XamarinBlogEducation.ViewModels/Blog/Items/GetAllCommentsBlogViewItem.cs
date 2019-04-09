using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinBlogEducation.ViewModels.Blog.Items
{
    public class GetAllCommentsBlogViewItem
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
