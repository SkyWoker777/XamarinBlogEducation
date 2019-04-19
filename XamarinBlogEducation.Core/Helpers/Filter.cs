using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinBlogEducation.Core.Helpers
{
    public class Filter
    {
        public Filter()
        {

        }

        public Filter(string name,int key)
        {
            this.Name = name;
            this.Key = key;
        }

        public string Name { get; set; }
        public int Key { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
