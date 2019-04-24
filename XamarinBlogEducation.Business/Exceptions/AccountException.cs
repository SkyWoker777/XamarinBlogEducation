using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinBlogEducation.Business.Exceptions
{
    public class AccountException: BaseException
    {
        public AccountException(string message) : base(message)
        {

        }
    }
}
