using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinBlogEducation.ViewModels.Responses
{
    public class GetAllCategoryResponseModel
    {
        public long Id { get; set; }
        public string Category { get; set; }

        public override string ToString()
        {
            return Category;
        }
    }
}
