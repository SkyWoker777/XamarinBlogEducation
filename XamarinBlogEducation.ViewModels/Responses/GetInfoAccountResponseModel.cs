using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinBlogEducation.ViewModels.Responses
{
    public class GetInfoAccountResponseModel
    {
        public string Email { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        public byte[] UserImage { get; set; }
    }
}
