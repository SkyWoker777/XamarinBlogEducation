using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XamarinBlogEducation.Core.Helpers
{
    public class AllFilters
    {
       
        public AllFilters()
        {
         
        }

        public  List<Filter> list = new List<Filter>() {
            new Filter("By Date",1),
            new Filter("By Date( newest)",3),
            new Filter("A-Z",2)};


    }
}
