using System;
using System.Collections.Generic;
using System.Text;

namespace task2.Models
{
    public class Recipe : EntityMenu
    {
        public string Description { get; set; }
        public int IdCategory { get; set; }

    }
}
