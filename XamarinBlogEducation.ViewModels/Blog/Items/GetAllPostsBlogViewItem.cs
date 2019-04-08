using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinBlogEducation.ViewModels.Blog.Items
{
    public class GetAllPostsBlogViewItem
    {     
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public long CategoryId { get; set; }
        public string Author { get; set; }
        public string AuthorId { get; set; }
        public List<GetAllCommentsBlogViewItem> Comments { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
