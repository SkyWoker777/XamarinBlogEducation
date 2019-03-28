using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using XamarinBlogEducation.ViewModels.Blog.Items;

namespace XamarinBlogEducation.ViewModels.Blog
{
    public class GetDetailsPostBlogView
    {
        [JsonProperty("Id")]
        public long Id { get; set; }
        [JsonProperty("Title")]
        public string Title { get; set; }
        [JsonProperty("Content")]
        public string Content { get; set; }
        [JsonProperty("Author")]
        public string Author { get; set; }
        [JsonProperty("Comments")]
        public List<GetAllCommentsBlogViewItem> Comments { get; set; }        
        [JsonProperty("CreationDate")]
        public DateTime CreationDate { get; set; }
    }
}
