using HomeTask4.Core.Entities;
using System.Collections.Generic;

namespace HomeTask6.Web.Entities
{
    public class CategoryMenu : Category
    {
        public List<CategoryMenu> Children { get; set; }
    }
}
