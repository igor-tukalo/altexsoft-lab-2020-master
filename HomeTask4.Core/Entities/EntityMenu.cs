﻿namespace HomeTask4.Core.Entities
{
    public class EntityMenu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }
        public string TypeEntity { get; set; }
    }
}