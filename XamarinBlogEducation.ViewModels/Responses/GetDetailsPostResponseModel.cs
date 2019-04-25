using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace XamarinBlogEducation.ViewModels.Responses
{
    public class GetDetailsPostResponseModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string AuthorName { get; set; }
        public List<GetAllCommentResponseModel> Comments { get; set; }     
        public DateTime CreationDate { get; set; }
        public string Category { get; set; }
    }
}
