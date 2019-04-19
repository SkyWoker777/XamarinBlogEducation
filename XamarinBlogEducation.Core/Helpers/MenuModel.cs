using MvvmCross.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinBlogEducation.Core.Helpers
{
    public class MenuModel
    {
        public String Title
        {
            get;
            set;
        }

        public IMvxCommand Navigate
        {
            get;
            set;
        }

        public MenuModel() { }
    }
}
