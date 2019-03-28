using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinBlogEducation.ViewModels.Blog.Items
{
    public class GetAllPostsBlogViewItem
    {
        [JsonProperty("Id")]
        public long Id { get; set; }
        [JsonProperty("Title")]
        public string Title { get; set; }
        [JsonProperty("Description")]
        public string Description { get; set; }
        [JsonProperty("Content")]
        public string Content { get; set; }
        [JsonProperty("CategoryId")]
        public long CategoryId { get; set; }
        [JsonProperty("Author")]
        public string Author { get; set; }
        [JsonProperty("Comments")]
        public List<GetAllCommentsBlogViewItem> Comments { get; set; }
        [JsonProperty("CreationDate")]
        public DateTime CreationDate { get; set; }
    }
}
