using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinBlogEducation.ViewModels.Responses
{
        public  class GetAllUserPostResponseModel
        {
            public long Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Content { get; set; }
            public long CategoryId { get; set; }
            public string AuthorName { get; set; }
            public string AuthorId { get; set; }
            public List<GetAllCommentResponseModel> Comments { get; set; }
            public DateTime CreationDate { get; set; }
            public string Category { get; set; }
        }
}
