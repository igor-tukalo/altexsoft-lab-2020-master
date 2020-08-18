using System;
using System.Collections.Generic;
using System.Text;

namespace task2.Models
{
    public abstract class EntityMenu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }
        public string TypeEntity { get; set; }

        public EntityMenu(int id = 0, string name = "", int? parentId = 0, string typeEntity= "")
        {
            this.Id = id;
            this.ParentId = parentId.HasValue ? Convert.ToInt32(parentId) : 0;
            this.Name = name;
            this.TypeEntity = typeEntity;
        }
    }
}
