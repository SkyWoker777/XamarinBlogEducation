using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XamarinBlogEducation.Business.Exceptions
{
    public class BaseException : ApplicationException
    {
        public BaseException(string message) : base(message)
        {
        }
    }
}
