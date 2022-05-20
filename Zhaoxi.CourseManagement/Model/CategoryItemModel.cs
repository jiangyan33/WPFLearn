using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhaoxi.CourseManagement.Model
{
    public class CategoryItemModel
    {
        public bool IsSelected { get; set; }

        public string CategoryName { get; set; }

        public CategoryItemModel(string name, bool state = false)
        {
            CategoryName = name;
            IsSelected = state;
        }
    }
}
