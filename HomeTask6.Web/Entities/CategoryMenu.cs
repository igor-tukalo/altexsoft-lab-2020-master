using HomeTask4.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeTask6.Web.Entities
{
    public class CategoryMenu : Category
    {
        public List<CategoryMenu> Children { get; set; }
    }
}
