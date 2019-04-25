using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinBlogEducation.ViewModels.Requests
{
    public class AddCommentRequestBlogView
    {
        public string Content { get; set; }
        public long PostId { get; set; }
        public string UserId { get; set; }

    }
}
