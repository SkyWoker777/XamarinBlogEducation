using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinBlogEducation.DataAccess.Entities
{
   public class Tag:BaseEntity
    {
        public string TagName { get; set; }

        public IList<PostTag> PostTags { get; set; }
    }
}
